using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.WikiSections;
using KenshiWikiValidator.Features.WikiTemplates.Creators;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.WeaponComponents;

namespace KenshiWikiValidator.Features.ArticleValidation.Weapons.Rules
{
    public class ContainsWeaponCraftingSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;

        public ContainsWeaponCraftingSectionRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        protected override WikiSectionBuilder CreateSectionBuilder(ArticleData data)
        {
            var stringId = data.StringIds.SingleOrDefault();
            if (string.IsNullOrEmpty(stringId))
            {
                return null!;
            }

            var item = this.itemRepository.GetItemByStringId(stringId);
            var cost = (int)item.Properties["material cost"];

            if (item is null || item is not Weapon weapon)
            {
                return null!;
            }

            var builder = new WikiSectionBuilder()
                .WithHeader("Crafting");

            if (weapon.UnlockingResearch is null)
            {
                builder.WithParagraph("This item cannot be crafted.");
                return builder;
            }

            var hasBlueprints = weapon.BlueprintSquads.Any();

            if (!hasBlueprints)
            {
                var research = this.itemRepository.GetDataItemByStringId(weapon.UnlockingResearch.StringId);

                var costsDictionary = new Dictionary<string, int>();
                foreach (var reference in research.GetReferences("cost"))
                {
                    var costItem = this.itemRepository.GetDataItemByStringId(reference.TargetId);
                    costsDictionary.Add(costItem.Name, reference.Value0);
                }

                var buildings = research.GetReferenceItems(this.itemRepository, "enable buildings")
                    .Select(building => building.Name.Equals("Weapon Smith") ? "Weapon Smithing Bench" : building.Name); // TODO: Apply ArticleData
                var items = research.GetReferenceItems(this.itemRepository, "enable weapon type")
                    .Select(weapon => weapon.Name);
                var prerequisites = research.GetReferenceItems(this.itemRepository, "requirements")
                    .Select(tech => $"{tech.Name} (Tech)");
                var requiredFor = this.itemRepository.GetReferencingDataItemsFor(research.StringId)
                    .Where(tech => tech.GetReferences("requirements")
                        .Any(reference => reference.TargetId.Equals(research.StringId)))
                    .Select(tech => $"{tech.Name} (Tech)");

                var researchInfoTemplateCreator = new ResearchInfoTemplateCreator()
                {
                    Costs = costsDictionary.Select(pair => $"{pair.Value} [[{pair.Key}]]s"),
                    Description = research.Values["description"].ToString()!,
                    NewBuildings = buildings,
                    NewItems = items,
                    Prerequisites = prerequisites,
                    RequiredFor = requiredFor,
                    ResearchName = research.Name,
                    TechLevel = research.GetInt("level"),
                    Time = research.GetInt("time"),
                };

                var template = researchInfoTemplateCreator.Generate();

                builder.WithTemplate(template)
                    .WithNewline();
            }

            var craftingListIntro = "This item can be crafted in various qualities using different levels of [[Weapon Smithing Bench]]";
            if (hasBlueprints)
            {
                craftingListIntro += " after learning the appropriate [[Blueprints|blueprint]].";
            }
            else
            {
                craftingListIntro += ".";
            }

            builder.WithParagraph(craftingListIntro);

            var craftingTemplateCreator = new CraftingTemplateCreator()
            {
                Output = weapon.Name,
                ImageSettings = "96px",
                Collapsed = true,
            };

            craftingTemplateCreator.BuildingName = "Weapon Smith I";
            craftingTemplateCreator.Input1 = ("Iron Plates", cost);

            builder.WithTemplate(craftingTemplateCreator.Generate());

            craftingTemplateCreator.BuildingName = "Weapon Smith II";
            craftingTemplateCreator.Input1 = ("Iron Plates", cost);
            craftingTemplateCreator.Input2 = ("Fabrics", cost);

            builder.WithTemplate(craftingTemplateCreator.Generate());

            craftingTemplateCreator.BuildingName = "Weapon Smith III";
            craftingTemplateCreator.Input1 = ("Steel Bars", cost);
            craftingTemplateCreator.Input2 = ("Fabrics", cost);

            builder.WithTemplate(craftingTemplateCreator.Generate());

            return builder;
        }
    }
}
