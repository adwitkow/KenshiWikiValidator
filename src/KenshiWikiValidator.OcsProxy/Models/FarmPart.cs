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
    public class FarmPart : ItemBase
    {
        public FarmPart(string stringId, string name)
            : base(stringId, name)
        {
            this.Material = Enumerable.Empty<ItemReference<MaterialSpec>>();
        }

        public override ItemType Type => ItemType.FarmPart;

        [Value("delay")]
        public float? Delay { get; set; }

        [Value("offset end")]
        public float? OffsetEnd { get; set; }

        [Value("offset start")]
        public float? OffsetStart { get; set; }

        [Value("scale end")]
        public float? ScaleEnd { get; set; }

        [Value("scale start")]
        public float? ScaleStart { get; set; }

        [Value("scale variance")]
        public float? ScaleVariance { get; set; }

        [Value("mesh")]
        public object? Mesh { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpec>> Material { get; set; }
    }
}