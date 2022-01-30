using System.Diagnostics;
using KenshiWikiValidator.Features.DataItemConversion.Builders.Components;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders
{
    internal class ArmourBuilder : IItemBuilder<Armour>
    {
        private readonly ItemRepository itemRepository;
        private readonly ItemSourcesCreator itemSourcesCreator;
        private readonly BlueprintSquadsConverter blueprintSquadsConverter;
        private readonly UnlockingResearchConverter unlockingResearchConverter;
        private readonly CoverageConverter coverageConverter;

        public ArmourBuilder(
            ItemRepository itemRepository,
            ItemSourcesCreator itemSourcesCreator,
            BlueprintSquadsConverter blueprintSquadsConverter,
            UnlockingResearchConverter unlockingResearchConverter)
        {
            this.itemRepository = itemRepository;
            this.itemSourcesCreator = itemSourcesCreator;
            this.blueprintSquadsConverter = blueprintSquadsConverter;
            this.unlockingResearchConverter = unlockingResearchConverter;

            this.coverageConverter = new CoverageConverter();
        }

        public Armour Build(DataItem baseItem)
        {
            if (baseItem.Type != ItemType.Armour)
            {
                throw new ArgumentException($"Cannot create Armour object using base ItemType {baseItem.Type}", nameof(baseItem));
            }

            var sw = Stopwatch.StartNew();
            var coverage = this.coverageConverter.Convert(baseItem);
            Console.WriteLine($" - Converting the coverage for {baseItem.Name} took {sw.Elapsed}");

            sw.Restart();
            var crafting = this.ConvertCrafting(baseItem, coverage);
            Console.WriteLine($" - Converting the craftings for {baseItem.Name} took {sw.Elapsed}");

            sw.Restart();
            var unlockingResearch = this.unlockingResearchConverter.Convert(baseItem);
            Console.WriteLine($" - Converting the unlocking research for {baseItem.Name} took {sw.Elapsed}");

            sw.Restart();
            var blueprintSquads = this.blueprintSquadsConverter.Convert(baseItem, "armour blueprints");
            Console.WriteLine($" - Converting the blueprint locations for {baseItem.Name} took {sw.Elapsed}");

            sw.Restart();
            var itemSources = this.itemSourcesCreator.Create(baseItem);
            Console.WriteLine($" - Converting the item sources for {baseItem.Name} took {sw.Elapsed}");

            sw.Stop();

            return new Armour()
            {
                Name = baseItem.Name,
                Properties = new Dictionary<string, object>(baseItem.Values),
                StringId = baseItem.StringId,
                Coverage = coverage,
                CraftedIn = crafting,
                Sources = itemSources,
                UnlockingResearch = unlockingResearch,
                BlueprintSquads = blueprintSquads,
            };
        }

        private static decimal GetMaterialCost(Coverage coverage)
        {
            var materialCost = ((coverage.Chest * 1.5m)
                + coverage.Head
                + coverage.Stomach
                + coverage.LeftForeleg
                + coverage.RightForeleg
                + coverage.LeftArm
                + coverage.RightArm
                + coverage.LeftLeg
                + coverage.RightLeg) * 0.01m;

            if (materialCost == 0)
            {
                materialCost = 1;
            }

            return materialCost;
        }

        private IEnumerable<Crafting> ConvertCrafting(DataItem baseItem, Coverage coverage)
        {
            var results = new List<Crafting>();

            var fabricsAmount = Convert.ToDecimal(baseItem.Values["fabrics amount"]);
            var realMaterialCost = GetMaterialCost(coverage);
            var realFabricsCost = realMaterialCost * fabricsAmount;

            var functionalities = this.itemRepository
                .GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.BuildingFunctionality);

            if (!functionalities.Any())
            {
                return results;
            }

            foreach (var functionality in functionalities)
            {
                var consumedMaterialNames = functionality
                    .GetReferenceItems(this.itemRepository, "consumes")
                    .Select(item => item.Name);
                var baseMaterial = consumedMaterialNames.FirstOrDefault(name => !"Fabrics".Equals(name));
                if (string.IsNullOrEmpty(baseMaterial))
                {
                    baseMaterial = consumedMaterialNames.First();
                    realFabricsCost = 0;
                }

                var craftingBuilding = this.itemRepository
                    .GetReferencingDataItemsFor(functionality)
                    .Single();

                results.Add(new Crafting()
                {
                    Building = craftingBuilding.Name,
                    BaseMaterial = baseMaterial,
                    BaseMaterialCost = realMaterialCost.Normalize(),
                    FabricsCost = realFabricsCost.Normalize(),
                });
            }

            return results;
        }
    }
}
