using KenshiWikiValidator.Features.DataItemConversion.Models;
using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;
using OpenConstructionSet.Mods.Context;

namespace KenshiWikiValidator.Features.DataItemConversion
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemBuilder itemBuilder;

        private readonly Dictionary<string, ICollection<IItem>> referenceCache;

        private Dictionary<string, IItem> dataItemLookup;
        private Dictionary<string, IDataItem> itemLookup;

        public ItemRepository()
        {
            this.dataItemLookup = new Dictionary<string, IItem>();
            this.itemLookup = new Dictionary<string, IDataItem>();

            this.referenceCache = new Dictionary<string, ICollection<IItem>>();

            this.itemBuilder = new ItemBuilder(this);
        }

        public IEnumerable<IItem> GetDataItems()
        {
            return this.dataItemLookup.Values;
        }

        public IEnumerable<IDataItem> GetItems()
        {
            return this.itemLookup.Values;
        }

        public IEnumerable<IItem> GetDataItemsByType(ItemType type)
        {
            return this.GetDataItems().Where(item => item.Type == type);
        }

        public IEnumerable<IItem> GetDataItemsByTypes(params ItemType[] types)
        {
            return this.GetDataItems().Where(item => types.Contains(item.Type));
        }

        public IItem GetDataItemByStringId(string id)
        {
            return this.dataItemLookup[id];
        }

        public IDataItem GetItemByStringId(string id)
        {
            return this.itemLookup[id];
        }

        public IEnumerable<IItem> GetReferencingDataItemsFor(string itemId)
        {
            var isItemCached = this.referenceCache.TryGetValue(itemId, out var cached);

            if (isItemCached)
            {
                return cached!;
            }
            else
            {
                return Enumerable.Empty<IItem>();
            }
        }

        public IEnumerable<IItem> GetReferencingDataItemsFor(IItem reference)
        {
            return this.GetReferencingDataItemsFor(reference.StringId);
        }

        public async Task Load()
        {
            var installations = await new InstallationService().DiscoverAllInstallationsAsync().ToDictionaryAsync(i => i.Identifier);
            var installation = installations.Values.First();

            var options = new ModContextOptions(
                Guid.NewGuid().ToString(),
                installation,
                loadGameFiles: ModLoadType.Base,
                loadEnabledMods: ModLoadType.Base,
                throwIfMissing: false);

            var context = await new ContextBuilder().BuildAsync(options);
            var contextItems = context.Items;

            foreach (var item in contextItems)
            {
                if (item.StringId == "923-gamedata.base")
                {
                    Console.WriteLine("sztop");
                }

                foreach (var reference in item.ReferenceCategories
                    .SelectMany(cat => cat.References))
                {
                    if (this.referenceCache.ContainsKey(reference.TargetId))
                    {
                        this.referenceCache[reference.TargetId].Add(item);
                    }
                    else
                    {
                        this.referenceCache.Add(reference.TargetId, new List<IItem>() { item });
                    }
                }
            }

            this.dataItemLookup = contextItems.ToDictionary(item => item.StringId, item => (IItem)item);

            var builtItems = this.itemBuilder.BuildItems();
            this.itemLookup = builtItems.ToDictionary(item => item.StringId!, item => item);
        }
    }
}
