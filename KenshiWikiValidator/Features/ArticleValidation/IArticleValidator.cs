using KenshiWikiValidator.Features.ArticleValidation;

namespace KenshiWikiValidator
{
    internal interface IArticleValidator
    {
        public string CategoryName { get; }

        ArticleValidationResult Validate(string title, string content);
    }
}