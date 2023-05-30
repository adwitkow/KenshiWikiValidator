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

namespace KenshiWikiValidator.BaseComponents
{
    public class ArticleData
    {
        public ArticleData()
        {
            this.WikiTemplates = new List<WikiTemplate>();
            this.StringIds = new List<string>();
            this.Categories = new List<string>();
            this.Sections = new List<string>();
            this.PotentialStringId = string.Empty;
        }

        public ICollection<string> StringIds { get; set; }

        public ICollection<string> Categories { get; set; }

        public ICollection<string> Sections { get; set; }

        public IEnumerable<WikiTemplate> WikiTemplates { get; set; }

        public string PotentialStringId { get; set; }

        public IEnumerable<string> GetAllPossibleStringIds()
        {
            var stringIds = this.StringIds;
            if (!stringIds.Any() && !string.IsNullOrEmpty(this.PotentialStringId))
            {
                stringIds = new[] { this.PotentialStringId };
            }

            return stringIds;
        }
    }
}
