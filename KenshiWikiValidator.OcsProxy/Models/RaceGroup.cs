using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class RaceGroup : ItemBase
    {
        public RaceGroup(string stringId, string name)
            : base(stringId, name)
        {
            this.Races = Enumerable.Empty<ItemReference<Race>>();
        }

        public override ItemType Type => ItemType.RaceGroup;

        [Value("description")]
        public string? Description { get; set; }

        [Reference("races")]
        public IEnumerable<ItemReference<Race>> Races { get; set; }

    }
}