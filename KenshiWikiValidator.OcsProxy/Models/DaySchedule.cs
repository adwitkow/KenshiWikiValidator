using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class DaySchedule : ItemBase
    {
        public DaySchedule(string stringId, string name)
            : base(stringId, name)
        {
            this.Building = Enumerable.Empty<ItemReference<Building>>();
        }

        public override ItemType Type => ItemType.DaySchedule;

        [Value("layout exterior")]
        public string? LayoutExterior { get; set; }

        [Value("layout interior")]
        public string? LayoutInterior { get; set; }

        [Reference("building")]
        public IEnumerable<ItemReference<Building>> Building { get; set; }

    }
}