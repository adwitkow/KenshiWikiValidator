// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiSections;
using KenshiWikiValidator.WikiTemplates;
using KenshiWikiValidator.WikiTemplates.Creators;

namespace KenshiWikiValidator.WikiCategories.SharedRules
{
    public class ContainsBlueprintsSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitleCache;

        public ContainsBlueprintsSectionRule(
            IItemRepository itemRepository,
            WikiTitleCache wikiTitleCache)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
        }

        protected override WikiSectionBuilder CreateSectionBuilder(ArticleData data)
        {
            var stringId = data.StringIds.SingleOrDefault();
            if (string.IsNullOrEmpty(stringId))
            {
                return null!;
            }

            var templateCreator = new BlueprintTemplateCreator(this.itemRepository, data);
            var template = templateCreator.Generate();

            if (template is null)
            {
                return new WikiSectionBuilder()
                    .WithHeader("Blueprints")
                    .WithParagraph("There are no [[Blueprints]] for this item available in the game.");
            }

            var item = this.itemRepository.GetItemByStringId(stringId);
            var research = this.GetUnlockingResearch(item);

            IEnumerable<Squad> blueprintSquads = this.GetBlueprintSquads(research);

            var shopSquads = blueprintSquads.Where(squad => squad.IsShop && this.wikiTitleCache.HasArticle(squad));
            var lootSquads = blueprintSquads.Where(squad => !squad.IsShop);

            var shopLocations = this.ConvertLocationLinks(shopSquads);
            var lootLocations = this.ConvertLocationLinks(lootSquads);

            return CreateBuilder(template, shopLocations, lootLocations);
        }

        private static WikiSectionBuilder CreateBuilder(WikiTemplate template, IEnumerable<string> shopLocations, IEnumerable<string> lootLocations)
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("Blueprints")
                .WithTemplate(template)
                .WithNewline()
                .WithParagraph("The [[Blueprints]] for this item can be found at the following locations.")
                .WithSubsection("Shops", 1);

            if (shopLocations.Any())
            {
                builder.WithUnorderedList(shopLocations)
                    .WithNewline();
            }
            else
            {
                builder.WithParagraph("''This item's blueprint cannot be bought at any location.''");
            }

            builder.WithSubsection("Loot", 1);

            if (lootLocations.Any())
            {
                builder.WithUnorderedList(lootLocations);
            }
            else
            {
                builder.WithParagraph("''This item's blueprint is not available as loot at any location.''");
            }

            return builder;
        }

        private IEnumerable<Squad> GetBlueprintSquads(IItem? item)
        {
            if (item is null)
            {
                return Enumerable.Empty<Squad>();
            }

            var vendorLists = this.itemRepository.GetItems<VendorList>()
                .Where(vendor => vendor.ArmourBlueprints.Any(armourBlueprintRef => armourBlueprintRef.Item == item)
                || vendor.Blueprints.Any(blueprintRef => blueprintRef.Item == item)
                || vendor.CrossbowBlueprints.Any(crossbowBlueprintRef => crossbowBlueprintRef.Item == item));
            var squads = this.itemRepository.GetItems<Squad>()
                .Where(squad => squad.Vendors
                    .Any(vendorRef => vendorLists.Contains(vendorRef.Item)));

            return squads;
        }

        private IEnumerable<string> ConvertLocationLinks(IEnumerable<Squad> squads)
        {
            var articleSquads = squads
                .Where(squad => this.wikiTitleCache.HasArticle(squad));
            var squadArticles = articleSquads.Select(squad => $"[[{this.wikiTitleCache.GetTitle(squad)}]]");

            var locationReferences = squads
                .Except(articleSquads)
                .SelectMany(squad => squad.GetLocations(this.itemRepository));

            var results = locationReferences
                .Select(reference => this.wikiTitleCache.HasArticle(reference.StringId)
                    ? $"[[{this.wikiTitleCache.GetTitle(reference.StringId, reference.Name)}]]"
                    : $"[[{reference.Name}]]")
                .Concat(squadArticles)
                .Distinct();
            return results.OrderBy(loc => loc);
        }

        private Research? GetUnlockingResearch(IItem item)
        {
            var items = this.itemRepository.GetItems<Research>();
            return items
                .Where(research => research.EnableWeaponTypes.Any(weaponTypeRef => weaponTypeRef.Item == item))
                .SingleOrDefault();
        }
    }
}
