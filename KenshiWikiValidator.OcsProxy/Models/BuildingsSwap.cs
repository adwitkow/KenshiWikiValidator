using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class BuildingsSwap : ItemBase
    {
        public BuildingsSwap(string stringId, string name)
            : base(stringId, name)
        {
            this.ReplaceWith = Enumerable.Empty<ItemReference<Building>>();
            this.ToReplace = Enumerable.Empty<ItemReference<Building>>();
        }

        public override ItemType Type => ItemType.BuildingsSwap;

        [Reference("replace with")]
        public IEnumerable<ItemReference<Building>> ReplaceWith { get; set; }

        [Reference("to replace")]
        public IEnumerable<ItemReference<Building>> ToReplace { get; set; }

    }
}