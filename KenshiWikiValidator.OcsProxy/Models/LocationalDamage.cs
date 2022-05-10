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
    public class LocationalDamage : ItemBase
    {
        public LocationalDamage(string stringId, string name)
            : base(stringId, name)
        {
            this.InjuryAnims = Enumerable.Empty<ItemReference<Animation>>();
            this.PainAnim = Enumerable.Empty<ItemReference<Animation>>();
        }

        public override ItemType Type => ItemType.LocationalDamage;

        [Value("collapses")]
        public bool? Collapses { get; set; }

        [Value("death")]
        public bool? Death { get; set; }

        [Value("severance")]
        public bool? Severance { get; set; }

        [Value("affects move speed")]
        public float? AffectsMoveSpeed { get; set; }

        [Value("affects skills")]
        public float? AffectsSkills { get; set; }

        [Value("KO mult")]
        public float? KoMult { get; set; }

        [Value("body part type")]
        public int? BodyPartType { get; set; }

        [Value("collapse part")]
        public int? CollapsePart { get; set; }

        [Value("bone name")]
        public string? BoneName { get; set; }

        [Value("bone name 2")]
        public string? BoneName2 { get; set; }

        [Value("bone name 3")]
        public string? BoneName3 { get; set; }

        [Reference("injury anims")]
        public IEnumerable<ItemReference<Animation>> InjuryAnims { get; set; }

        [Reference("pain anim")]
        public IEnumerable<ItemReference<Animation>> PainAnim { get; set; }
    }
}