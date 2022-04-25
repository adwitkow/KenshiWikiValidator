using System.Text.RegularExpressions;
using KenshiWikiValidator.WikiTemplates;

namespace KenshiWikiValidator.BaseComponents
{
    public abstract class ArticleValidatorBase : IArticleValidator
    {
        private static readonly Regex CategoryRegex = new Regex(@"\[\[Category:(?<name>.*?)(\|#)?]]");

        public abstract string CategoryName { get; }

        public abstract IEnumerable<IValidationRule> Rules { get; }

        public ArticleValidationResult Validate(string title, string content)
        {
            var result = new ArticleValidationResult();
            var results = new List<RuleResult>();

            var categories = new List<string>();
            foreach (Match match in CategoryRegex.Matches(content))
            {
                var category = match.Groups["name"].Value;
                categories.Add(category);
            }

            var data = new ArticleData
            {
                WikiTemplates = this.ParseTemplates(content),
                Categories = categories,
            };

            foreach (IValidationRule? rule in this.Rules)
            {
                results.Add(rule.Execute(title, content, data));
            }

            var success = !results.Any(result => !result.Success);

            if (!success)
            {
                var issues = results.SelectMany(result => result.Issues);
                result.Issues = issues;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{title} is OK!");
                Console.ResetColor();
            }

            return result;
        }

        public IEnumerable<WikiTemplate> ParseTemplates(string content)
        {
            var parser = new TemplateParser();
            var templates = new List<WikiTemplate>();

            var startingIndex = content.IndexOf("{{");
            var endingIndex = content.IndexOf("}}");

            while (startingIndex != -1 && endingIndex != -1)
            {
                var body = content.Substring(startingIndex, endingIndex - startingIndex + 2);
                templates.Add(parser.Parse(body));

                startingIndex = content.IndexOf("{{", endingIndex);
                if (startingIndex != -1)
                {
                    endingIndex = content.IndexOf("}}", startingIndex);
                }
            }

            return templates;
        }
    }
}
