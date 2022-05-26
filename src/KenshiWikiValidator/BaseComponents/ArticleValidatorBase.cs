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
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiCategories.SharedRules;
using KenshiWikiValidator.WikiTemplates;

namespace KenshiWikiValidator.BaseComponents
{
    public abstract class ArticleValidatorBase : IArticleValidator
    {
        private static readonly Regex CategoryRegex = new Regex(@"\[\[Category:(?<name>.*?)(\|#)?]]");

        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitles;

        protected ArticleValidatorBase(IItemRepository itemRepository, WikiTitleCache wikiTitles)
        {
            this.ArticleDataMap = new Dictionary<string, ArticleData>();
            this.itemRepository = itemRepository;
            this.wikiTitles = wikiTitles;
        }

        public Dictionary<string, ArticleData> ArticleDataMap { get; }

        public abstract string CategoryName { get; }

        public abstract IEnumerable<IValidationRule> Rules { get; }

        public virtual IEnumerable<IArticleValidator> Dependencies => Enumerable.Empty<IArticleValidator>();

        public ArticleValidationResult Validate(string title, string content)
        {
            var result = new ArticleValidationResult();
            var results = new List<RuleResult>();

            if (!this.ArticleDataMap.ContainsKey(title))
            {
                this.CachePageData(title, content);
            }

            var data = this.ArticleDataMap[title];
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

        public void CachePageData(string title, string content)
        {
            var categories = new List<string>();
            foreach (Match match in CategoryRegex.Matches(content))
            {
                var category = match.Groups["name"].Value;
                categories.Add(category);
            }

            var articleData = new ArticleData
            {
                WikiTemplates = this.ParseTemplates(content),
                Categories = categories,
            };

            // TODO: This is a hack: I should figure out a different way
            // to process the string ids before the rules are ran.
            var stringIdRule = new StringIdRule(this.itemRepository, this.wikiTitles);
            stringIdRule.Execute(title, content, articleData);

            this.ArticleDataMap[title] = articleData;
        }

        public IEnumerable<WikiTemplate> ParseTemplates(string content)
        {
            var parser = new TemplateParser();
            return parser.ParseAllTemplates(content);
        }
    }
}
