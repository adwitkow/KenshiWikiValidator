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
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.BaseComponents.Creators
{
    public class ItemInfoboxTemplateCreator : ITemplateCreator
    {
        private readonly IItemRepository itemRepository;

        public ItemInfoboxTemplateCreator(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public WikiTemplate? Generate(ArticleData data)
        {
            var stringIds = data.GetAllPossibleStringIds();

            if (!stringIds.Any())
            {
                return null;
            }

            var dataItem = this.itemRepository.GetItemByStringId(stringIds.Single());

            ItemInfoboxParameters parameters;
            if (dataItem is Item item)
            {
                parameters = CreateItemParameters(item);
            }
            else if (dataItem is MapItem mapItem)
            {
                parameters = CreateItemParameters(mapItem);
            }
            else
            {
                throw new InvalidOperationException("Invalid item type to create the item infobox.");
            }

            var properties = new IndexedDictionary<string, string?>
            {
                { "icon", parameters.Icon },
                { "type", parameters.Type },
                { "nutrition", parameters.Nutrition },
                { "ingredients", parameters.Ingredients },
                { "weight", parameters.Weight },
                { "value", parameters.Value },
                { "sell value", parameters.SellValue },
                { "avgprice", parameters.AveragePrice },
                { "markup", parameters.Markup },
                { "charges", parameters.Charges },
                { "quality", parameters.Quality },
                { "description", parameters.Description },
                { "string id", parameters.StringId },
            };

            if (string.IsNullOrEmpty(parameters.TemplateName))
            {
                throw new InvalidOperationException("Trying to create a template without a name.");
            }

            return new WikiTemplate(parameters.TemplateName, properties);
        }

        private static ItemInfoboxParameters CreateItemParameters(MapItem mapItem)
        {
            return new ItemInfoboxParameters()
            {
                TemplateName = "Map item",
                Icon = GetIcon(mapItem.Icon),
                Weight = mapItem.WeightKg.ToString(),
                Value = mapItem.Value.ToString(),
                SellValue = (mapItem.Value / 2).ToString(),
                Description = mapItem.Description,
                StringId = mapItem.StringId,
            };
        }

        private static ItemInfoboxParameters CreateItemParameters(Item item)
        {
            throw new NotImplementedException();
        }

        private static string GetIcon(string? iconPath)
        {
            if (string.IsNullOrWhiteSpace(iconPath))
            {
                return string.Empty;
            }

            var segments = iconPath.Split(Path.DirectorySeparatorChar);

            return segments[segments.Length - 1];
        }

        private record struct ItemInfoboxParameters
        {
            public string? TemplateName { get; init; }

            public string? Icon { get; init; }

            public string? Type { get; init; }

            public string? Nutrition { get; init; }

            public string? Ingredients { get; init; }

            public string? Weight { get; init; }

            public string? Value { get; init; }

            public string? SellValue { get; init; }

            public string? AveragePrice { get; init; }

            public string? Markup { get; init; }

            public string? Charges { get; init; }

            public string? Quality { get; init; }

            public string? Description { get; init; }

            public string? StringId { get; init; }
        }
    }
}
