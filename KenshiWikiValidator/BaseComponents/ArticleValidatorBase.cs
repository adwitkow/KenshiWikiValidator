// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

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
