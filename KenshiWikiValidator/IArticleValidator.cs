namespace KenshiWikiValidator
{
    internal interface IArticleValidator
    {
        public string CategoryName { get; }

        ArticleValidationResult Validate(string articleContent);
    }
}