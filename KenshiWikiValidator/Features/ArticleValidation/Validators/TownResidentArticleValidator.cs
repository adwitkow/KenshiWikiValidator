using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.DataItemConversion;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators
{
    public class TownResidentArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public TownResidentArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new NewLinesRule(),
                new StringIdRule(itemRepository, wikiTitles),
            };
        }

        public override string CategoryName => "Town Residents";

        protected override IEnumerable<IValidationRule> Rules => this.rules;
    }
}
