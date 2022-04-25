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

using KenshiWikiValidator.BaseComponents;

namespace KenshiWikiValidator.WikiCategories.SharedRules
{
    public class ContainsTemplateRule : IValidationRule
    {
        public ContainsTemplateRule(string templateName, IEnumerable<string>? categoryExceptions = null)
        {
            this.TemplateName = templateName;
            this.CategoryExceptions = categoryExceptions ?? Enumerable.Empty<string>();
        }

        public string TemplateName { get; }

        public IEnumerable<string> CategoryExceptions { get; }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            if (this.AnyCategoryMatchesExceptions(data))
            {
                return result;
            }

            if (!data.WikiTemplates.Any(template => template.Name.Equals(this.TemplateName)))
            {
                result.AddIssue($"Article does not contain template of name '{this.TemplateName}'");
            }

            return result;
        }

        private bool AnyCategoryMatchesExceptions(ArticleData data)
        {
            return this.CategoryExceptions
                .Any(exception => data.Categories
                    .Any(category => category.Equals(exception, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}
