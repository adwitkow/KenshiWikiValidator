namespace KenshiWikiValidator
{
    internal interface IArticleValidator
    {
        ArticleValidationResult Validate(string articleContent);
    }
}