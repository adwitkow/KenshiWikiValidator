using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemBuilder itemBuilder;

        private readonly Dictionary<string, ICollection<DataItem>> referenceCache;

        private HashSet<DataItem> dataItems;
        private Dictionary<string, DataItem> dataItemLookup;

        private HashSet<IItem> items;
        private Dictionary<string, IItem> itemLookup;

        public ItemRepository()
        {
            dataItems = new HashSet<DataItem>();
            dataItemLookup = new Dictionary<string, DataItem>();
            items = new HashSet<IItem>();
            itemLookup = new Dictionary<string, IItem>();

            referenceCache = new Dictionary<string, ICollection<DataItem>>();

            itemBuilder = new ItemBuilder(this);
        }

        public string? GameDirectory { get; private set; }

        public IEnumerable<DataItem> GetDataItems()
        {
            return dataItems;
        }

        public IEnumerable<IItem> GetItems()
        {
            return items;
        }

        public IEnumerable<DataItem> GetDataItemsByType(ItemType type)
        {
            return dataItems.Where(item => item.Type == type);
        }

        public IEnumerable<DataItem> GetDataItemsByTypes(params ItemType[] types)
        {
            return dataItems.Where(item => types.Contains(item.Type));
        }

        public DataItem GetDataItemByStringId(string id)
        {
            return dataItemLookup[id];
        }

        public IItem GetItemByStringId(string id)
        {
            return itemLookup[id];
        }

        public IEnumerable<DataItem> GetReferencingDataItemsFor(string itemId)
        {
            var isItemCached = referenceCache.TryGetValue(itemId, out var cached);

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
            return GetReferencingDataItemsFor(reference.StringId);
        }

        public void Load()
        {
            var installations = OcsDiscoveryService.Default.DiscoverAllInstallations();
            var installation = installations.Values.First();
            var baseMods = installation.Data.Mods;

            var options = new OcsDataContexOptions(
                Name: Guid.NewGuid().ToString(),
                Installation: installation,
                LoadGameFiles: ModLoadType.Base,
                LoadEnabledMods: ModLoadType.Base,
                ThrowIfMissing: false);

            var contextItems = OcsDataContextBuilder.Default.Build(options).Items.Values.ToList();

            foreach (var item in contextItems)
            {
                foreach (var reference in item.ReferenceCategories
                    .SelectMany(cat => cat.Value)
                    .Select(pair => pair.Value))
                {
                    if (referenceCache.ContainsKey(reference.TargetId))
                    {
                        referenceCache[reference.TargetId].Add(item);
                    }
                    else
                    {
                        referenceCache.Add(reference.TargetId, new List<DataItem>() { item });
                    }
                }
            }

            GameDirectory = installation.Game;

            dataItemLookup = contextItems.ToDictionary(item => item.StringId, item => item);
            dataItems = new HashSet<DataItem>(contextItems);

            var builtItems = itemBuilder.BuildItems();
            itemLookup = builtItems.ToDictionary(item => item.StringId!, item => item);
            items = new HashSet<IItem>(builtItems);
        }
    }
}
