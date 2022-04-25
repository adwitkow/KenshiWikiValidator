using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using KenshiWikiValidator.OcsProxy;

namespace KenshiWikiValidator.Features.ArticleValidation.TownResident
{
    public class TownResidentArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        public TownResidentArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new NewLinesRule(),
                new ContainsTemplateRule("Traders"),
                new StringIdRule(itemRepository, wikiTitles, true),
            };
        }

        public override string CategoryName => "Town Residents";

        public override IEnumerable<IValidationRule> Rules => this.rules;
    }
}
