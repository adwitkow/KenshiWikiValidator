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
    public class CraftingTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Crafting";

        public string? BuildingName { get; set; }

        public (string Name, int Amount) Input1 { get; set; }

        public (string Name, int Amount)? Input2 { get; set; }

        public string? Output { get; set; }

        public string? ImageSettings { get; set; }

        public bool Collapsed { get; set; }

        public WikiTemplate Generate(ArticleData data)
        {
            var unnamedProperties = new SortedSet<string>();
            var properties = new SortedList<string, string?>()
            {
                { "building", this.BuildingName },
                { "input0", this.Input1.Name },
                { "input0amount", this.Input1.Amount.ToString() },
                { "input1", this.Input2?.Name },
                { "input1amount", this.Input2?.Amount.ToString() },
                { "imagesettings", this.ImageSettings },
                { "output", this.Output },
            };

            if (this.Collapsed)
            {
                unnamedProperties.Add("collapsed");
            }

            return new WikiTemplate(TemplateName, unnamedProperties, properties);
        }
    }
}
