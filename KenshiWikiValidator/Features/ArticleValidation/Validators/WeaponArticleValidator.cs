using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators
{
    public class WeaponArticleValidator : IArticleValidator
    {
        private readonly IEnumerable<IValidationRule> rules;

        public WeaponArticleValidator(IItemRepository itemRepository)
        {
            rules = new List<IValidationRule>()
            {
                new StringIdRule(itemRepository),
                new NewLinesRule(),
            };
        }

        public string CategoryName => "Weapons"; // TODO: Should be melee weapons, actually

        public ArticleValidationResult Validate(string title, string articleContent)
        {
            var result = new ArticleValidationResult();
            var results = new List<RuleResult>();
            foreach (var rule in rules)
            {
                results.Add(rule.Execute(title, articleContent));
            }

            var success = !results.Any(result => !result.Success);

            if (!success)
            {
                var issues = results.SelectMany(result => result.Issues);
                result.Issues = issues;
            }

            return result;
        }
    }
}