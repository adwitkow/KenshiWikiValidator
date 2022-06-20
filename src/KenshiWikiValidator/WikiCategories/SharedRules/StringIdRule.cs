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
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.WikiTemplates;

namespace KenshiWikiValidator.WikiCategories.SharedRules
{
    public class StringIdRule : IValidationRule
    {
        private static readonly string[] ValidTemplateNames = { "Weapon", "Armour", "Traders", "Town" };

        private readonly IWikiTitleCache wikiTitleCache;

        public StringIdRule(IItemRepository itemRepository, IWikiTitleCache wikiTitleCache)
        {
            this.ItemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
        }

        protected IItemRepository ItemRepository { get; }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var validTemplates = data.WikiTemplates
                .Where(template => ValidTemplateNames
                    .Any(valid => template.Name.Equals(valid)));
            var matchingItems = this.GetMatchingItems(title);

            if (!validTemplates.Any())
            {
                if (matchingItems.Count == 1)
                {
                    var matchingItem = matchingItems.Single();
                    data.PotentialStringId = matchingItem.StringId;
                }

                return result;
            }

            var fcsNameValue = this.SelectSingleParameter(validTemplates, "fcs_name");

            if (!string.IsNullOrEmpty(fcsNameValue))
            {
                matchingItems.Clear();
                var fcsNames = fcsNameValue.Split(',').Select(name => name.Trim());

                foreach (var fcsName in fcsNames)
                {
                    matchingItems.AddRange(this.GetMatchingItems(fcsName));
                }
            }

            var stringIdValue = this.SelectSingleParameter(validTemplates, "string id");
            if (!string.IsNullOrEmpty(stringIdValue))
            {
                matchingItems = this.CheckStringIds(title, data, result, matchingItems, stringIdValue);
            }
            else
            {
                result.AddIssue($"No string id! Most likely string id: [{string.Join(", ", matchingItems.Select(item => $"string id = {item.StringId}|"))}]");
            }

            if (matchingItems.Count == 1)
            {
                var matchingItem = matchingItems.Single();
                data.PotentialStringId = matchingItem.StringId;
            }

            return result;
        }

        protected virtual IEnumerable<IItem> GetRelevantItems()
        {
            return this.ItemRepository.GetItems();
        }

        private List<IItem> CheckStringIds(string title, ArticleData data, RuleResult result, List<IItem> matchingItems, string stringIdValue)
        {
            var stringIds = stringIdValue.Split(',')
                .Select(id => id.Trim());

            if (!matchingItems.Any())
            {
                matchingItems = stringIds.Select(id => this.ItemRepository.GetItemByStringId(id)).ToList();
            }

            foreach (var stringId in stringIds)
            {
                var matchingItem = matchingItems.FirstOrDefault(item => item.StringId == stringId);

                if (matchingItem is null && matchingItems.Any())
                {
                    result.AddIssue($"String id '{stringId}' is incorrect in the article. Should be corrected to one of the following: [{string.Join(", ", matchingItems.Select(item => item.StringId))}]");
                }
                else
                {
                    data.StringIds.Add(stringId);
                    this.wikiTitleCache.AddTitle(stringId, title);
                }
            }

            return matchingItems;
        }

        private string? SelectSingleParameter(IEnumerable<WikiTemplate> validTemplates, string parameterName)
        {
            return validTemplates.Where(template => template.Parameters.ContainsKey(parameterName))
                .Select(template => template.Parameters[parameterName])
                .Distinct()
                .SingleOrDefault();
        }

        private List<IItem> GetMatchingItems(string name)
        {
            IEnumerable<IItem> items = this.GetRelevantItems();
            return items
                .Where(item => name.ToLower().Trim().Equals(item.Name.ToLower().Trim()))
                .ToList();
        }
    }
}
