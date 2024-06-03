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
    public class MapFeatures : ItemBase
    {
        public MapFeatures(string stringId, string name)
            : base(stringId, name)
        {
            this.BirdAttractor = Enumerable.Empty<ItemReference<WildlifeBirds>>();
        }

        public override ItemType Type => ItemType.MapFeatures;

        [Value("hidden")]
        public bool? Hidden { get; set; }

        [Value("local coordinates")]
        public bool? LocalCoordinates { get; set; }

        [Value("distance")]
        public float? Distance { get; set; }

        [Value("specular mult")]
        public float? SpecularMult { get; set; }

        [Value("tile X")]
        public float? TileX { get; set; }

        [Value("tile Y")]
        public float? TileY { get; set; }

        [Value("alpha threshold")]
        public int? AlphaThreshold { get; set; }

        [Value("texture mode")]
        public int? TextureMode { get; set; }

        [Value("collision")]
        public object? Collision { get; set; }

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

        [Reference("bird attractor")]
        public IEnumerable<ItemReference<WildlifeBirds>> BirdAttractor { get; set; }
    }
}