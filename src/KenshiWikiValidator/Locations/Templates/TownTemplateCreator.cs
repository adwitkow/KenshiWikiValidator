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
using KenshiWikiValidator.BaseComponents.Creators;
using KenshiWikiValidator.Locations;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.Locations.Templates
{
    public class TownTemplateCreator : ITemplateCreator
    {
        private const string WikiTemplateName = "Town";

        private readonly IItemRepository itemRepository;
        private readonly IZoneDataProvider zoneDataProvider;
        private readonly IWikiTitleCache wikiTitles;
        private readonly string[] townTypes;

        public TownTemplateCreator(IItemRepository itemRepository, IZoneDataProvider zoneDataProvider, IWikiTitleCache wikiTitles)
        {
            this.itemRepository = itemRepository;
            this.zoneDataProvider = zoneDataProvider;
            this.wikiTitles = wikiTitles;

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

        public WikiTemplate? Generate(ArticleData data)
        {
            var stringIds = data.GetAllPossibleStringIds();

            if (!stringIds.Any())
            {
                return null;
            }

            var items = stringIds.Select(stringId => this.itemRepository.GetItemByStringId<Town>(stringId));
            var articleTitle = this.wikiTitles.GetTitle(stringIds.First(), items.First().Name);
            var baseArticleTitle = articleTitle.Split('/').First();

            var factions = items
                .SelectMany(item => item.Factions
                    .Select(factionRef => $"[[{factionRef.Item.Name}]]"))
                .Distinct();

            var zones = this.ExtractZones(items, baseArticleTitle)
                .Distinct();

            var regions = string.Join(", ", zones);
            var fcsNames = items
                .Select(item => item.Name)
                .Distinct();

            if (fcsNames.Count() == 1 && fcsNames.Single().Equals(articleTitle))
            {
                fcsNames = Enumerable.Empty<string>();
            }

            var existingTemplate = data.WikiTemplates
                .SingleOrDefault(template => template.Name.ToLower().Equals("town"));

            var properties = new SortedList<string, string?>
            {
                { "string id", string.Join(", ", stringIds) },
                { "fcs_name", string.Join(", ", fcsNames) },
                { "type", this.townTypes[items.Min(item => item.TownType.GetValueOrDefault())] },
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

        private IEnumerable<string> ExtractZones(IEnumerable<Town> items, string baseArticleTitle)
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

            var baseTowns = items.SelectMany(item => item.BaseTowns)
                .Distinct();

            if (baseTowns.Count() > 1)
            {
                return Enumerable.Empty<string>();
            }

            if (!baseTowns.Any())
            {
                baseTowns = items;
            }

            zones = this.zoneDataProvider.GetZones(baseTowns.Single().Name);

            return zones;
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