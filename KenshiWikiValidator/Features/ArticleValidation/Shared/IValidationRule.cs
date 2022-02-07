namespace KenshiWikiValidator.Features.ArticleValidation.Shared
{
    public interface IValidationRule
    {
        RuleResult Execute(string title, string content, ArticleData data);
    }
}
