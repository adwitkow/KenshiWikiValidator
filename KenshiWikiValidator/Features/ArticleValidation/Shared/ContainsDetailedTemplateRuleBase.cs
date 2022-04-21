using System.Text.RegularExpressions;
using KenshiWikiValidator.Features.WikiTemplates;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared
{
    public abstract class ContainsDetailedTemplateRuleBase : IValidationRule
    {
        private readonly TemplateBuilder templateBuilder;

        public ContainsDetailedTemplateRuleBase()
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

            var addNewlines = template.Parameters.Count > 3;
            var correctTemplateString = this.templateBuilder.Build(template, addNewlines);

            var output = "output";

            title = title.Replace("/", string.Empty);

            var contentToValidate = MakeNewlinesConsistent(content);

            if (!contentToValidate.Contains(correctTemplateString))
            {
                result.AddIssue($"Incorrect or missing {template.Name} template");

                if (!Directory.Exists(output))
                {
                    Directory.CreateDirectory(output);
                }

                File.WriteAllText(Path.Combine(output, $"{title}-{template.Name.Replace("/", string.Empty)}-Template.txt"), correctTemplateString);
            }

            return result;
        }

        protected abstract WikiTemplate? PrepareTemplate(ArticleData data);

        private static string MakeNewlinesConsistent(string input)
        {
            return Regex.Replace(input, @"\r\n|\r|\n", Environment.NewLine);
        }
    }
}
