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
    public class EnvironmentResources : ItemBase
    {
        public EnvironmentResources(ModItem item)
            : base(item)
        {
        }

        public override ItemType Type => ItemType.EnvironmentResources;

        [Value("arid")]
        public float? Arid { get; set; }

        [Value("carbon mult")]
        public float? CarbonMult { get; set; }

        [Value("copper mult")]
        public float? CopperMult { get; set; }

        [Value("farming min")]
        public float? FarmingMin { get; set; }

        [Value("farming mult")]
        public float? FarmingMult { get; set; }

        [Value("green")]
        public float? Green { get; set; }

        [Value("iron mult")]
        public float? IronMult { get; set; }

        [Value("stone mult")]
        public float? StoneMult { get; set; }

        [Value("stone noise cutoff")]
        public float? StoneNoiseCutoff { get; set; }

        [Value("stone noise zoom")]
        public float? StoneNoiseZoom { get; set; }

        [Value("swamp")]
        public float? Swamp { get; set; }

        [Value("water min")]
        public float? WaterMin { get; set; }

        [Value("water mult")]
        public float? WaterMult { get; set; }

        [Value("farming altitude fade")]
        public int? FarmingAltitudeFade { get; set; }

        [Value("farming altitude max")]
        public int? FarmingAltitudeMax { get; set; }

        [Value("farming altitude min")]
        public int? FarmingAltitudeMin { get; set; }

        [Value("water altitude fade")]
        public int? WaterAltitudeFade { get; set; }

        [Value("water altitude max")]
        public int? WaterAltitudeMax { get; set; }

        [Value("water altitude min")]
        public int? WaterAltitudeMin { get; set; }
    }
}