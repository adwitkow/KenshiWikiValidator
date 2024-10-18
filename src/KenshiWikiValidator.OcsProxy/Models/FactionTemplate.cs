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
    public class FactionTemplate : ItemBase
    {
        public FactionTemplate(ModItem item)
            : base(item)
        {
            this.Clothing = Enumerable.Empty<ItemReference<Armour>>();
            this.WeaponLevel = Enumerable.Empty<ItemReference<WeaponManufacturer>>();
            this.WeaponModels = Enumerable.Empty<ItemReference<Weapon>>();
        }

        public override ItemType Type => ItemType.FactionTemplate;

        [Value("strength/size balanced")]
        public bool? StrengthOrSizeBalanced { get; set; }

        [Value("face weirdness")]
        public float? FaceWeirdness { get; set; }

        [Value("armour cap")]
        public int? ArmourCap { get; set; }

        [Value("armour max")]
        public int? ArmourMax { get; set; }

        [Value("armour min")]
        public int? ArmourMin { get; set; }

        [Value("combat stats max")]
        public int? CombatStatsMax { get; set; }

        [Value("combat stats min")]
        public int? CombatStatsMin { get; set; }

        [Value("default relation")]
        public int? DefaultRelation { get; set; }

        [Value("fundamental type")]
        public int? FundamentalType { get; set; }

        [Value("leader increase max")]
        public int? LeaderIncreaseMax { get; set; }

        [Value("leader increase min")]
        public int? LeaderIncreaseMin { get; set; }

        [Value("leader levels max")]
        public int? LeaderLevelsMax { get; set; }

        [Value("leader levels min")]
        public int? LeaderLevelsMin { get; set; }

        [Value("skill cap")]
        public int? SkillCap { get; set; }

        [Value("squad size max")]
        public int? SquadSizeMax { get; set; }

        [Value("squad size min")]
        public int? SquadSizeMin { get; set; }

        [Reference("clothing")]
        public IEnumerable<ItemReference<Armour>> Clothing { get; set; }

        [Reference("weapon level")]
        public IEnumerable<ItemReference<WeaponManufacturer>> WeaponLevel { get; set; }

        [Reference("weapon models")]
        public IEnumerable<ItemReference<Weapon>> WeaponModels { get; set; }
    }
}