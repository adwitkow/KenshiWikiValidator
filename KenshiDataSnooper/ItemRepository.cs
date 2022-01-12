using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper
{
    public class ItemRepository
    {
        private HashSet<DataItem> items;
        private Dictionary<string, DataItem> lookup;

        public ItemRepository()
        {
            this.items = new HashSet<DataItem>();
            this.lookup = new Dictionary<string, DataItem>();
        }

        public string? GameDirectory { get; private set; }

        public IEnumerable<DataItem> GetItems()
        {
            return this.items;
        }

        public IEnumerable<DataItem> GetItemsByType(ItemType type)
        {
            return this.items.Where(item => item.Type == type);
        }

        public IEnumerable<DataItem> GetItemsByTypes(params ItemType[] types)
        {
            return this.items.Where(item => types.Contains(item.Type));
        }

        public DataItem GetByStringId(string id)
        {
            return this.lookup[id];
        }

        public IEnumerable<DataItem> GetReferencingItemsFor(DataItem reference)
        {
            return this.items.Where(item => item.IsReferencing(reference.StringId));
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

            var builtItems = OcsDataContextBuilder.Default.Build(options).Items.Values.ToList();

            this.GameDirectory = installation.Game;

            this.lookup = builtItems.ToDictionary(item => item.StringId, item => item);
            this.items = new HashSet<DataItem>(builtItems);
        }
    }
}
