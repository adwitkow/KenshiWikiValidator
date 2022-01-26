namespace KenshiWikiValidator.Features.ArticleValidation
{
    public class ArticleValidationResult
    {
        public ArticleValidationResult()
        {
            Issues = new List<string>();
        }

        public bool Success => !Issues.Any();

        public IEnumerable<string> Issues { get; set; }
    }
}