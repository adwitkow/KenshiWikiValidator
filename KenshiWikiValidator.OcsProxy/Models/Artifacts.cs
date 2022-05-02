using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Artifacts : ItemBase
    {
        public Artifacts(string stringId, string name)
            : base(stringId, name)
        {
            this.Armours = Enumerable.Empty<ItemReference<Armour>>();
            this.ArmoursHq = Enumerable.Empty<ItemReference<Armour>>();
            this.Crossbows = Enumerable.Empty<ItemReference<Crossbow>>();
            this.Items = Enumerable.Empty<ItemReference<Item>>();
            this.Robotics = Enumerable.Empty<ItemReference<LimbReplacement>>();
            this.Weapons = Enumerable.Empty<ItemReference<MaterialSpecsWeapon>>();
        }

        public override ItemType Type => ItemType.Artifacts;

        [Reference("armours")]
        public IEnumerable<ItemReference<Armour>> Armours { get; set; }

        [Reference("armours hq")]
        public IEnumerable<ItemReference<Armour>> ArmoursHq { get; set; }

        [Reference("crossbows")]
        public IEnumerable<ItemReference<Crossbow>> Crossbows { get; set; }

        [Reference("items")]
        public IEnumerable<ItemReference<Item>> Items { get; set; }

        [Reference("robotics")]
        public IEnumerable<ItemReference<LimbReplacement>> Robotics { get; set; }

        [Reference("weapons")]
        public IEnumerable<ItemReference<MaterialSpecsWeapon>> Weapons { get; set; }

    }
}