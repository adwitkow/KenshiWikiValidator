namespace KenshiWikiValidator.BaseComponents
{
    public interface IValidationRule
    {
        RuleResult Execute(string title, string content, ArticleData data);
    }
}
