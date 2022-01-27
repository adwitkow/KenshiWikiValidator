using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators
{
    public abstract class ArticleValidatorBase : IArticleValidator
    {
        public abstract string CategoryName { get; }

        protected abstract IEnumerable<IValidationRule> Rules { get; }

        public ArticleValidationResult Validate(string title, string content)
        {
            var result = new ArticleValidationResult();
            var results = new List<RuleResult>();
            var data = new ArticleData();
            foreach (IValidationRule? rule in this.Rules)
            {
                results.Add(rule.Execute(title, content, data));
            }

            var success = !results.Any(result => !result.Success);

            if (!success)
            {
                var issues = results.SelectMany(result => result.Issues);
                result.Issues = issues;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{title} is OK!");
                Console.ResetColor();
            }

            return result;
        }
    }
}
