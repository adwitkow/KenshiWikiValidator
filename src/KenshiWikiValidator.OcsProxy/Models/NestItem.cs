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
    public class NestItem : ItemBase, IDescriptive
    {
        public NestItem(ModItem item)
            : base(item)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
            this.Cluster = Enumerable.Empty<ItemReference<NestItem>>();
            this.Egg = Enumerable.Empty<ItemReference<AnimalCharacter>>();
        }

        public override ItemType Type => ItemType.NestItem;

        [Value("auto icon image")]
        public bool? AutoIconImage { get; set; }

        [Value("has collision")]
        public bool? HasCollision { get; set; }

        [Value("trade item")]
        public bool? TradeItem { get; set; }

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

        [Value("weight kg")]
        public float? WeightKg { get; set; }

        [Value("cluster max")]
        public int? ClusterMax { get; set; }

        [Value("cluster min")]
        public int? ClusterMin { get; set; }

        [Value("cluster range")]
        public int? ClusterRange { get; set; }

        [Value("inventory footprint height")]
        public int? InventoryFootprintHeight { get; set; }

        [Value("inventory footprint width")]
        public int? InventoryFootprintWidth { get; set; }

        [Value("number of meshes")]
        public int? NumberOfMeshes { get; set; }

        [Value("slot")]
        public int? Slot { get; set; }

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

        [Value("physics file")]
        public object? PhysicsFile { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }

        [Reference("cluster")]
        public IEnumerable<ItemReference<NestItem>> Cluster { get; set; }

        [Reference("egg")]
        public IEnumerable<ItemReference<AnimalCharacter>> Egg { get; set; }
    }
}