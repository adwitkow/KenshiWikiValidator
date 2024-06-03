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
    public class Camera : ItemBase
    {
        public Camera(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.Camera;

        [Value("grass")]
        public bool? Grass { get; set; }

        [Value("density")]
        public float? Density { get; set; }

        [Value("max height")]
        public float? MaxHeight { get; set; }

        [Value("max width")]
        public float? MaxWidth { get; set; }

        [Value("min height")]
        public float? MinHeight { get; set; }

        [Value("min width")]
        public float? MinWidth { get; set; }

        [Value("lod range")]
        public int? LodRange { get; set; }

        [Value("Collision")]
        public object? Collision { get; set; }

        [Value("color map")]
        public object? ColorMap { get; set; }

        [Value("Mesh")]
        public object? Mesh { get; set; }
    }
}