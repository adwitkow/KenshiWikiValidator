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
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.TownResidents.Rules
{
    internal class StockSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;

        public StockSectionRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        protected override WikiSectionBuilder? CreateSectionBuilder(ArticleData data)
        {
            var stringId = data.GetAllPossibleStringIds().FirstOrDefault();

            if (string.IsNullOrEmpty(stringId))
            {
                return null;
            }

            var squad = this.itemRepository.GetItemByStringId<Squad>(stringId);

            var builder = new WikiSectionBuilder()
                .WithHeader("Stock");

            if (squad.GearArtifactsBaseValue != 0 || squad.ItemArtifactsBaseValue != 0)
            {
                builder.WithParagraph("''Randomly chosen [[artifacts]] are possible to spawn at this place - thus, the list below may be incomplete.''");
            }

            var vendorsWithWeights = squad.Vendors
                .Select(reference => (Vendor: reference.Item, Weight: reference.Value0));
            var itemCategories = CollectItemCategories(vendorsWithWeights);

            foreach (var category in itemCategories)
            {
                var itemType = category.Key;
                var items = category.Value
                    .OrderByDescending(item => item.Weight)
                    .ThenBy(item => item.Name)
                    .Select(item => $"{item.Name} ({item.Weight})");
                builder.WithSubsection(itemType.ToString(), 1)
                    .WithUnorderedList(items)
                    .WithLine(string.Empty);
            }

            return builder;
        }

        private static Dictionary<ItemType, List<IStockItem>> CollectItemCategories(IEnumerable<(VendorList Vendor, int Weight)> vendorsWithWeights)
        {
            var itemCategories = new Dictionary<ItemType, List<IStockItem>>();
            foreach (var vendorWithWeight in vendorsWithWeights)
            {
                var vendor = vendorWithWeight.Vendor;
                var vendorWeight = vendorWithWeight.Weight;

                foreach (var reference in vendor.AllReferences)
                {
                    var item = reference.Item;
                    var weight = reference.Value0;
                    var itemType = item.Type;

                    var totalWeight = ;
                    var typeExists = itemCategories.TryGetValue(itemType, out var itemList);

                    IStockItem stockItem;
                    switch (itemType)
                    {
                        case ItemType.Armour:
                        case ItemType.Weapon:
                        case ItemType.LimbReplacement:
                        case ItemType.Crossbow:
                            stockItem = new GradedStockItem()
                            {
                                Name = item.Name,
                                Weight = totalWeight,
                                Grades = new int[] { },
                            };
                            break;
                        default:
                            stockItem = new StockItem()
                            {
                                Name = item.Name,
                                Weight = totalWeight,
                            };
                            break;
                    }

                    if (typeExists)
                    {
                        itemList!.Add(stockItem);
                    }
                    else
                    {
                        itemList = new List<IStockItem> { stockItem };
                        itemCategories.Add(itemType, itemList);
                    }
                }
            }

            return itemCategories;
        }

        private interface IStockItem
        {
            string Name { get; init; }

            int Weight { get; init; }
        }

        private readonly struct StockItem : IStockItem
        {
            public string Name { get; init; }

            public int Weight { get; init; }
        }

        private readonly struct GradedStockItem : IStockItem
        {
            public string Name { get; init; }

            public int Weight { get; init; }

            public int[] Grades { get; init; }
        }
    }
}
