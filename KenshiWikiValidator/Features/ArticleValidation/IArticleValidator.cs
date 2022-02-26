using KenshiWikiValidator.Features.ArticleValidation;
using KenshiWikiValidator.Features.ArticleValidation.Shared;

namespace KenshiWikiValidator
{
    public interface IArticleValidator
    {
        public string CategoryName { get; }

        public IEnumerable<IValidationRule> Rules { get; }

        ArticleValidationResult Validate(string title, string content);
    }
}