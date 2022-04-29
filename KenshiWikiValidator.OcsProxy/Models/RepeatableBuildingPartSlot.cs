using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class RepeatableBuildingPartSlot : ItemBase
    {
        public RepeatableBuildingPartSlot(string stringId, string name)
            : base(stringId, name)
        {
            this.Parts = Enumerable.Empty<ItemReference<BuildingPart>>();
        }

        public override ItemType Type => ItemType.RepeatableBuildingPartSlot;

        [Reference("parts")]
        public IEnumerable<ItemReference<BuildingPart>> Parts { get; set; }

    }
}