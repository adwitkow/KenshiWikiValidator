using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy
{
    public class ModelGeneratorItemRepository
    {
        private Dictionary<string, ModItem> dataItemLookup;

        public ModelGeneratorItemRepository()
        {
            this.dataItemLookup = new Dictionary<string, ModItem>();
        }

        public IEnumerable<ModItem> GetDataItems()
        {
            return this.dataItemLookup.Values;
        }

        public ModItem GetDataItemByStringId(string id)
        {
            return this.dataItemLookup[id];
        }

        public void Load()
        {
            var provider = new ContextProvider();
            var context = provider.GetDataMiningContext();

            this.dataItemLookup = context.Items.ToDictionary(item => item.StringId, item => item);
        }
    }
}
