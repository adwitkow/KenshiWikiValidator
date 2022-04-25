using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiTemplates;
using KenshiWikiValidator.WikiTemplates.Creators;

namespace KenshiWikiValidator.WikiCategories.Weapons.Rules
{
    internal class ContainsWeaponTemplateRule : ContainsDetailedTemplateRuleBase
    {
        private readonly IItemRepository itemRepository;

        public ContainsWeaponTemplateRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        protected override WikiTemplate PrepareTemplate(ArticleData data)
        {
            var weaponTemplateCreator = new WeaponTemplateCreator(this.itemRepository, data);

            return weaponTemplateCreator.Generate();
        }
    }
}
