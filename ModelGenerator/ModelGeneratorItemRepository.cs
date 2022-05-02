using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy
{
    public class ModelGeneratorItemRepository
    {
        private readonly Dictionary<string, ICollection<DataItem>> referenceCache;

        private Dictionary<string, DataItem> dataItemLookup;

        public ModelGeneratorItemRepository()
        {
            this.dataItemLookup = new Dictionary<string, DataItem>();
            this.referenceCache = new Dictionary<string, ICollection<DataItem>>();
        }

        public string? GameDirectory { get; private set; }

        public IEnumerable<DataItem> GetDataItems()
        {
            return this.dataItemLookup.Values;
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
                    if (this.referenceCache.ContainsKey(reference.TargetId))
                    {
                        this.referenceCache[reference.TargetId].Add(item);
                    }
                    else
                    {
                        this.referenceCache.Add(reference.TargetId, new List<DataItem>() { item });
                    }
                }
            }

            this.GameDirectory = installation.Game;

            this.dataItemLookup = contextItems.ToDictionary(item => item.StringId, item => item);
        }
    }
}
