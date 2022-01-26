namespace KenshiWikiValidator.Features.ArticleValidation
{
    public class RuleResult
    {
        public RuleResult()
        {
            Issues = new List<string>();
            Success = true;
        }

        public bool Success { get; private set; }
        public ICollection<string> Issues { get; private set; }

        public void AddIssue(string issueMessage)
        {
            Issues.Add(issueMessage);
            Success = false;
        }
    }
}