using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.Features.DataItemConversion.Builders.Components
{
    internal class CoverageConverter
    {
        private readonly Dictionary<string, Action<Coverage, int>> coverageMap;

        public CoverageConverter()
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

        public Coverage Convert(IItem baseItem)
        {
            var coverageCategory = baseItem.ReferenceCategories
                .FirstOrDefault(cat => "part coverage".Equals(cat.Name));

            var coverage = new Coverage();
            if (coverageCategory is not null)
            {
                var references = coverageCategory.References;
                foreach (var reference in references)
                {
                    var action = this.coverageMap[reference.TargetId];
                    action(coverage, reference.Value0);
                }
            }

            return coverage;
        }
    }
}
