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

using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Models
{
    public class CharacterPhysicsAttachment : ItemBase
    {
        public CharacterPhysicsAttachment(string stringId, string name)
            : base(stringId, name)
        {
            this.LightData = Enumerable.Empty<ItemReference<Light>>();
            this.Material = Enumerable.Empty<ItemReference<MaterialSpecsClothing>>();
        }

        public override ItemType Type => ItemType.CharacterPhysicsAttachment;

        [Value("bone name")]
        public string? BoneName { get; set; }

        [Value("file female")]
        public object? FileFemale { get; set; }

        [Value("file male")]
        public object? FileMale { get; set; }

        [Reference("light data")]
        public IEnumerable<ItemReference<Light>> LightData { get; set; }

        [Reference("material")]
        public IEnumerable<ItemReference<MaterialSpecsClothing>> Material { get; set; }
    }
}