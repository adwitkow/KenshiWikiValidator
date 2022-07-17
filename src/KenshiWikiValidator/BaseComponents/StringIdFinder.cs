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

using KenshiWikiValidator.OcsProxy;

namespace KenshiWikiValidator.BaseComponents
{
    public class StringIdFinder
    {
        private readonly IItemRepository itemRepository;
        private readonly IWikiTitleCache wikiTitleCache;
        private readonly Dictionary<string, string> categoryToTemplateMap;

        public StringIdFinder(IItemRepository itemRepository, IWikiTitleCache wikiTitleCache)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
            this.categoryToTemplateMap = CreateCategoryToTemplateMap();
        }

        public void PopulateStringIds(string title, ArticleData data, string category)
        {
            var validTemplates = data.WikiTemplates
                .Where(template => this.categoryToTemplateMap[category].Equals(template.Name));
            var matchingItems = this.GetMatchingItems(title);

            if (!validTemplates.Any())
            {
                if (matchingItems.Count == 1)
                {
                    var matchingItem = matchingItems.Single();
                    data.PotentialStringId = matchingItem.StringId;
                }

                return;
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
                matchingItems = this.CheckStringIds(title, data, matchingItems, stringIdValue);
            }

            if (matchingItems.Count == 1)
            {
                var matchingItem = matchingItems.Single();
                data.PotentialStringId = matchingItem.StringId;
            }
        }

        private static Dictionary<string, string> CreateCategoryToTemplateMap()
        {
            return new Dictionary<string, string>()
            {
                { "Weapons", "Weapon" },
                { "Armour", "Armour" },
                { "Town Residents", "Traders" },
                { "Locations", "Town" },
            };
        }

        private List<IItem> CheckStringIds(string title, ArticleData data, List<IItem> matchingItems, string stringIdValue)
        {
            var stringIds = stringIdValue.Split(',')
                .Select(id => id.Trim());

            if (!matchingItems.Any())
            {
                matchingItems = stringIds.Select(id => this.itemRepository.GetItemByStringId(id)).ToList();
            }

            foreach (var stringId in stringIds)
            {
                var matchingItem = matchingItems.FirstOrDefault(item => item.StringId == stringId);

                if (matchingItem is not null)
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
            return this.itemRepository.GetItems()
                .Where(item => name.ToLower().Trim().Equals(item.Name.ToLower().Trim()))
                .ToList();
        }
    }
}
