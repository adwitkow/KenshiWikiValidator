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
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Weapon : ItemBase, IDescriptive
    {
        public Weapon(string stringId, string name)
            : base(stringId, name)
        {
            this.RaceDamage = Enumerable.Empty<ItemReference<Race>>();
        }

        public override ItemType Type => ItemType.Weapon;

        [Value("auto icon image")]
        public bool? AutoIconImage { get; set; }

        [Value("can block")]
        public bool? CanBlock { get; set; }

        [Value("has collision")]
        public bool? HasCollision { get; set; }

        [Value("animal damage mult")]
        public float? AnimalDamageMultiplier { get; set; }

        [Value("armour penetration")]
        public float? ArmourPenetration { get; set; }

        [Value("bleed mult")]
        public float? BleedMultiplier { get; set; }

        [Value("blunt damage multiplier")]
        public float? BluntDamageMultiplier { get; set; }

        [Value("cut damage multiplier")]
        public float? CutDamageMultiplier { get; set; }

        [Value("human damage mult")]
        public float? HumanDamageMulitplier { get; set; }

        [Value("icon offset H")]
        public float? IconOffsetH { get; set; }

        [Value("icon offset V")]
        public float? IconOffsetV { get; set; }

        [Value("icon zoom")]
        public float? IconZoom { get; set; }

        [Value("min cut damage mult")]
        public float? MinCutDamageMult { get; set; }

        [Value("overall scale")]
        public float? OverallScale { get; set; }

        [Value("pierce damage multiplier")]
        public float? PierceDamageMultiplier { get; set; }

        [Value("robot damage mult")]
        public float? RobotDamageMultiplier { get; set; }

        [Value("scale length")]
        public float? ScaleLength { get; set; }

        [Value("scale thickness")]
        public float? ScaleThickness { get; set; }

        [Value("scale width")]
        public float? ScaleWidth { get; set; }

        [Value("weight kg")]
        public float? WeightKg { get; set; }

        [Value("weight mult")]
        public float? WeightMult { get; set; }

        [Value("attack mod")]
        public int? AttackModifier { get; set; }

        [Value("defence mod")]
        public int? DefenceModifier { get; set; }

        [Value("indoors mod")]
        public int? IndoorsModifier { get; set; }

        [Value("inventory footprint height")]
        public int? InventoryFootprintHeight { get; set; }

        [Value("inventory footprint width")]
        public int? InventoryFootprintWidth { get; set; }

        [Value("length")]
        public int? Length { get; set; }

        [Value("material")]
        public int? Material { get; set; }

        [Value("material cost")]
        public int? MaterialCost { get; set; }

        [Value("skill category")]
        public int? SkillCategory { get; set; }

        [Value("skill category animation override")]
        public int? SkillCategoryAnimationOverride { get; set; }

        [Value("slot")]
        public int? Slot { get; set; }

        [Value("value")]
        public int? Value { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("bare sword")]
        public object? BareSword { get; set; }

        [Value("ground mesh")]
        public object? GroundMesh { get; set; }

        [Value("icon")]
        public object? Icon { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("physics file")]
        public object? PhysicsFile { get; set; }

        [Value("sheath")]
        public object? Sheath { get; set; }

        [Value("scale from weight")]
        public bool? ScaleFromWeight { get; set; }

        [Value("craft time")]
        public float? CraftTime { get; set; }

        [Reference("race damage")]
        public IEnumerable<ItemReference<Race>> RaceDamage { get; set; }
    }
}