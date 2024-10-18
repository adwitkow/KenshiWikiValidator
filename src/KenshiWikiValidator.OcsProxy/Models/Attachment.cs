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
    public class Attachment : ItemBase
    {
        public Attachment(ModItem item)
            : base(item)
        {
        }

        public override ItemType Type => ItemType.Attachment;

        [Value("playable")]
        public bool? Playable { get; set; }

        [Value("mipmap bias")]
        public float? MipmapBias { get; set; }

        [Value("specular")]
        public float? Specular { get; set; }

        [Value("alpha rejection")]
        public int? AlphaRejection { get; set; }

        [Value("attach slot")]
        public int? AttachSlot { get; set; }

        [Value("hair alpha channel")]
        public int? HairAlphaChannel { get; set; }

        [Value("hair diffuse channel")]
        public int? HairDiffuseChannel { get; set; }

        [Value("head alpha channel")]
        public int? HeadAlphaChannel { get; set; }

        [Value("head alpha channel female")]
        public int? HeadAlphaChannelFemale { get; set; }

        [Value("head channel")]
        public int? HeadChannel { get; set; }

        [Value("head channel female")]
        public int? HeadChannelFemale { get; set; }

        [Value("head texture")]
        public object? HeadTexture { get; set; }

        [Value("head texture female")]
        public object? HeadTextureFemale { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Value("mesh female")]
        public object? MeshFemale { get; set; }

        [Value("texture map")]
        public object? TextureMap { get; set; }

        [Value("texture map female")]
        public object? TextureMapFemale { get; set; }

        [Value("head multiply")]
        public object? HeadMultiply { get; set; }

        [Value("head multiply female")]
        public object? HeadMultiplyFemale { get; set; }
    }
}