namespace KenshiWikiValidator.Features.ArticleValidation
{
    public class RuleResult
    {
        public RuleResult()
        {
            this.Issues = new List<string>();
            this.Success = true;
        }

        public bool Success { get; private set; }

        public ICollection<string> Issues { get; private set; }

        public void AddIssue(string issueMessage)
        {
            this.Issues.Add(issueMessage);
            this.Success = false;
        }
    }
}