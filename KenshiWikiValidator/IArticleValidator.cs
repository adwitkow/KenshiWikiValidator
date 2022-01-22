internal interface IArticleValidator
{
    ArticleValidationResult Validate(string articleContent);
}