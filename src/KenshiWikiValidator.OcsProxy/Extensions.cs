﻿// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
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
    public static class Extensions
    {
        public static bool ContainsItem<T>(this IEnumerable<ItemReference<T>> references, IItem item)
            where T : IItem
        {
            return references.TryGetReference(item, out _);
        }

        public static bool TryGetReference<T>(this IEnumerable<ItemReference<T>> references, IItem item, out ItemReference<T> reference)
            where T : IItem
        {
            reference = references.FirstOrDefault(reference => ReferenceEquals(item, reference.Item));

            return reference.Item is not null;
        }

        public static IEnumerable<TResult> SelectItems<T, TResult>(
            this IEnumerable<T> items,
            Func<T, IEnumerable<ItemReference<TResult>>> func)
            where T : IItem
            where TResult : IItem
        {
            return items.SelectMany(item => func(item)
                    .Select(townReference => townReference.Item));
        }
    }
}
