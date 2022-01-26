namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public interface IValidationRule
    {
        RuleResult Execute(string content);
    }
}
