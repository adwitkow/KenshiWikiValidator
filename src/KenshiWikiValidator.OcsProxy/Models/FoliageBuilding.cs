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

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class FoliageBuilding : ItemBase
    {
        public FoliageBuilding(string stringId, string name)
            : base(stringId, name)
        {
            this.Building = Enumerable.Empty<ItemReference<Building>>();
        }

        public override ItemType Type => ItemType.FoliageBuilding;

        [Value("clustered")]
        public bool? Clustered { get; set; }

        [Value("keep upright")]
        public bool? KeepUpright { get; set; }

        [Value("limit to grass areas")]
        public bool? LimitToGrassAreas { get; set; }

        [Value("slope align")]
        public bool? SlopeAlign { get; set; }

        [Value("use accurate trace")]
        public bool? UseAccurateTrace { get; set; }

        [Value("child cluster radius")]
        public float? ChildClusterRadius { get; set; }

        [Value("cluster radius max")]
        public float? ClusterRadiusMax { get; set; }

        [Value("cluster radius min")]
        public float? ClusterRadiusMin { get; set; }

        [Value("max altitude")]
        public float? MaxAltitude { get; set; }

        [Value("max slope")]
        public float? MaxSlope { get; set; }

        [Value("min altitude")]
        public float? MinAltitude { get; set; }

        [Value("min slope")]
        public float? MinSlope { get; set; }

        [Value("vertical offset max")]
        public float? VerticalOffsetMax { get; set; }

        [Value("vertical offset min")]
        public float? VerticalOffsetMin { get; set; }

        [Value("wind factor")]
        public float? WindFactor { get; set; }

        [Value("cluster num max")]
        public int? ClusterNumMax { get; set; }

        [Value("cluster num min")]
        public int? ClusterNumMin { get; set; }

        [Reference("building")]
        public IEnumerable<ItemReference<Building>> Building { get; set; }
    }
}