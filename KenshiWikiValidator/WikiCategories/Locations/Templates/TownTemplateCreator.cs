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
using KenshiWikiValidator.WikiTemplates.Creators;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.WikiCategories.Locations.Templates
{
    internal class TownTemplateCreator : ITemplateCreator
    {
        private const string WikiTemplateName = "Town";

        private readonly IItemRepository itemRepository;
        private readonly ZoneDataProvider zoneDataProvider;
        private readonly WikiTitleCache wikiTitles;
        private readonly ArticleData data;
        private readonly string[] townTypes;

        public TownTemplateCreator(IItemRepository itemRepository, ZoneDataProvider zoneDataProvider, WikiTitleCache wikiTitles, ArticleData data)
        {
            this.itemRepository = itemRepository;
            this.zoneDataProvider = zoneDataProvider;
            this.wikiTitles = wikiTitles;
            this.data = data;

            this.townTypes = new[]
            {
                "Nest",
                "Outpost",
                "Town",
                "Village",
                "Ruins",
                "Slave camp",
                "Military",
                "Prison",
                "NEST MARKER",
                "POI",
                "NULL",
            };
        }

        public WikiTemplate? Generate()
        {
            var stringIds = this.data.StringIds;
            if (!stringIds.Any())
            {
                if (string.IsNullOrEmpty(this.data.PotentialStringId))
                {
                    return null;
                }
                else
                {
                    stringIds = new[] { this.data.PotentialStringId };
                }
            }

            var items = stringIds.Select(stringId => this.itemRepository.GetDataItemByStringId(stringId));
            var articleTitle = this.wikiTitles.GetTitle(stringIds.First(), items.First().Name);
            var baseArticleTitle = articleTitle.Split('/').First();

            var existingTemplate = this.data.WikiTemplates
                .SingleOrDefault(template => template.Name.ToLower().Equals("town"));

            var factions = items
                .SelectMany(item => item.GetReferenceItems(this.itemRepository, "faction")
                    .Select(faction => $"[[{faction.Name}]]"))
                .Distinct();

            var zones = this.ExtractZones(items, baseArticleTitle);

            var regions = string.Join(", ", zones
                    .Select(region => this.ConvertRegion(region))
                .Distinct());
            var fcsNames = items
                .Select(item => item.Name)
                .Distinct();

            if (fcsNames.Count() == 1 && fcsNames.Single().Equals(articleTitle))
            {
                fcsNames = Enumerable.Empty<string>();
            }

            var properties = new SortedList<string, string?>
            {
                { "string id", string.Join(", ", stringIds) },
                { "fcs_name", string.Join(", ", fcsNames) },
                { "type", this.townTypes[items.Min(item => (int)item.Values["type"])] },
                { "biome", regions },
                { "image1", this.GetExistingParameter(existingTemplate, "image1") },
                { "caption1", this.GetExistingParameter(existingTemplate, "caption1") },
                { "map", this.GetExistingParameter(existingTemplate, "map") },
                { "caption2", this.GetExistingParameter(existingTemplate, "caption2") },
                { "factions", string.Join(", ", factions) },
            };

            if (fcsNames.Any(fcsName => !fcsName.Equals(articleTitle)))
            {
                properties.Add("title1", baseArticleTitle);
            }

            return new WikiTemplate(WikiTemplateName, properties);
        }

        private string ConvertRegion(string region)
        {
            var regionExceptions = new[] { "Bast", "Flats Lagoon", "Rebirth" };

            return regionExceptions.Contains(region) ? $"[[{region} (Zone)|{region}]]" : $"[[{region}]]";
        }

        private IEnumerable<string> ExtractZones(IEnumerable<OpenConstructionSet.Data.Models.DataItem> items, string baseArticleTitle)
        {
            var zones = this.zoneDataProvider.GetZones(baseArticleTitle);
            if (zones.Any())
            {
                return zones;
            }

            zones = this.zoneDataProvider.GetZones(items.Single().Name);
            if (zones.Any())
            {
                return zones;
            }

            var baseTowns = this.FindBaseItems(items);

            if (!baseTowns.Any())
            {
                var joinedItems = string.Join(", ", items.Select(item => $"'{item.Name}'"));
                throw new InvalidOperationException($"Cannot find any zones for base article '{baseArticleTitle}' or items '{joinedItems}'");
            }

            zones = this.zoneDataProvider.GetZones(baseTowns.Single().Name);

            return zones;
        }

        private IEnumerable<OpenConstructionSet.Data.Models.DataItem> FindBaseItems(IEnumerable<OpenConstructionSet.Data.Models.DataItem> items)
        {
            var baseItems = items;
            var previousBaseItems = items;
            while (baseItems is not null && baseItems.Any())
            {
                previousBaseItems = baseItems;
                baseItems = baseItems
                    .SelectMany(item => this.itemRepository.GetReferencingDataItemsFor(item))
                    .Where(item => item.Type == ItemType.Town)
                    .Distinct()
                    .ToList();
            }

            return previousBaseItems;
        }

        private string? GetExistingParameter(WikiTemplate? existingTemplate, string parameter)
        {
            if (existingTemplate is null)
            {
                return null;
            }

            existingTemplate.Parameters.TryGetValue(parameter, out var value);

            return value;
        }
    }
}