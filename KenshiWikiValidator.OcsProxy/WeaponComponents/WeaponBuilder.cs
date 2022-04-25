using System.Diagnostics;
using KenshiWikiValidator.OcsProxy.Builder;
using KenshiWikiValidator.OcsProxy.SharedComponents;
using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.OcsProxy.WeaponComponents
{
    internal class WeaponBuilder : ItemBuilderBase<Weapon>
    {
        private readonly ItemRepository itemRepository;
        private readonly ItemSourcesCreator itemSourcesCreator;
        private readonly BlueprintSquadsConverter blueprintSquadsConverter;
        private readonly UnlockingResearchConverter unlockingResearchConverter;

        public WeaponBuilder(
            ItemRepository itemRepository,
            ItemSourcesCreator itemSourcesCreator,
            BlueprintSquadsConverter blueprintSquadsConverter,
            UnlockingResearchConverter unlockingResearchConverter)
        {
            this.itemRepository = itemRepository;

            this.itemSourcesCreator = itemSourcesCreator;
            this.blueprintSquadsConverter = blueprintSquadsConverter;
            this.unlockingResearchConverter = unlockingResearchConverter;
        }

        public override Weapon Build(DataItem baseItem)
        {
            var sw = Stopwatch.StartNew();
            var unlockingResearch = this.unlockingResearchConverter.Convert(baseItem);
            Console.WriteLine($" - Converting the unlocking research for {baseItem.Name} took {sw.Elapsed}");

            var blueprintSquads = Enumerable.Empty<ItemReference>();
            if (unlockingResearch is not null)
            {
                var unlockingResearchItem = this.itemRepository.GetDataItemByStringId(unlockingResearch.StringId!);

                sw.Restart();
                blueprintSquads = this.blueprintSquadsConverter.Convert(unlockingResearchItem, "blueprints");
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
                BlueprintSquads = blueprintSquads,
                UnlockingResearch = unlockingResearch,
            };
        }
    }
}