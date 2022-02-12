using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.WikiSections;
using KenshiWikiValidator.Features.WikiTemplates.Creators;

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
            var stringId = data.Get("string id");
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

            var craftingTemplateCreator = new CraftingTemplateCreator();
            var builder = new WikiSectionBuilder()
                .WithHeader("Crafting");

            if (weapon.UnlockingResearch is not null)
            {
                builder.WithParagraph("This item can be crafted using [[Weapon Smithing Bench]].");

                craftingTemplateCreator.Output = weapon.Name;
                craftingTemplateCreator.ImageSettings = "96px";

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
            }
            else
            {
                builder.WithParagraph("''This item cannot be crafted.''");
            }

            return builder;
        }
    }
}
