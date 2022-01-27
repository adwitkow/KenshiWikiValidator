using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Validators.Rules
{
    public abstract class ContainsTemplateRuleBase : IValidationRule
    {
        private readonly TemplateBuilder templateBuilder;

        public ContainsTemplateRuleBase()
        {
            this.templateBuilder = new TemplateBuilder();
        }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var template = this.PrepareTemplate(data);

            if (template is null)
            {
                result.AddIssue($"Cannot check the templates since string id is not available in the article.");
                return result;
            }

            var correctBlueprintString = this.templateBuilder.Build(template);

            var templateDirectory = Path.Combine("templates", template.Name);
            if (!Directory.Exists(templateDirectory))
            {
                Directory.CreateDirectory(templateDirectory);
            }

            File.WriteAllText(Path.Combine(templateDirectory, $"{title}.txt"), correctBlueprintString);

            if (!content.Contains(correctBlueprintString))
            {
                result.AddIssue($"Incorrect or missing {template.Name} template");
            }

            return result;
        }

        protected abstract WikiTemplate PrepareTemplate(ArticleData data);
    }
}
