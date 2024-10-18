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
    public class ItemPlacementGroup : ItemBase
    {
        public ItemPlacementGroup(ModItem item)
            : base(item)
        {
            this.Items = Enumerable.Empty<ItemReference<Item>>();
            this.WeaponManufacturers = Enumerable.Empty<ItemReference<WeaponManufacturer>>();
            this.Weapons = Enumerable.Empty<ItemReference<Weapon>>();
            this.Clothing = Enumerable.Empty<ItemReference<Armour>>();
        }

        public override ItemType Type => ItemType.ItemPlacementGroup;

        [Value("random yaw")]
        public bool? RandomYaw { get; set; }

        [Value("max respawn time")]
        public int? MaxRespawnTime { get; set; }

        [Value("min respawn time")]
        public int? MinRespawnTime { get; set; }

        [Value("placeholder")]
        public string? Placeholder { get; set; }

        [Reference("items")]
        public IEnumerable<ItemReference<Item>> Items { get; set; }

        [Reference("weapon manufacturers")]
        public IEnumerable<ItemReference<WeaponManufacturer>> WeaponManufacturers { get; set; }

        [Reference("weapons")]
        public IEnumerable<ItemReference<Weapon>> Weapons { get; set; }

        [Reference("clothing")]
        public IEnumerable<ItemReference<Armour>> Clothing { get; set; }
    }
}