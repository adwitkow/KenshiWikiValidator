using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Features.ArticleValidation.Shared.Rules
{
    public class ContainsTemplateRule : IValidationRule
    {
        public ContainsTemplateRule(string templateName)
        {
            this.TemplateName = templateName;
        }

        public string TemplateName { get; }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            if (!data.WikiTemplates.Any(template => template.Name.Equals(this.TemplateName)))
            {
                result.AddIssue($"Article does not contain template of name '{this.TemplateName}'");
            }

            return result;
        }
    }
}
