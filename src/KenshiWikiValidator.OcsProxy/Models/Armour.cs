// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using KenshiWikiValidator.OcsProxy.Models.Interfaces;
using OpenConstructionSet.Data;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Armour : ItemBase, IDescriptive
    {
        public Armour(string stringId, string name)
            : base(stringId, name)
        {
            this.Color = Enumerable.Empty<ItemReference<ColorData>>();
            this.RacesExclude = Enumerable.Empty<ItemReference<Race>>();
            this.PartCoverage = Enumerable.Empty<ItemReference<LocationalDamage>>();
            this.Material = Enumerable.Empty<ItemReference<MaterialSpecsClothing>>();
            this.GoesBadlyWith = Enumerable.Empty<ItemReference<Armour>>();
            this.Races = Enumerable.Empty<ItemReference<Race>>();
            this.OverlapItems = Enumerable.Empty<ItemReference<Armour>>();
            this.GoesWellWith = Enumerable.Empty<ItemReference<Armour>>();
        }

        public override ItemType Type => ItemType.Armour;

        [Value("auto icon image")]
        public bool? AutoIconImage { get; set; }

        [Value("dont colorise")]
        public bool? DontColorise { get; set; }

        [Value("has collision")]
        public bool? HasCollision { get; set; }

        [Value("hide beard")]
        public bool? HideBeard { get; set; }

        [Value("hide hair")]
        public bool? HideHair { get; set; }

        [Value("hide part b")]
        public bool? HidePartB { get; set; }

        [Value("hide part g")]
        public bool? HidePartG { get; set; }

        [Value("hide part r")]
        public bool? HidePartR { get; set; }

        [Value("is locked")]
        public bool? IsLocked { get; set; }

        [Value("asassination mult")]
        public float? AsassinationMult { get; set; }

        [Value("athletics mult")]
        public float? AthleticsMult { get; set; }

        [Value("blunt def bonus")]
        public float? BluntDefBonus { get; set; }

        [Value("boot height")]
        public float? BootHeight { get; set; }

        [Value("combat speed mult")]
        public float? CombatSpeedMult { get; set; }

        [Value("cut def bonus")]
        public float? CutDefBonus { get; set; }

        [Value("cut into stun")]
        public float? CutIntoStun { get; set; }

        [Value("damage output mult")]
        public float? DamageOutputMult { get; set; }

        [Value("dexterity mult")]
        public float? DexterityMult { get; set; }

        [Value("dodge mult")]
        public float? DodgeMult { get; set; }

        [Value("fabrics amount")]
        public float? FabricsAmount { get; set; }

        [Value("fist injury mult")]
        public float? FistInjuryMult { get; set; }

        [Value("hardness")]
        public float? Hardness { get; set; }

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

        [Value("pierce def mult")]
        public float? PierceDefMult { get; set; }

        [Value("ranged skill mult")]
        public float? RangedSkillMult { get; set; }

        [Value("relative price mult")]
        public float? RelativePriceMult { get; set; }

        [Value("stealth mult")]
        public float? StealthMult { get; set; }

        [Value("weather protection amount")]
        public float? WeatherProtectionAmount { get; set; }

        [Value("weight kg")]
        public float? WeightKg { get; set; }

        [Value("class")]
        public int? Class { get; set; }

        [Value("combat attk bonus")]
        public int? CombatAttkBonus { get; set; }

        [Value("combat def bonus")]
        public int? CombatDefBonus { get; set; }

        [Value("combat skill bonus")]
        public int? CombatSkillBonus { get; set; }

        [Value("hide parts")]
        public int? HideParts { get; set; }

        [Value("hide stump")]
        public int? HideStump { get; set; }

        [Value("inventory footprint height")]
        public int? InventoryFootprintHeight { get; set; }

        [Value("inventory footprint width")]
        public int? InventoryFootprintWidth { get; set; }

        [Value("level bonus")]
        public int? LevelBonus { get; set; }

        [Value("lock level")]
        public int? LockLevel { get; set; }

        [Value("material type")]
        public int? MaterialType { get; set; }

        [Value("perception bonus")]
        public int? PerceptionBonus { get; set; }

        [Value("ranged bonus")]
        public int? RangedBonus { get; set; }

        [Value("slot")]
        public AttachSlot Slot { get; set; }

        [Value("stigma")]
        public int? Stigma { get; set; }

        [Value("unarmed bonus")]
        public int? UnarmedBonus { get; set; }

        [Value("value")]
        public int? Value { get; set; }

        [Value("weather protection0")]
        public int? WeatherProtection0 { get; set; }

        [Value("class name")]
        public string? ClassName { get; set; }

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

        [Value("overlap mesh")]
        public object? OverlapMesh { get; set; }

        [Value("overlap mesh female")]
        public object? OverlapMeshFemale { get; set; }

        [Value("physics file")]
        public object? PhysicsFile { get; set; }

        [Value("vest colormap")]
        public object? VestColormap { get; set; }

        [Value("vest colormap female")]
        public object? VestColormapFemale { get; set; }

        [Value("vest normalmap")]
        public object? VestNormalmap { get; set; }

        [Value("vest normalmap female")]
        public object? VestNormalmapFemale { get; set; }

        [Value("vest texture")]
        public object? VestTexture { get; set; }

        [Value("vest texture female")]
        public object? VestTextureFemale { get; set; }

        [Value("weather protection1")]
        public int? WeatherProtection1 { get; set; }

        [Value("weather protection2")]
        public int? WeatherProtection2 { get; set; }

        [Value("inventory sound")]
        public int? InventorySound { get; set; }

        [Value("weather protection3")]
        public int? WeatherProtection3 { get; set; }

        [Reference("color")]
        public IEnumerable<ItemReference<ColorData>> Color { get; set; }

        [Reference("races exclude")]
        public IEnumerable<ItemReference<Race>> RacesExclude { get; set; }

        [Reference("part coverage")]
        public IEnumerable<ItemReference<LocationalDamage>> PartCoverage { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpecsClothing>> Material { get; set; }

        [Reference("goes badly with")]
        public IEnumerable<ItemReference<Armour>> GoesBadlyWith { get; set; }

        [Reference("races")]
        public IEnumerable<ItemReference<Race>> Races { get; set; }

        [Reference("overlap items")]
        public IEnumerable<ItemReference<Armour>> OverlapItems { get; set; }

        [Reference("goes well with")]
        public IEnumerable<ItemReference<Armour>> GoesWellWith { get; set; }
    }
}