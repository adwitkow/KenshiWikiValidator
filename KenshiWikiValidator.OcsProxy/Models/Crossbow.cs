using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Crossbow : ItemBase, IDescriptive, IAllow2DIconOffset, IAllow3DIconOffset
    {
        public Crossbow(string stringId, string name)
            : base(stringId, name)
        {
            this.Ammo = Enumerable.Empty<ItemReference<Item>>();
            this.Ingredients = Enumerable.Empty<ItemReference<Item>>();
            this.Material = Enumerable.Empty<ItemReference<MaterialSpecsWeapon>>();
            this.ReloadAnim = Enumerable.Empty<ItemReference<Animation>>();
        }

        public override ItemType Type => ItemType.Crossbow;

        [Value("auto icon image")]
        public bool? AutoIconImage { get; set; }

        [Value("has collision")]
        public bool? HasCollision { get; set; }

        [Value("accuracy deviation at 0 skill")]
        public float? AccuracyDeviationAt0Skill { get; set; }

        [Value("accuracy deviation at 0 skill 1")]
        public float? AccuracyDeviationAt0Skill1 { get; set; }

        [Value("aim speed")]
        public float? AimSpeed { get; set; }

        [Value("animal damage mult")]
        public float? AnimalDamageMult { get; set; }

        [Value("barrel pos Y")]
        public float? BarrelPosY { get; set; }

        [Value("barrel pos Z")]
        public float? BarrelPosZ { get; set; }

        [Value("bleed mult")]
        public float? BleedMult { get; set; }

        [Value("bleed mult 1")]
        public float? BleedMult1 { get; set; }

        [Value("craft time hrs")]
        public float? CraftTimeHrs { get; set; }

        [Value("equip offset")]
        public float? EquipOffset { get; set; }

        [Value("human damage mult")]
        public float? HumanDamageMult { get; set; }

        [Value("icon offset H")]
        public float? IconOffsetH { get; set; }

        [Value("icon offset pitch")]
        public float? IconOffsetPitch { get; set; }

        [Value("icon offset roll")]
        public float? IconOffsetRoll { get; set; }

        [Value("icon offset V")]
        public float? IconOffsetV { get; set; }

        [Value("icon offset yaw")]
        public float? IconOffsetYaw { get; set; }

        [Value("icon zoom")]
        public float? IconZoom { get; set; }

        [Value("overall scale")]
        public float? OverallScale { get; set; }

        [Value("pierce damage")]
        public float? PierceDamage { get; set; }

        [Value("pierce damage max 0")]
        public float? PierceDamageMax0 { get; set; }

        [Value("pierce damage max 1")]
        public float? PierceDamageMax1 { get; set; }

        [Value("pierce damage min 0")]
        public float? PierceDamageMin0 { get; set; }

        [Value("pierce damage min 1")]
        public float? PierceDamageMin1 { get; set; }

        [Value("radius")]
        public float? Radius { get; set; }

        [Value("reload time max")]
        public float? ReloadTimeMax { get; set; }

        [Value("reload time max 1")]
        public float? ReloadTimeMax1 { get; set; }

        [Value("reload time min")]
        public float? ReloadTimeMin { get; set; }

        [Value("reload time min 1")]
        public float? ReloadTimeMin1 { get; set; }

        [Value("robot damage mult")]
        public float? RobotDamageMult { get; set; }

        [Value("scale length")]
        public float? ScaleLength { get; set; }

        [Value("scale thickness")]
        public float? ScaleThickness { get; set; }

        [Value("scale width")]
        public float? ScaleWidth { get; set; }

        [Value("shot speed")]
        public float? ShotSpeed { get; set; }

        [Value("shot speed 1")]
        public float? ShotSpeed1 { get; set; }

        [Value("weight")]
        public float? Weight { get; set; }

        [Value("weight kg")]
        public float? WeightKg { get; set; }

        [Value("accuracy perfect skill")]
        public int? AccuracyPerfectSkill { get; set; }

        [Value("inventory footprint height")]
        public int? InventoryFootprintHeight { get; set; }

        [Value("inventory footprint width")]
        public int? InventoryFootprintWidth { get; set; }

        [Value("material cost")]
        public int? MaterialCost { get; set; }

        [Value("num shots")]
        public int? NumShots { get; set; }

        [Value("range")]
        public int? Range { get; set; }

        [Value("range 1")]
        public int? Range1 { get; set; }

        [Value("shoot sound")]
        public int? ShootSound { get; set; }

        [Value("skill mod")]
        public int? SkillMod { get; set; }

        [Value("slot")]
        public int? Slot { get; set; }

        [Value("value")]
        public int? Value { get; set; }

        [Value("value 1")]
        public int? Value1 { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("extra mesh loaded")]
        public object? ExtraMeshLoaded { get; set; }

        [Value("extra mesh unloaded")]
        public object? ExtraMeshUnloaded { get; set; }

        [Value("ground mesh")]
        public object? GroundMesh { get; set; }

        [Value("icon")]
        public object? Icon { get; set; }

        [Value("live ammo")]
        public object? LiveAmmo { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("physics file")]
        public object? PhysicsFile { get; set; }

        [Value("sheath")]
        public object? Sheath { get; set; }

        [Reference("ammo")]
        public IEnumerable<ItemReference<Item>> Ammo { get; set; }

        [Reference("ingredients")]
        public IEnumerable<ItemReference<Item>> Ingredients { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpecsWeapon>> Material { get; set; }

        [Reference("reload anim")]
        public IEnumerable<ItemReference<Animation>> ReloadAnim { get; set; }

    }
}