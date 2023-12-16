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
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.MapItems.Rules
{
    internal class MapItemLootSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly IWikiTitleCache wikiTitleCache;

        public MapItemLootSectionRule(IItemRepository itemRepository, IWikiTitleCache wikiTitleCache)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
        }

        protected override WikiSectionBuilder? CreateSectionBuilder(ArticleData data)
        {
            var stringId = data.GetAllPossibleStringIds().First();
            var mapItem = this.itemRepository.GetItemByStringId<MapItem>(stringId);

            var vendors = this.itemRepository.GetItems<VendorList>()
                .Where(vendor => vendor.Maps.ContainsItem(mapItem))
                .DistinctBy(vendor => vendor.StringId)
                .ToList();
            var squadsToTownsMap = this.itemRepository.GetItems<Squad>()
                .Where(squad => squad.SpecialMapItems.ContainsItem(mapItem) || squad.Vendors.ContainsAny(vendors))
                .DistinctBy(squad => squad.StringId)
                .ToDictionary(squad => squad, squad => this.GetSquadTowns(squad));

            var locations = new List<string>();
            foreach (var pair in squadsToTownsMap)
            {
                var squad = pair.Key;
                var towns = pair.Value;

                try
                {
                    var townLinks = towns.Select(town => $"[[{this.wikiTitleCache[town]}]]")
                        .OrderBy(name => name)
                        .Distinct();

                    if (towns.Count() == 1 || !squad.IsShop)
                    {
                        locations.AddRange(townLinks);
                    }
                    else
                    {
                        var vendorLink = $"[[{this.wikiTitleCache[squad]}]]";
                        locations.Add($"{vendorLink} ({string.Join(", ", townLinks)})");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return new WikiSectionBuilder()
                .WithHeader("Possible loot")
                .WithUnorderedList(locations);
        }

        private IEnumerable<Town> GetSquadTowns(Squad squad)
        {
            var unusedLocations = new[] { "58704-rebirth.mod" };
            return this.itemRepository.GetItems<Town>()
                .Where(town => !unusedLocations.Contains(town.StringId) && town.Residents.ContainsItem(squad))
                .DistinctBy(town => town.StringId);
        }
    }
}
