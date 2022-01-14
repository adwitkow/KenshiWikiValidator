using KenshiDataSnooper.Models;
using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper
{
    public class ItemRepository
    {
        private readonly ItemBuilder itemBuilder;

        private readonly Dictionary<string, IEnumerable<DataItem>> referenceCache;

        private HashSet<DataItem> dataItems;
        private Dictionary<string, DataItem> dataItemLookup;

        private HashSet<IItem> items;
        private Dictionary<string, IItem> itemLookup;

        public ItemRepository()
        {
            this.dataItems = new HashSet<DataItem>();
            this.dataItemLookup = new Dictionary<string, DataItem>();
            this.items = new HashSet<IItem>();
            this.itemLookup = new Dictionary<string, IItem>();

            this.referenceCache = new Dictionary<string, IEnumerable<DataItem>>();

            this.itemBuilder = new ItemBuilder(this);
        }

        public string? GameDirectory { get; private set; }

        public IEnumerable<DataItem> GetDataItems()
        {
            return this.dataItems;
        }

        public IEnumerable<IItem> GetItems()
        {
            return this.items;
        }

        public IEnumerable<DataItem> GetDataItemsByType(ItemType type)
        {
            return this.dataItems.Where(item => item.Type == type);
        }

        public IEnumerable<DataItem> GetDataItemsByTypes(params ItemType[] types)
        {
            return this.dataItems.Where(item => types.Contains(item.Type));
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

            var results = this.dataItems.Where(item => item.IsReferencing(itemId));

            this.referenceCache.Add(itemId, results);

            return results;
        }

        public IEnumerable<DataItem> GetReferencingDataItemsFor(DataItem reference)
        {
            return this.GetReferencingDataItemsFor(reference.StringId);
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

            this.GameDirectory = installation.Game;

            this.dataItemLookup = contextItems.ToDictionary(item => item.StringId, item => item);
            this.dataItems = new HashSet<DataItem>(contextItems);

            var builtItems = this.itemBuilder.BuildItems();
            this.itemLookup = builtItems.ToDictionary(item => item.StringId!, item => item);
            this.items = new HashSet<IItem>(builtItems);
        }
    }
}
