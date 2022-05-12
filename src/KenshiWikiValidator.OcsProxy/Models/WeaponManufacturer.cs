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
    public class WeaponManufacturer : ItemBase
    {
        public WeaponManufacturer(string stringId, string name)
            : base(stringId, name)
        {
            this.WeaponModels = Enumerable.Empty<ItemReference<MaterialSpecsWeapon>>();
            this.WeaponTypes = Enumerable.Empty<ItemReference<Weapon>>();
        }

        public override ItemType Type => ItemType.WeaponManufacturer;

        [Value("blunt damage mod")]
        public float? BluntDamageMod { get; set; }

        [Value("cut damage mod")]
        public float? CutDamageMod { get; set; }

        [Value("price mod")]
        public float? PriceMod { get; set; }

        [Value("weight mod")]
        public float? WeightMod { get; set; }

        [Value("min cut damage")]
        public int? MinCutDamage { get; set; }

        [Value("company description")]
        public string? CompanyDescription { get; set; }

        [Reference("weapon models")]
        public IEnumerable<ItemReference<MaterialSpecsWeapon>> WeaponModels { get; set; }

        [Reference("weapon types")]
        public IEnumerable<ItemReference<Weapon>> WeaponTypes { get; set; }
    }
}