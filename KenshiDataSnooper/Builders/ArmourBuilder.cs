using System.Linq.Expressions;
using KenshiDataSnooper.Models;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiDataSnooper.Builders
{
    internal class ArmourBuilder : IItemBuilder<Armour>
    {
        private readonly Dictionary<string, Action<Coverage, int>> coverageMap;

        public ArmourBuilder()
        {
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
        }

        public Armour Build(DataItem baseItem)
        {
            if (baseItem.Type != ItemType.Armour)
            {
                throw new ArgumentException($"Cannot create Armour object using base ItemType {baseItem.Type}", nameof(baseItem));
            }

            var coverage = this.ConvertCoverage(baseItem);

            return new Armour()
            {
                Name = baseItem.Name,
                Properties = new Dictionary<string, object>(baseItem.Values),
                StringId = baseItem.StringId,
                Coverage = coverage,
            };
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
