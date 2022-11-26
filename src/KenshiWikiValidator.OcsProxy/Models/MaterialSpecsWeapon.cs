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

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class MaterialSpecsWeapon : ItemBase, IDescriptive
    {
        public MaterialSpecsWeapon(string stringId, string name)
            : base(stringId, name)
        {
        }

        public override ItemType Type => ItemType.MaterialSpecsWeapon;

        [Value("craft list fixed")]
        public bool? CraftListFixed { get; set; }

        [Value("overall scale")]
        public float? OverallScale { get; set; }

        [Value("scale length")]
        public float? ScaleLength { get; set; }

        [Value("scale thickness")]
        public float? ScaleThickness { get; set; }

        [Value("scale width")]
        public float? ScaleWidth { get; set; }

        [Value("specular mult")]
        public float? SpecularMult { get; set; }

        [Value("attack mod")]
        public int? AttackMod { get; set; }

        [Value("defence mod")]
        public int? DefenceMod { get; set; }

        [Value("description")]
        public string? Description { get; set; }

        [Value("metalness map")]
        public object? MetalnessMap { get; set; }

        [Value("normal map")]
        public object? NormalMap { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }
    }
}