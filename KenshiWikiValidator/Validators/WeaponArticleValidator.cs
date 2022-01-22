using KenshiWikiValidator.Validators.Rules;

namespace KenshiWikiValidator.Validators
{
    public class WeaponArticleValidator : IArticleValidator
    {
        private readonly TemplateParser parser;
        private readonly IEnumerable<IValidationRule> rules;

        public WeaponArticleValidator(TemplateParser parser)
        {
            this.parser = parser;

            this.rules = new List<IValidationRule>()
            {
                new NewLinesRule()
            };
        }

        public ArticleValidationResult Validate(string articleContent)
        {
            return new ArticleValidationResult();
        }
    }
}