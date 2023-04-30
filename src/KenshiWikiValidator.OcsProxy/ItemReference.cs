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

namespace KenshiWikiValidator.OcsProxy
{
    public readonly struct ItemReference<T> : IItemReference<T>
        where T : IItem
    {
        public ItemReference(T item, int value0, int value1, int value2)
        {
            this.Item = item;
            this.Value0 = value0;
            this.Value1 = value1;
            this.Value2 = value2;
        }

        public T Item { get; }

        public int Value0 { get; }

        public int Value1 { get; }

        public int Value2 { get; }
    }
}
