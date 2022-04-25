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
