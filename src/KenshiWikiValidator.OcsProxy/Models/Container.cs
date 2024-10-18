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
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class Container : ItemBase, IDescriptive
    {
        public Container(ModItem item)
            : base(item)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpecsClothing>>();
            this.Color = Enumerable.Empty<ItemReference<ColorData>>();
            this.PhysicsAttachment = Enumerable.Empty<ItemReference<CharacterPhysicsAttachment>>();
            this.Races = Enumerable.Empty<ItemReference<Race>>();
        }

        public override ItemType Type => ItemType.Container;

        [Value("auto icon image")]
        public bool? AutoIconImage { get; set; }

        [Value("dont colorise")]
        public bool? DontColorise { get; set; }

        [Value("has collision")]
        public bool? HasCollision { get; set; }

        [Value("athletics mult")]
        public float? AthleticsMult { get; set; }

        [Value("combat speed mult")]
        public float? CombatSpeedMult { get; set; }

        [Value("encumbrance effect")]
        public float? EncumbranceEffect { get; set; }

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

        [Value("stackable bonus mult")]
        public float? StackableBonusMult { get; set; }

        [Value("stealth mult")]
        public float? StealthMult { get; set; }

        [Value("weight kg")]
        public float? WeightKg { get; set; }

        [Value("combat skill bonus")]
        public int? CombatSkillBonus { get; set; }

        [Value("inventory footprint height")]
        public int? InventoryFootprintHeight { get; set; }

        [Value("inventory footprint width")]
        public int? InventoryFootprintWidth { get; set; }

        [Value("inventory sound")]
        public int? InventorySound { get; set; }

        [Value("slot")]
        public int? Slot { get; set; }

        [Value("stackable bonus minimum")]
        public int? StackableBonusMinimum { get; set; }

        [Value("storage size height")]
        public int? StorageSizeHeight { get; set; }

        [Value("storage size width")]
        public int? StorageSizeWidth { get; set; }

        [Value("value")]
        public int? Value { get; set; }

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

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpecsClothing>> Material { get; set; }

        [Reference("color")]
        public IEnumerable<ItemReference<ColorData>> Color { get; set; }

        [Reference("physics attachment")]
        public IEnumerable<ItemReference<CharacterPhysicsAttachment>> PhysicsAttachment { get; set; }

        [Reference("races")]
        public IEnumerable<ItemReference<Race>> Races { get; set; }
    }
}