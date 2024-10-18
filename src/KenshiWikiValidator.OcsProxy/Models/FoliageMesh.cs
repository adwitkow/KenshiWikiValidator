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
    public class FoliageMesh : ItemBase
    {
        public FoliageMesh(ModItem item)
            : base(item)
        {
            this.Meshes = Enumerable.Empty<ItemReference<FoliageMesh>>();
            this.BuildingType = Enumerable.Empty<ItemReference<Building>>();
        }

        public override ItemType Type => ItemType.FoliageMesh;

        [Value("avoid towns")]
        public bool? AvoidTowns { get; set; }

        [Value("clustered")]
        public bool? Clustered { get; set; }

        [Value("floating")]
        public bool? Floating { get; set; }

        [Value("keep upright")]
        public bool? KeepUpright { get; set; }

        [Value("limit to grass areas")]
        public bool? LimitToGrassAreas { get; set; }

        [Value("slope align")]
        public bool? SlopeAlign { get; set; }

        [Value("use accurate trace")]
        public bool? UseAccurateTrace { get; set; }

        [Value("walkable")]
        public bool? Walkable { get; set; }

        [Value("child cluster radius")]
        public float? ChildClusterRadius { get; set; }

        [Value("cluster radius max")]
        public float? ClusterRadiusMax { get; set; }

        [Value("cluster radius min")]
        public float? ClusterRadiusMin { get; set; }

        [Value("grass spot")]
        public float? GrassSpot { get; set; }

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("max height")]
        public float? MaxHeight { get; set; }

        [Value("max slope")]
        public float? MaxSlope { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("min height")]
        public float? MinHeight { get; set; }

        [Value("min slope")]
        public float? MinSlope { get; set; }

        [Value("navmesh cutter")]
        public float? NavmeshCutter { get; set; }

        [Value("road avoidance")]
        public float? RoadAvoidance { get; set; }

        [Value("specular mult")]
        public float? SpecularMult { get; set; }

        [Value("tile X")]
        public float? TileX { get; set; }

        [Value("tile Y")]
        public float? TileY { get; set; }

        [Value("vertical offset max")]
        public float? VerticalOffsetMax { get; set; }

        [Value("vertical offset min")]
        public float? VerticalOffsetMin { get; set; }

        [Value("wind factor")]
        public float? WindFactor { get; set; }

        [Value("alpha threshold")]
        public int? AlphaThreshold { get; set; }

        [Value("cluster num max")]
        public int? ClusterNumMax { get; set; }

        [Value("cluster num min")]
        public int? ClusterNumMin { get; set; }

        [Value("leaves alpha threshold")]
        public int? LeavesAlphaThreshold { get; set; }

        [Value("material type")]
        public int? MaterialType { get; set; }

        [Value("collision")]
        public object? Collision { get; set; }

        [Value("leaves mesh")]
        public object? LeavesMesh { get; set; }

        [Value("leaves normal")]
        public object? LeavesNormal { get; set; }

        [Value("leaves texture")]
        public object? LeavesTexture { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("normal map")]
        public object? NormalMap { get; set; }

        [Value("normal map 2")]
        public object? NormalMap2 { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }

        [Value("texture map 2")]
        public object? TextureMap2 { get; set; }

        [Value("reflectivity")]
        public float? Reflectivity { get; set; }

        [Reference("meshes")]
        public IEnumerable<ItemReference<FoliageMesh>> Meshes { get; set; }

        [Reference("building type")]
        public IEnumerable<ItemReference<Building>> BuildingType { get; set; }
    }
}