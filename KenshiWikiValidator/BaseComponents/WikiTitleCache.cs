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

using KenshiWikiValidator.OcsProxy;

namespace KenshiWikiValidator.BaseComponents
{
    public class WikiTitleCache
    {
        private readonly Dictionary<string, string> data;

        public WikiTitleCache()
        {
            this.data = new Dictionary<string, string>();
        }

        public bool HasArticle(IItem item)
        {
            return this.HasArticle(item.StringId);
        }

        public bool HasArticle(string stringId)
        {
            return this.data.ContainsKey(stringId);
        }

        public string GetTitle(IItem item)
        {
            return this.GetTitle(item.StringId, item.Name);
        }

        public string GetTitle(string stringId, string itemName)
        {
            var success = this.data.TryGetValue(stringId, out var title);
            if (success)
            {
                return title!;
            }
            else
            {
                return itemName;
            }
        }

        public void AddTitle(string stringId, string title)
        {
            var exists = this.data.TryGetValue(stringId, out var existingValue);

            if (exists && title.Equals(existingValue))
            {
                return;
            }

            this.data.Add(stringId, title);
        }
    }
}