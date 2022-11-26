using OpenConstructionSet;
using OpenConstructionSet.Mods;
using OpenConstructionSet.Mods.Context;

namespace KenshiWikiValidator.OcsProxy
{
    public class ModelGeneratorItemRepository
    {
        private Dictionary<string, ModItem> dataItemLookup;

        public ModelGeneratorItemRepository()
        {
            this.dataItemLookup = new Dictionary<string, ModItem>();
        }

        public string? GameDirectory { get; private set; }

        public IEnumerable<ModItem> GetDataItems()
        {
            return this.dataItemLookup.Values;
        }

        public ModItem GetDataItemByStringId(string id)
        {
            return this.dataItemLookup[id];
        }

        public async Task LoadAsync()
        {
            var installations = await new InstallationService().DiscoverAllInstallationsAsync()
                .ToDictionaryAsync(i => i.Identifier);
            var installation = installations.Values.FirstOrDefault();

            var contextOptions = new ModContextOptions(
                Guid.NewGuid().ToString(),
                installation,
                loadGameFiles: ModLoadType.Base,
                loadEnabledMods: ModLoadType.Base,
                throwIfMissing: false);

            var context = await new ContextBuilder().BuildAsync(contextOptions);
            var contextItems = context.Items.AsEnumerable();

            this.dataItemLookup = contextItems.ToDictionary(item => item.StringId, item => item);
        }
    }
}
