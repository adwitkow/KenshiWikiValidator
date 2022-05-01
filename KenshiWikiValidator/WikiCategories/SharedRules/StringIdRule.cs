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
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.WikiCategories.SharedRules
{
    public class StringIdRule : IValidationRule
    {
        private static readonly string[] ValidTemplateNames = { "Weapon", "Armour", "Traders", "Town" };

        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitleCache;
        private readonly bool shouldCheckFcsName;
        private readonly ItemType? itemType;

        public StringIdRule(IItemRepository itemRepository, WikiTitleCache wikiTitleCache, bool shouldCheckFcsName = false, ItemType? itemType = null)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
            this.shouldCheckFcsName = shouldCheckFcsName;
            this.itemType = itemType;
        }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();

            var validTemplates = data.WikiTemplates.Where(template => ValidTemplateNames.Any(valid => template.Name.Equals(valid)));
            if (!validTemplates.Any())
            {
                return result;
            }

            var matchingItems = this.GetMatchingItems(title);
            var fcsNameValue = this.SelectSingleParameter(validTemplates, "fcs_name");

            if (string.IsNullOrEmpty(fcsNameValue))
            {
                var possibleFcsNames = matchingItems.Select(item => item.Name.ToLower().Trim());

                if (this.shouldCheckFcsName && possibleFcsNames.Any(name => !name.Equals(title.ToLower().Trim())))
                {
                    result.AddIssue($"FCS name is missing! Possible FCS names: {string.Join(", ", possibleFcsNames)}");
                }
            }
            else
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
                var stringIds = stringIdValue.Split(',')
                    .Select(id => id.Trim());

                if (!matchingItems.Any())
                {
                    matchingItems = stringIds.Select(id => this.itemRepository.GetDataItemByStringId(id)).ToList();
                }

                foreach (var stringId in stringIds)
                {
                    var matchingItem = matchingItems.FirstOrDefault(item => item.StringId == stringId);

                    if (matchingItem is null)
                    {
                        if (matchingItems.Any())
                        {
                            result.AddIssue($"String id '{stringId}' is incorrect in the article. Should be corrected to one of the following: [{string.Join(", ", matchingItems.Select(item => item.StringId))}]");
                        }
                        else
                        {
                            result.AddIssue($"String id '{stringId}' could not be found in the game files.");
                        }
                    }
                    else
                    {
                        data.StringIds.Add(stringId);
                        this.wikiTitleCache.AddTitle(stringId, title);
                    }
                }
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

        private string? SelectSingleParameter(IEnumerable<WikiTemplate> validTemplates, string parameterName)
        {
            return validTemplates.Where(template => template.Parameters.ContainsKey(parameterName))
                .Select(template => template.Parameters[parameterName])
                .Distinct()
                .SingleOrDefault();
        }

        private List<DataItem> GetMatchingItems(string name)
        {
            IEnumerable<DataItem> items;
            if (this.itemType is null)
            {
                items = this.itemRepository.GetDataItems();
            }
            else
            {
                items = this.itemRepository.GetDataItemsByType(this.itemType.Value);
            }

            return items
                .Where(item => name.ToLower().Trim().Equals(item.Name.ToLower().Trim()))
                .ToList();
        }
    }
}
