using System.Diagnostics;
using KenshiDataSnooper.Builders.Components;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;

namespace KenshiDataSnooper.Builders
{
    internal class WeaponBuilder : IItemBuilder<Weapon>
    {
        private readonly ItemRepository itemRepository;
        private readonly ItemSourcesCreator itemSourcesCreator;
        private readonly BlueprintLocationsConverter blueprintLocationsConverter;
        private readonly UnlockingResearchConverter unlockingResearchConverter;

        public WeaponBuilder(
            ItemRepository itemRepository,
            ItemSourcesCreator itemSourcesCreator,
            BlueprintLocationsConverter blueprintLocationsConverter,
            UnlockingResearchConverter unlockingResearchConverter)
        {
            this.itemRepository = itemRepository;

            this.itemSourcesCreator = itemSourcesCreator;
            this.blueprintLocationsConverter = blueprintLocationsConverter;
            this.unlockingResearchConverter = unlockingResearchConverter;
        }

        public Weapon Build(DataItem baseItem)
        {
            var sw = Stopwatch.StartNew();
            var unlockingResearch = this.unlockingResearchConverter.Convert(baseItem);
            Console.WriteLine($" - Converting the unlocking research for {baseItem.Name} took {sw.Elapsed}");

            var blueprintLocations = Enumerable.Empty<ItemReference>();
            if (unlockingResearch is not null)
            {
                var unlockingResearchItem = this.itemRepository.GetDataItemByStringId(unlockingResearch.StringId!);

                sw.Restart();
                blueprintLocations = this.blueprintLocationsConverter.Convert(unlockingResearchItem, "blueprints");
                Console.WriteLine($" - Converting the blueprint locations for {baseItem.Name} took {sw.Elapsed}");

            }

            sw.Restart();
            var itemSources = this.itemSourcesCreator.Create(baseItem);
            Console.WriteLine($" - Converting the item sources for {baseItem.Name} took {sw.Elapsed}");

            sw.Stop();

            return new Weapon()
            {
                Name = baseItem.Name,
                Properties = baseItem.Values,
                StringId = baseItem.StringId,
                Sources = itemSources,
                BlueprintLocations = blueprintLocations,
                UnlockingResearch = unlockingResearch,
            };
        }
    }
}