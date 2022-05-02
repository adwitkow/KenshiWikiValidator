using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class DiplomaticAssaults : ItemBase
    {
        public DiplomaticAssaults(string stringId, string name)
            : base(stringId, name)
        {
            this.Assaults = Enumerable.Empty<ItemReference<SingleDiplomaticAssault>>();
        }

        public override ItemType Type => ItemType.DiplomaticAssaults;

        [Reference("assaults")]
        public IEnumerable<ItemReference<SingleDiplomaticAssault>> Assaults { get; set; }

    }
}