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

using KenshiWikiValidator.OcsProxy.Models;
using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public class ItemRepository : IItemRepository
    {
        private readonly Dictionary<string, IItem> itemLookup;
        private readonly Dictionary<Type, IEnumerable<IItem>> itemsByType;

        public ItemRepository()
        {
            this.itemLookup = new Dictionary<string, IItem>();
            this.itemsByType = new Dictionary<Type, IEnumerable<IItem>>();
        }

        public string? GameDirectory { get; private set; }

        public IEnumerable<IItem> GetItems()
        {
            return this.itemLookup.Values;
        }

        public IEnumerable<T> GetItems<T>()
            where T : IItem
        {
            var success = this.itemsByType.TryGetValue(typeof(T), out var items);

            if (success)
            {
                return (IEnumerable<T>)items!;
            }
            else
            {
                var filtered = this.GetItems().OfType<T>().ToList();
                this.itemsByType.Add(typeof(T), (IEnumerable<IItem>)filtered);

                return filtered;
            }
        }

        public IItem GetItemByStringId(string id)
        {
            return this.itemLookup[id];
        }

        public T GetItemByStringId<T>(string id)
            where T : IItem
        {
            return (T)this.GetItemByStringId(id);
        }

        public void Load()
        {
            var installations = OcsDiscoveryService.Default.DiscoverAllInstallations();
            var installation = installations.Values.First();

            var options = new OcsDataContexOptions(
                Name: Guid.NewGuid().ToString(),
                Installation: installation,
                LoadGameFiles: ModLoadType.Base,
                LoadEnabledMods: ModLoadType.None,
                ThrowIfMissing: false);

            var contextItems = OcsDataContextBuilder.Default.Build(options).Items.Values.ToList();

            this.GameDirectory = installation.Game;

            var modelConverter = new ItemModelConverter(this);
            var convertedItems = modelConverter.Convert(contextItems).ToArray();

            // First, we add all the newly created (not yet mapped) items to the lookup dictionary
            foreach (var (baseItem, result) in convertedItems)
            {
                this.itemLookup[baseItem.StringId] = result;
            }

            // And now we are able to map all the properties
            // (since we can resolve the references using the lookup dictionary)
            // and unfortunately, we have to also update the dictionary with our
            // mapped items (even though they are based on the same collection)
            // because dictionaries contain shallow copies of
            // added objects instead of their references
            foreach (var convertedPair in convertedItems)
            {
                var item = modelConverter.MapProperties(convertedPair);

                this.itemLookup[item.StringId] = item;
            }

            foreach (var item in this.GetItems<Town>())
            {
                item.BaseTowns = this.FindBaseTowns(item);
            }
        }

        private IEnumerable<Town> FindBaseTowns(Town item)
        {
            var baseItems = new List<Town>() { item };
            var previousBaseItems = baseItems;
            var allBaseTowns = this.GetItems<Town>()
                .Where(town => town.OverrideTown.Any())
                .ToList();
            while (baseItems is not null && baseItems.Any())
            {
                previousBaseItems = baseItems.ToList();

                baseItems.Clear();
                foreach (var town in allBaseTowns)
                {
                    if (town.OverrideTown.ContainsItem(item))
                    {
                        baseItems.Add(town);
                    }
                }

                baseItems = baseItems
                    .Except(new[] { item })
                    .Distinct()
                    .ToList();

                if (previousBaseItems.All(baseItems.Contains))
                {
                    break;
                }
            }

            return previousBaseItems;
        }
    }
}
