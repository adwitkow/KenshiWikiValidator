using KenshiWikiValidator.OcsProxy.SharedComponents;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.WeaponComponents
{
    public class Weapon : ItemBase, IResearchable
    {
        public Weapon(Dictionary<string, object> properties, string stringId, string name) : base(properties, stringId, name)
        {
            this.BlueprintSquads = Enumerable.Empty<ItemReference>();
        }

        public Weapon(string stringId, string name) : this(new Dictionary<string, object>(), stringId, name)
        {
        }

        public override ItemType Type => ItemType.Weapon;

        public ItemSources? Sources { get; set; }

        public ItemReference? UnlockingResearch { get; set; }

        public IEnumerable<ItemReference> BlueprintSquads { get; set; }
    }
}
