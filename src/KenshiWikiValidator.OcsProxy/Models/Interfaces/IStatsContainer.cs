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

namespace KenshiWikiValidator.OcsProxy.Models.Interfaces
{
    public interface IStatsContainer
    {
        public int? StatsRandomise { get; set; }

        public int? StealthStats { get; set; }

        public int? Strength { get; set; }

        public int? UnarmedStats { get; set; }

        public int? RangedStats { get; set; }

        public int? CombatStats { get; set; }

        public IEnumerable<ItemReference<Stats>> Stats { get; set; }
    }
}
