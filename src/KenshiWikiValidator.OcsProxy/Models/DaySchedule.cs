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
    public class DaySchedule : ItemBase
    {
        public DaySchedule(ModItem item)
            : base(item)
        {
            this.Building = Enumerable.Empty<ItemReference<Building>>();
        }

        public override ItemType Type => ItemType.DaySchedule;

        [Value("layout exterior")]
        public string? LayoutExterior { get; set; }

        [Value("layout interior")]
        public string? LayoutInterior { get; set; }

        [Reference("building")]
        public IEnumerable<ItemReference<Building>> Building { get; set; }
    }
}