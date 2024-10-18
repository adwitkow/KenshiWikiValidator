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

using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class BuildingPart : ItemBase
    {
        public BuildingPart(ModItem item)
            : base(item)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
            this.Parts = Enumerable.Empty<ItemReference<BuildingPart>>();
            this.MaterialMatch = Enumerable.Empty<ItemReference<BuildingPart>>();
        }

        public override ItemType Type => ItemType.BuildingPart;

        [Value("above ground")]
        public bool? AboveGround { get; set; }

        [Value("affects footprint")]
        public bool? AffectsFootprint { get; set; }

        [Value("is door")]
        public bool? IsDoor { get; set; }

        [Value("is for position marker")]
        public bool? IsForPositionMarker { get; set; }

        [Value("is stairs")]
        public bool? IsStairs { get; set; }

        [Value("is unwalkable roof")]
        public bool? IsUnwalkableRoof { get; set; }

        [Value("passable")]
        public bool? Passable { get; set; }

        [Value("density")]
        public float? Density { get; set; }

        [Value("footprint vertical")]
        public float? FootprintVertical { get; set; }

        [Value("offset X")]
        public float? OffsetX { get; set; }

        [Value("offset Y")]
        public float? OffsetY { get; set; }

        [Value("offset Z")]
        public float? OffsetZ { get; set; }

        [Value("rotation speed max")]
        public float? RotationSpeedMax { get; set; }

        [Value("rotation speed min")]
        public float? RotationSpeedMin { get; set; }

        [Value("wind speed rotation danger")]
        public float? WindSpeedRotationDanger { get; set; }

        [Value("wind speed rotation max")]
        public float? WindSpeedRotationMax { get; set; }

        [Value("wind speed rotation min")]
        public float? WindSpeedRotationMin { get; set; }

        [Value("building floor")]
        public int? BuildingFloor { get; set; }

        [Value("ground type")]
        public int? GroundType { get; set; }

        [Value("material match base chance")]
        public int? MaterialMatchBaseChance { get; set; }

        [Value("material match chance")]
        public int? MaterialMatchChance { get; set; }

        [Value("rotation axis")]
        public int? RotationAxis { get; set; }

        [Value("rotation function")]
        public int? RotationFunction { get; set; }

        [Value("destroyed collision")]
        public object? DestroyedCollision { get; set; }

        [Value("destroyed mesh")]
        public object? DestroyedMesh { get; set; }

        [Value("phs or mesh")]
        public object? PhsOrMesh { get; set; }

        [Value("xml collision")]
        public object? XmlCollision { get; set; }

        [Value("physics material")]
        public int? PhysicsMaterial { get; set; }

        [Value("door slide x")]
        public float? DoorSlideX { get; set; }

        [Value("door slide y")]
        public float? DoorSlideY { get; set; }

        [Value("door slide z")]
        public float? DoorSlideZ { get; set; }

        [Value("rotor radius")]
        public float? RotorRadius { get; set; }

        [Value("wind speed efficiency max")]
        public float? WindSpeedEfficiencyMax { get; set; }

        [Value("wind speed efficiency min")]
        public float? WindSpeedEfficiencyMin { get; set; }

        [Value("repeatable name")]
        public string? RepeatableName { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }

        [Reference("parts")]
        public IEnumerable<ItemReference<BuildingPart>> Parts { get; set; }

        [Reference("material match")]
        public IEnumerable<ItemReference<BuildingPart>> MaterialMatch { get; set; }
    }
}