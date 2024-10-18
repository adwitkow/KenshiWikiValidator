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
    public class Light : ItemBase
    {
        public Light(ModItem item)
            : base(item)
        {
        }

        public override ItemType Type => ItemType.Light;

        [Value("buildings")]
        public bool? Buildings { get; set; }

        [Value("characters")]
        public bool? Characters { get; set; }

        [Value("landscape")]
        public bool? Landscape { get; set; }

        [Value("brightness")]
        public float? Brightness { get; set; }

        [Value("falloff")]
        public float? Falloff { get; set; }

        [Value("inner")]
        public float? Inner { get; set; }

        [Value("outer")]
        public float? Outer { get; set; }

        [Value("radius")]
        public float? Radius { get; set; }

        [Value("variance")]
        public float? Variance { get; set; }

        [Value("diffuse")]
        public int? Diffuse { get; set; }

        [Value("effect")]
        public int? Effect { get; set; }

        [Value("type")]
        public int? LightType { get; set; }

        [Value("specular")]
        public int? Specular { get; set; }
    }
}