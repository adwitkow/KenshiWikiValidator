using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class LimbReplacement : ItemBase, IDescriptive, IAllow2DIconOffset, IAllow3DIconOffset
    {
        public LimbReplacement(string stringId, string name)
            : base(stringId, name)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpecsClothing>>();
            this.Ingredients = Enumerable.Empty<ItemReference<Item>>();
        }

        public override ItemType Type => ItemType.LimbReplacement;

        [Value("auto icon image")]
        public bool? AutoIconImage { get; set; }

        [Value("has collision")]
        public bool? HasCollision { get; set; }

        [Value("athletics mult")]
        public float? AthleticsMult { get; set; }

        [Value("athletics mult 1")]
        public float? AthleticsMult1 { get; set; }

        [Value("combat speed mult")]
        public float? CombatSpeedMult { get; set; }

        [Value("craft time hrs")]
        public float? CraftTimeHrs { get; set; }

        [Value("dexterity mult")]
        public float? DexterityMult { get; set; }

        [Value("dexterity mult 1")]
        public float? DexterityMult1 { get; set; }

        [Value("encumbrance mult")]
        public float? EncumbranceMult { get; set; }

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

        [Value("offset")]
        public float? Offset { get; set; }

        [Value("overall mult")]
        public float? OverallMult { get; set; }

        [Value("ranged mult")]
        public float? RangedMult { get; set; }

        [Value("ranged mult 1")]
        public float? RangedMult1 { get; set; }

        [Value("stealth mult")]
        public float? StealthMult { get; set; }

        [Value("stealth mult 1")]
        public float? StealthMult1 { get; set; }

        [Value("strength mult")]
        public float? StrengthMult { get; set; }

        [Value("strength mult 1")]
        public float? StrengthMult1 { get; set; }

        [Value("swimming mult")]
        public float? SwimmingMult { get; set; }

        [Value("swimming mult 1")]
        public float? SwimmingMult1 { get; set; }

        [Value("thievery mult")]
        public float? ThieveryMult { get; set; }

        [Value("thievery mult 1")]
        public float? ThieveryMult1 { get; set; }

        [Value("weight kg")]
        public float? WeightKg { get; set; }

        [Value("HP")]
        public int? Hp { get; set; }

        [Value("HP 1")]
        public int? Hp1 { get; set; }

        [Value("inventory footprint height")]
        public int? InventoryFootprintHeight { get; set; }

        [Value("inventory footprint width")]
        public int? InventoryFootprintWidth { get; set; }

        [Value("slot")]
        public int? Slot { get; set; }

        [Value("unarmed damage bonus")]
        public int? UnarmedDamageBonus { get; set; }

        [Value("unarmed damage bonus 1")]
        public int? UnarmedDamageBonus1 { get; set; }

        [Value("value")]
        public int? Value { get; set; }

        [Value("value 1")]
        public int? Value1 { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("ground mesh")]
        public object? GroundMesh { get; set; }

        [Value("icon")]
        public object? Icon { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("mesh female")]
        public object? MeshFemale { get; set; }

        [Value("physics file")]
        public object? PhysicsFile { get; set; }

        [Value("speed limit")]
        public int? SpeedLimit { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpecsClothing>> Material { get; set; }

        [Reference("ingredients")]
        public IEnumerable<ItemReference<Item>> Ingredients { get; set; }

    }
}