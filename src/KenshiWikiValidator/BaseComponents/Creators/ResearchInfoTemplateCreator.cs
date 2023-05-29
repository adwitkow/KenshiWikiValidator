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

using KenshiWikiValidator.BaseComponents;

namespace KenshiWikiValidator.BaseComponents.Creators
{
    public class ResearchInfoTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Research info";

        public string? ResearchName { get; set; }

        public string? Icon { get; set; }

        public string? Description { get; set; }

        public int Time { get; set; }

        public int TechLevel { get; set; }

        public IEnumerable<string> Costs { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> Prerequisites { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> NewBuildings { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> NewItems { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> RequiredFor { get; set; } = Enumerable.Empty<string>();

        public WikiTemplate Generate(ArticleData data)
        {
            var prerequisites = string.Join(", ", this.Prerequisites.Select(item => $"[[{item}]]"));
            var newBuildings = string.Join(", ", this.NewBuildings.Select(item => $"[[{item}]]"));
            var newItems = string.Join(", ", this.NewItems.Select(item => $"[[{item}]]"));
            var requiredFor = string.Join(", ", this.RequiredFor.Select(item => $"[[{item}]]"));
            var costs = string.Join(", ", this.Costs);

            var properties = new IndexedDictionary<string, string?>()
            {
                { "name", this.ResearchName },
                { "image", this.Icon },
                { "description", this.Description },
                { "time", this.Time.ToString() },
                { "level", this.TechLevel.ToString() },
                { "prerequisites", prerequisites },
                { "new_bldgs", newBuildings },
                { "new_items", newItems },
                { "costs", costs },
                { "required_for", requiredFor },
            };

            return new WikiTemplate(TemplateName, properties);
        }
    }
}
