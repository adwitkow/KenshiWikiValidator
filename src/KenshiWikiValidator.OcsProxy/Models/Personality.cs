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
    public class Personality : ItemBase
    {
        public Personality(ModItem item)
            : base(item)
        {
        }

        public override ItemType Type => ItemType.Personality;

        [Value("tags always0")]
        public int? TagsAlways0 { get; set; }

        [Value("tags common0")]
        public int? TagsCommon0 { get; set; }

        [Value("tags never0")]
        public int? TagsNever0 { get; set; }

        [Value("tags rare0")]
        public int? TagsRare0 { get; set; }

        [Value("tags never1")]
        public int? TagsNever1 { get; set; }

        [Value("tags never2")]
        public int? TagsNever2 { get; set; }

        [Value("tags never3")]
        public int? TagsNever3 { get; set; }

        [Value("tags common1")]
        public int? TagsCommon1 { get; set; }

        [Value("tags common2")]
        public int? TagsCommon2 { get; set; }

        [Value("tags common3")]
        public int? TagsCommon3 { get; set; }

        [Value("tags common4")]
        public int? TagsCommon4 { get; set; }

        [Value("tags common5")]
        public int? TagsCommon5 { get; set; }

        [Value("tags rare1")]
        public int? TagsRare1 { get; set; }

        [Value("tags common6")]
        public int? TagsCommon6 { get; set; }

        [Value("tags rare2")]
        public int? TagsRare2 { get; set; }

        [Value("tags always1")]
        public int? TagsAlways1 { get; set; }

        [Value("tags always2")]
        public int? TagsAlways2 { get; set; }
    }
}