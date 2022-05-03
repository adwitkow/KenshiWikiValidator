using KenshiWikiValidator.OcsProxy.Models;
using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public class ItemRepository : IItemRepository
    {
        private readonly Dictionary<string, ICollection<DataItem>> referenceCache;

        private Dictionary<string, DataItem> dataItemLookup;
        private readonly Dictionary<string, IItem> itemLookup;
        private readonly Dictionary<Type, IEnumerable<IItem>> itemsByType;

        public ItemRepository()
        {
            this.dataItemLookup = new Dictionary<string, DataItem>();
            this.itemLookup = new Dictionary<string, IItem>();
            this.itemsByType = new Dictionary<Type, IEnumerable<IItem>>();

            this.referenceCache = new Dictionary<string, ICollection<DataItem>>();
        }

        public string? GameDirectory { get; private set; }

        public IEnumerable<DataItem> GetDataItems()
        {
            return this.dataItemLookup.Values;
        }

        public IEnumerable<IItem> GetItems()
        {
            return this.itemLookup.Values;
        }

        public IEnumerable<T> GetItems<T>() where T : IItem
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

        public IEnumerable<DataItem> GetDataItemsByType(ItemType type)
        {
            return this.GetDataItems().Where(item => item.Type == type);
        }

        public IEnumerable<DataItem> GetDataItemsByTypes(params ItemType[] types)
        {
            return this.GetDataItems().Where(item => types.Contains(item.Type));
        }

        public DataItem GetDataItemByStringId(string id)
        {
            return this.dataItemLookup[id];
        }

        public IItem GetItemByStringId(string id)
        {
            return this.itemLookup[id];
        }

        public IEnumerable<DataItem> GetReferencingDataItemsFor(string itemId)
        {
            var isItemCached = this.referenceCache.TryGetValue(itemId, out var cached);

            if (isItemCached)
            {
                return cached!;
            }
            else
            {
                return Enumerable.Empty<DataItem>();
            }
        }

        public IEnumerable<DataItem> GetReferencingDataItemsFor(DataItem reference)
        {
            return this.GetReferencingDataItemsFor(reference.StringId);
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

            foreach (var item in contextItems)
            {
                foreach (var targetId in item.ReferenceCategories
                    .SelectMany(cat => cat.Value)
                    .Select(pair => pair.Value.TargetId))
                {
                    if (this.referenceCache.ContainsKey(targetId))
                    {
                        this.referenceCache[targetId].Add(item);
                    }
                    else
                    {
                        this.referenceCache.Add(targetId, new List<DataItem>() { item });
                    }
                }
            }

            this.GameDirectory = installation.Game;

            this.dataItemLookup = contextItems.ToDictionary(item => item.StringId, item => item);

            var modelConverter = new ItemModelConverter(this);
            var convertedItems = modelConverter.Convert(contextItems).ToArray();

            // First, we add all the newly created (not yet mapped) items to the lookup dictionary
            foreach (var (Base, Result) in convertedItems)
            {
                this.itemLookup[Base.StringId] = Result;
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
        }
    }
}
