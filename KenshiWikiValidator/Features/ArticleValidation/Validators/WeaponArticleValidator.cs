using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators
{
    public class WeaponArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public WeaponArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new StringIdRule(itemRepository, wikiTitles),
                new ContainsBlueprintTemplateRule(itemRepository, wikiTitles),
                new NewLinesRule(),
            };
        }

        public override string CategoryName => "Weapons"; // TODO: Should be melee weapons, actually

        protected override IEnumerable<IValidationRule> Rules => this.rules;
    }
}