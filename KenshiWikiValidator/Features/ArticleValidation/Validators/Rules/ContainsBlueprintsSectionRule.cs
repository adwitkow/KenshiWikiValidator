﻿using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Builders.Components;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using KenshiWikiValidator.Features.WikiSections;
using KenshiWikiValidator.Features.WikiTemplates.Creators;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public class ContainsBlueprintsSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitleCache;
        private readonly BlueprintSquadsConverter blueprintSquadsConverter;

        public ContainsBlueprintsSectionRule(
            IItemRepository itemRepository,
            WikiTitleCache wikiTitleCache)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;

            this.blueprintSquadsConverter = new BlueprintSquadsConverter(itemRepository);
        }

        protected override WikiSectionBuilder CreateSectionBuilder(ArticleData data)
        {
            var builder = new WikiSectionBuilder();

            var stringId = data.Get("string id");
            if (string.IsNullOrEmpty(stringId))
            {
                return null!;
            }

            var templateCreator = new BlueprintWikiTemplateCreator(this.itemRepository, data);
            var template = templateCreator.Generate();

            if (template is null)
            {
                return builder
                    .WithHeader("Blueprints")
                    .WithParagraph("There are no [[Blueprints]] for this item available in the game.");
            }

            var item = this.itemRepository.GetItemByStringId(stringId);

            if (item is null || item is not IResearchable researchable)
            {
                throw new InvalidOperationException($"{item.Name} is not researchable.");
            }

            IEnumerable<Squad> blueprintSquads = this.GetBlueprintSquads(researchable);

            var shopSquads = blueprintSquads.Where(squad => squad.IsShop && this.wikiTitleCache.HasArticle(squad));
            var lootSquads = blueprintSquads.Where(squad => !squad.IsShop);

            var shopLocations = this.ConvertLocationLinks(shopSquads);
            var lootLocations = this.ConvertLocationLinks(lootSquads);

            builder.WithHeader("Blueprints")
                .WithTemplate(template)
                .WithNewline()
                .WithLine("<br />")
                .WithParagraph("The [[Blueprints]] for this item can be found at the following locations.")
                .WithSubsection("Shops", 1)
                .WithUnorderedList(shopLocations)
                .WithSubsection("Loot", 1)
                .WithUnorderedList(lootLocations);

            return builder;
        }

        private IEnumerable<Squad> GetBlueprintSquads(IResearchable researchable)
        {
            IEnumerable<ItemReference> blueprintSquadReferences;
            if (researchable.UnlockingResearch is null)
            {
                if (!researchable.BlueprintSquads.Any())
                {
                    return Enumerable.Empty<Squad>();
                }

                blueprintSquadReferences = researchable.BlueprintSquads;
            }
            else
            {
                var research = this.itemRepository.GetDataItemByStringId(researchable.UnlockingResearch.StringId!);
                blueprintSquadReferences = this.blueprintSquadsConverter.Convert(research, "blueprints");
            }

            return blueprintSquadReferences
                   .Select(reference => this.itemRepository.GetItemByStringId(reference.StringId))
                   .Cast<Squad>();
        }

        private IEnumerable<string> ConvertLocationLinks(IEnumerable<Squad> squads)
        {
            var articleSquads = squads
                .Where(squad => this.wikiTitleCache.HasArticle(squad));
            var squadArticles = articleSquads.Select(squad => $"[[{this.wikiTitleCache.GetTitle(squad)}]]");

            var locationReferences = squads
                .Except(articleSquads)
                .SelectMany(squad => squad.Locations);

            var results = locationReferences
                .GroupBy(reference => reference.Name)
                .Select(group => group.First())
                .Select(reference => $"[[{reference.Name}]]")
                .Concat(squadArticles);
            return results.OrderBy(loc => loc);
        }
    }
}
