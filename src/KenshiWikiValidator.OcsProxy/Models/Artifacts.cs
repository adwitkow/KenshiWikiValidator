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
    public class Artifacts : ItemBase
    {
        public Artifacts(string stringId, string name)
            : base(stringId, name)
        {
            this.Armours = Enumerable.Empty<ItemReference<Armour>>();
            this.ArmoursHq = Enumerable.Empty<ItemReference<Armour>>();
            this.Crossbows = Enumerable.Empty<ItemReference<Crossbow>>();
            this.Items = Enumerable.Empty<ItemReference<Item>>();
            this.Robotics = Enumerable.Empty<ItemReference<LimbReplacement>>();
            this.Weapons = Enumerable.Empty<ItemReference<MaterialSpecsWeapon>>();
        }

        public override ItemType Type => ItemType.Artifacts;

        [Reference("armours")]
        public IEnumerable<ItemReference<Armour>> Armours { get; set; }

        [Reference("armours hq")]
        public IEnumerable<ItemReference<Armour>> ArmoursHq { get; set; }

        [Reference("crossbows")]
        public IEnumerable<ItemReference<Crossbow>> Crossbows { get; set; }

        [Reference("items")]
        public IEnumerable<ItemReference<Item>> Items { get; set; }

        [Reference("robotics")]
        public IEnumerable<ItemReference<LimbReplacement>> Robotics { get; set; }

        [Reference("weapons")]
        public IEnumerable<ItemReference<MaterialSpecsWeapon>> Weapons { get; set; }
    }
}