namespace KenshiWikiValidator.BaseComponents
{
    public interface IArticleValidator
    {
        public string CategoryName { get; }

        public IEnumerable<IValidationRule> Rules { get; }

        ArticleValidationResult Validate(string title, string content);
    }
}