using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Research : ItemBase, IDescriptive
    {
        public Research(string stringId, string name)
            : base(stringId, name)
        {
            this.Costs = Enumerable.Empty<ItemReference<Item>>();
            this.EnableBuildings = Enumerable.Empty<ItemReference<Building>>();
            this.Requirements = Enumerable.Empty<ItemReference<Research>>();
            this.EnableWeaponTypes = Enumerable.Empty<ItemReference<Weapon>>();
            this.EnableRobotics = Enumerable.Empty<ItemReference<LimbReplacement>>();
            this.BlueprintItem = Enumerable.Empty<ItemReference<Item>>();
            this.EnableArmour = Enumerable.Empty<ItemReference<Armour>>();
            this.ImproveBuildings = Enumerable.Empty<ItemReference<Building>>();
            this.EnableWeaponModel = Enumerable.Empty<ItemReference<MaterialSpecsWeapon>>();
            this.EnableItem = Enumerable.Empty<ItemReference<Item>>();
            this.EnableCrossbow = Enumerable.Empty<ItemReference<Crossbow>>();
        }

        public override ItemType Type => ItemType.Research;

        [Value("blueprint only")]
        public bool? BlueprintOnly { get; set; }

        [Value("is level upgrade")]
        public bool? IsLevelUpgrade { get; set; }

        [Value("production mult")]
        public float? ProductionMult { get; set; }

        [Value("repeat mult")]
        public float? RepeatMult { get; set; }

        [Value("level")]
        public int? Level { get; set; }

        [Value("money")]
        public int? Money { get; set; }

        [Value("power capacity increase")]
        public int? PowerCapacityIncrease { get; set; }

        [Value("power increase")]
        public int? PowerIncrease { get; set; }

        [Value("repeats")]
        public int? Repeats { get; set; }

        [Value("time")]
        public int? Time { get; set; }

        [Value("category")]
        public string? Category { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Reference("cost")]
        public IEnumerable<ItemReference<Item>> Costs { get; set; }

        [Reference("enable buildings")]
        public IEnumerable<ItemReference<Building>> EnableBuildings { get; set; }

        [Reference("requirements")]
        public IEnumerable<ItemReference<Research>> Requirements { get; set; }

        [Reference("enable weapon type")]
        public IEnumerable<ItemReference<Weapon>> EnableWeaponTypes { get; set; }

        [Reference("enable robotics")]
        public IEnumerable<ItemReference<LimbReplacement>> EnableRobotics { get; set; }

        [Reference("blueprint item")]
        public IEnumerable<ItemReference<Item>> BlueprintItem { get; set; }

        [Reference("enable armour")]
        public IEnumerable<ItemReference<Armour>> EnableArmour { get; set; }

        [Reference("improve buildings")]
        public IEnumerable<ItemReference<Building>> ImproveBuildings { get; set; }

        [Reference("enable weapon model")]
        public IEnumerable<ItemReference<MaterialSpecsWeapon>> EnableWeaponModel { get; set; }

        [Reference("enable item")]
        public IEnumerable<ItemReference<Item>> EnableItem { get; set; }

        [Reference("enable crossbow")]
        public IEnumerable<ItemReference<Crossbow>> EnableCrossbow { get; set; }

    }
}