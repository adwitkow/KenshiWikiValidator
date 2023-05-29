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

using System.Collections;
using System.Collections.Specialized;

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable S2365 // Properties should not make collection or array copies
namespace KenshiWikiValidator.BaseComponents
{
    [Serializable]
    public sealed class IndexedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly OrderedDictionary backing;

        public IndexedDictionary()
        {
            this.backing = new OrderedDictionary();
        }

        public IndexedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> baseDict)
            : this()
        {
            foreach (var pair in baseDict)
            {
                this.Add(pair);
            }
        }

        public int Count
        {
            get { return this.backing.Count; }
        }

        public bool IsReadOnly
        {
            get { return this.backing.IsReadOnly; }
        }

        public ICollection<TKey> Keys
        {
            get => this.backing.Keys.OfType<TKey>().ToList();
        }

        public ICollection<TValue> Values
        {
            get => this.backing.Values.OfType<TValue>().ToList();
        }

        private IEnumerable<KeyValuePair<TKey, TValue>> KeyValuePairs
        {
            get
            {
                return this.backing.OfType<DictionaryEntry>()
                    .Select(e => new KeyValuePair<TKey, TValue>((TKey)e.Key, (TValue)e.Value));
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (this.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                throw new KeyNotFoundException();
            }

            set => this.backing[key] = value;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.KeyValuePairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.backing[item.Key] = item.Value;
        }

        public void Clear()
        {
            this.backing.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.backing.Contains(item.Key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            this.KeyValuePairs.ToList().CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (this.TryGetValue(item.Key, out TValue value)
                && Equals(value, item.Value))
            {
                this.Remove(item.Key);
                return true;
            }

            return false;
        }

        public bool ContainsKey(TKey key)
        {
            return this.backing.Contains(key);
        }

        public void Add(TKey key, TValue value)
        {
            this.backing.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            var result = this.backing.Contains(key);
            if (result)
            {
                this.backing.Remove(key);
            }

            return result;
        }

        public void Insert(int index, TKey key, TValue value)
        {
            this.backing.Insert(index, key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            object? foundValue;
            if ((foundValue = this.backing[key]) != null
                || this.backing.Contains(key))
            {
                // Either found with a non-null value, or contained value is null.
                value = (TValue)foundValue;
                return true;
            }

            value = default;
            return false;
        }
    }
}
