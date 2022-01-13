using System.Linq.Expressions;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Builders
{
    internal class ArmourBuilder : IItemBuilder<Armour>
    {
        private readonly Dictionary<string, Action<Coverage, int>> coverageMap;
        private readonly ItemRepository itemRepository;

        public ArmourBuilder(ItemRepository itemRepository)
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
        }

        public Armour Build(DataItem baseItem)
        {
            if (baseItem.Type != ItemType.Armour)
            {
                throw new ArgumentException($"Cannot create Armour object using base ItemType {baseItem.Type}", nameof(baseItem));
            }

            var coverage = this.ConvertCoverage(baseItem);
            var crafting = this.ConvertCrafting(baseItem, coverage);

            return new Armour()
            {
                Name = baseItem.Name,
                Properties = new Dictionary<string, object>(baseItem.Values),
                StringId = baseItem.StringId,
                Coverage = coverage,
                CraftedIn = crafting,
            };
        }

        private static decimal GetMaterialCost(Coverage coverage)
        {
            return ((coverage.Chest * 1.5m)
                + coverage.Head
                + coverage.Stomach
                + coverage.LeftForeleg
                + coverage.RightForeleg
                + coverage.LeftArm
                + coverage.RightArm
                + coverage.LeftLeg
                + coverage.RightLeg) / 100;
        }

        private IEnumerable<Crafting> ConvertCrafting(DataItem baseItem, Coverage coverage)
        {
            var results = new List<Crafting>();

            var fabricsAmount = Convert.ToDecimal(baseItem.Values["fabrics amount"]);
            var realMaterialCost = GetMaterialCost(coverage);
            var realFabricsCost = realMaterialCost * fabricsAmount;

            var functionalities = this.itemRepository
                .GetReferencingItemsFor(baseItem)
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
                    .Select(cat => this.itemRepository.GetByStringId(cat.TargetId).Name);

                var baseMaterial = consumedMaterialNames.FirstOrDefault(name => !"Fabrics".Equals(name));
                if (string.IsNullOrEmpty(baseMaterial))
                {
                    baseMaterial = consumedMaterialNames.First();
                    realFabricsCost = 0;
                }

                var craftingBuilding = this.itemRepository
                    .GetReferencingItemsFor(functionality)
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
