using System.Text.RegularExpressions;
using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared
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
                return result;
            }

            var addNewlines = template.Properties.Count > 3;
            var correctTemplateString = this.templateBuilder.Build(template, addNewlines);

            var templateDirectory = Path.Combine("templates", template.Name);

            title = title.Replace("/", string.Empty);

            var contentToValidate = MakeNewlinesConsistent(content);

            if (!contentToValidate.Contains(correctTemplateString))
            {
                result.AddIssue($"Incorrect or missing {template.Name} template");

                if (!Directory.Exists(templateDirectory))
                {
                    Directory.CreateDirectory(templateDirectory);
                }

                File.WriteAllText(Path.Combine(templateDirectory, $"{title}.txt"), correctTemplateString);
            }

            return result;
        }

        protected abstract WikiTemplate PrepareTemplate(ArticleData data);

        private static string MakeNewlinesConsistent(string input)
        {
            return Regex.Replace(input, @"\r\n|\r|\n", Environment.NewLine);
        }
    }
}
