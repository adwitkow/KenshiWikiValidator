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
    public class EffectFogVolume : ItemBase
    {
        public EffectFogVolume(ModItem item)
            : base(item)
        {
        }

        public override ItemType Type => ItemType.EffectFogVolume;

        [Value("additive colour")]
        public bool? AdditiveColour { get; set; }

        [Value("ground movement")]
        public bool? GroundMovement { get; set; }

        [Value("alpha")]
        public float? Alpha { get; set; }

        [Value("distance")]
        public float? Distance { get; set; }

        [Value("position 2 x")]
        public float? Position2X { get; set; }

        [Value("position 2 y")]
        public float? Position2Y { get; set; }

        [Value("position 2 z")]
        public float? Position2Z { get; set; }

        [Value("position x")]
        public float? PositionX { get; set; }

        [Value("position y")]
        public float? PositionY { get; set; }

        [Value("position z")]
        public float? PositionZ { get; set; }

        [Value("radius")]
        public float? Radius { get; set; }

        [Value("colour")]
        public int? Colour { get; set; }

        [Value("type")]
        public int? EffectType { get; set; }
    }
}