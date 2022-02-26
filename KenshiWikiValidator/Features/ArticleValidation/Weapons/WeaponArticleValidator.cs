using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using KenshiWikiValidator.Features.ArticleValidation.Weapons.Rules;
using KenshiWikiValidator.Features.DataItemConversion;

namespace KenshiWikiValidator.Features.ArticleValidation.Weapons
{
    public class WeaponArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public WeaponArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new StringIdRule(itemRepository, wikiTitles),
                new NewLinesRule(),
                new ContainsBlueprintsSectionRule(itemRepository, wikiTitles),
                new ContainsWeaponTemplateRule(itemRepository),
                new ContainsWeaponNavboxRule(),
                new ContainsWeaponCraftingSectionRule(itemRepository),
            };
        }

        public override string CategoryName => "Weapons"; // TODO: Should be melee weapons, actually

        public override IEnumerable<IValidationRule> Rules => this.rules;
    }
}