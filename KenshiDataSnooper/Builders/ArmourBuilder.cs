using System.Diagnostics;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Builders
{
    internal class ArmourBuilder : IItemBuilder<Armour>
    {
        private readonly Dictionary<string, Action<Coverage, int>> coverageMap;
        private readonly ItemRepository itemRepository;
        private readonly ItemSourcesCreator itemSourcesCreator;

        public ArmourBuilder(ItemRepository itemRepository, ItemSourcesCreator itemSourcesCreator)
        {
            // This can be partailly replaced by ItemRepository lookup
            this.coverageMap = new Dictionary<string, Action<Coverage, int>>()
            {
                { "101-gamedata.quack", (coverage, val) => coverage.Chest = val },
                { "32-gamedata.quack", (coverage, val) => coverage.Head = val },
                { "28-gamedata.quack", (coverage, val) => coverage.LeftArm = val },
                { "4019-gamedata.base", (coverage, val) => coverage.LeftForeleg = val },
                { "30-gamedata.quack", (coverage, val) => coverage.LeftLeg = val },
                { "29-gamedata.quack", (coverage, val) => coverage.RightArm = val },
                { "4018-gamedata.base", (coverage, val) => coverage.RightForeleg = val },
                { "31-gamedata.quack", (coverage, val) => coverage.RightLeg = val },
                { "100-gamedata.quack", (coverage, val) => coverage.Stomach = val },
            };
            this.itemRepository = itemRepository;
            this.itemSourcesCreator = itemSourcesCreator;
        }

        public Armour Build(DataItem baseItem)
        {
            if (baseItem.Type != ItemType.Armour)
            {
                throw new ArgumentException($"Cannot create Armour object using base ItemType {baseItem.Type}", nameof(baseItem));
            }

            var sw = Stopwatch.StartNew();
            var coverage = this.ConvertCoverage(baseItem);
            Console.WriteLine($" - Converting the coverage for {baseItem.Name} took {sw.Elapsed}");

            sw.Restart();
            var crafting = this.ConvertCrafting(baseItem, coverage);
            Console.WriteLine($" - Converting the craftings for {baseItem.Name} took {sw.Elapsed}");

            sw.Restart();
            var unlockingResearch = this.ConvertUnlockingResearch(baseItem);
            Console.WriteLine($" - Converting the unlocking research for {baseItem.Name} took {sw.Elapsed}");

            sw.Restart();
            var blueprintLocations = this.ConvertBlueprintlocations(baseItem);
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
                BlueprintLocations = blueprintLocations,
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

        private IEnumerable<ItemReference> ConvertBlueprintlocations(DataItem baseItem)
        {
            var results = new List<ItemReference>();

            var referencingVendorLists = this.itemRepository
                .GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.VendorList)
                .ToList();
            var blueprintVendorLists = referencingVendorLists
                .Where(list => list.ReferenceCategories.Values
                    .Where(cat => "armour blueprints".Equals(cat.Name))
                    .SelectMany(cat => cat.Values)
                    .Any(cat => cat.TargetId.Equals(baseItem.StringId)))
                .ToList();
            var squads = blueprintVendorLists
                .SelectMany(vendor => this.itemRepository
                    .GetReferencingDataItemsFor(vendor)
                    .Where(item => item.Type == ItemType.SquadTemplate))
                .ToList();

            foreach (var squad in squads)
            {
                var towns = this.itemRepository.GetReferencingDataItemsFor(squad)
                    .Where(item => item.Type == ItemType.Town);

                foreach (var town in towns)
                {
                    var reference = new ItemReference()
                    {
                        StringId = town.StringId,
                        Name = town.Name,
                    };

                    results.Add(reference);
                }
            }

            return results;
        }

        private ItemReference? ConvertUnlockingResearch(DataItem baseItem)
        {
            var researchItems = this.itemRepository.GetReferencingDataItemsFor(baseItem)
                .Where(item => item.Type == ItemType.Research);

            ItemReference? result = null;
            if (researchItems.Any())
            {
                var research = researchItems.Single();
                return new ItemReference()
                {
                    Name = research.Name,
                    StringId = research.StringId,
                };
            }

            return result;
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
                var flatConsumedReferences = functionality.ReferenceCategories.Values
                    .Where(cat => "consumes".Equals(cat.Name))
                    .SelectMany(cat => cat.Values);
                var consumedMaterialNames = flatConsumedReferences
                    .Select(cat => this.itemRepository.GetDataItemByStringId(cat.TargetId).Name);

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

        private Coverage ConvertCoverage(DataItem baseItem)
        {
            var coverageCategory = baseItem.ReferenceCategories.Values
                .FirstOrDefault(cat => "part coverage".Equals(cat.Key));

            Coverage coverage = new ();
            if (coverageCategory is not null)
            {
                var references = coverageCategory.Values;
                foreach (var reference in references)
                {
                    var action = this.coverageMap[reference.Key];
                    action(coverage, reference.Value0);
                }
            }

            return coverage;
        }
    }
}
