using KenshiWikiValidator.OcsProxy.SharedComponents;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.ArmourComponents
{
    public class Armour : ItemBase, IResearchable
    {
        public Armour(Dictionary<string, object> properties, string stringId, string name)
            : base(properties, stringId, name)
        {
            this.CraftedIn = Enumerable.Empty<Crafting>();
            this.BlueprintSquads = Enumerable.Empty<ItemReference>();
        }

        public override ItemType Type => ItemType.Armour;

        public Coverage? Coverage { get; set; }

        public ItemSources? Sources { get; set; }

        public IEnumerable<Crafting> CraftedIn { get; set; }

        public ItemReference? UnlockingResearch { get; set; }

        public IEnumerable<ItemReference> BlueprintSquads { get; set; }
    }
}
