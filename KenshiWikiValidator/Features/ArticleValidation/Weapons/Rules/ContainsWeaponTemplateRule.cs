using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.WikiTemplates;
using KenshiWikiValidator.Features.WikiTemplates.Creators;
using KenshiWikiValidator.OcsProxy;

namespace KenshiWikiValidator.Features.ArticleValidation.Weapons.Rules
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
