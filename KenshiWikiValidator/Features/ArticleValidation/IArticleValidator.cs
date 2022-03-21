using KenshiWikiValidator.Features.ArticleValidation.Shared;

namespace KenshiWikiValidator.Features.ArticleValidation
{
    public interface IArticleValidator
    {
        public string CategoryName { get; }

        public IEnumerable<IValidationRule> Rules { get; }

        ArticleValidationResult Validate(string title, string content);
    }
}