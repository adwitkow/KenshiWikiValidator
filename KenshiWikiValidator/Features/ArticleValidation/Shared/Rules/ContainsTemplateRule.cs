namespace KenshiWikiValidator.Features.ArticleValidation.Shared.Rules
{
    public class ContainsTemplateRule : IValidationRule
    {
        public ContainsTemplateRule(string templateName, IEnumerable<string>? categoryExceptions = null)
        {
            this.TemplateName = templateName;
            this.CategoryExceptions = categoryExceptions ?? Enumerable.Empty<string>();
        }

        public string TemplateName { get; }

        public IEnumerable<string> CategoryExceptions { get; }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            if (this.AnyCategoryMatchesExceptions(data))
            {
                return result;
            }

            if (!data.WikiTemplates.Any(template => template.Name.Equals(this.TemplateName)))
            {
                result.AddIssue($"Article does not contain template of name '{this.TemplateName}'");
            }

            return result;
        }

        private bool AnyCategoryMatchesExceptions(ArticleData data)
        {
            return this.CategoryExceptions
                .Any(exception => data.Categories
                    .Any(category => category.Equals(exception, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
