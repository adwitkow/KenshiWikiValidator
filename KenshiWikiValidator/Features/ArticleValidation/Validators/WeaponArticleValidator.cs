using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators
{
    public class WeaponArticleValidator : IArticleValidator
    {
        private readonly TemplateParser parser;
        private readonly IEnumerable<IValidationRule> rules;

        public WeaponArticleValidator(TemplateParser parser)
        {
            this.parser = parser;

            rules = new List<IValidationRule>()
            {
                new NewLinesRule()
            };
        }

        public string CategoryName => "Weapons"; // TODO: Should be melee weapons, actually

        public ArticleValidationResult Validate(string articleContent)
        {
            var result = new ArticleValidationResult();
            var results = new List<RuleResult>();
            foreach (var rule in rules)
            {
                results.Add(rule.Execute(articleContent));
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