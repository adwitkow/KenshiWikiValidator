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
using KenshiWikiValidator.OcsProxy;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.WikiTemplates.Creators;

public class BlueprintTemplateCreator : ITemplateCreator
{
    private readonly IItemRepository itemRepository;
    private readonly ArticleData data;

    public BlueprintTemplateCreator(IItemRepository itemRepository, ArticleData data)
    {
        this.itemRepository = itemRepository;
        this.data = data;
    }

    public WikiTemplate Generate()
    {
        var stringId = this.data.StringIds.SingleOrDefault();
        if (string.IsNullOrEmpty(stringId))
        {
            return null!;
        }

        var item = this.itemRepository.GetItemByStringId(stringId);

        if (item is not IResearchable researchable)
        {
            throw new InvalidOperationException($"{item.Name} is not researchable.");
        }

        var color = item.Type switch
        {
            ItemType.Crossbow => "yellow",
            ItemType.Armour => "green",
            _ => "blue",
        };

        var templateProperties = new SortedList<string, string?>();
        if (researchable.UnlockingResearch is null)
        {
            if (!researchable.BlueprintSquads.Any())
            {
                return null!;
            }

            templateProperties.Add("name", item.Name!);
            templateProperties.Add("color", color);
            templateProperties.Add("description", item.Properties["description"].ToString());
            templateProperties.Add("level", "1");
            templateProperties.Add("value", "???");
            templateProperties.Add("prerequisites", string.Empty);
            templateProperties.Add("sell value", "???");
            templateProperties.Add("new items", item.Name!);
        }
        else
        {
            var research = this.itemRepository.GetDataItemByStringId(researchable.UnlockingResearch.StringId!);
            int cost = (int)research.Values["money"];

            var requirements = research.GetReferenceItems(this.itemRepository, "requirements");
            var newItems = research.ReferenceCategories.Values
                .Where(cat => cat.Key.StartsWith("enable"))
                .SelectMany(cat => cat.Values)
                .Select(reference => this.itemRepository.GetDataItemByStringId(reference.TargetId));
            var costs = research.GetReferences("cost")
                .ToDictionary(reference => reference, reference => this.itemRepository.GetDataItemByStringId(reference.TargetId));

            if (cost != 0)
            {
                templateProperties.Add("name", research.Name!);
                templateProperties.Add("color", color);
                templateProperties.Add("description", research.Values["description"].ToString());
                templateProperties.Add("level", research.Values["level"].ToString());
                templateProperties.Add("value", string.Format("{0:n0}", cost));
                templateProperties.Add("prerequisites", string.Join(", ", requirements.Select(req => $"[[{req.Name} (Tech)]]")));
                templateProperties.Add("sell value", string.Format("{0:n0}", cost / 4));
                templateProperties.Add("new items", string.Join(", ", newItems.Select(newItem => $"[[{newItem.Name}]]")));
            }
            else
            {
                return null!;
            }
        }

        var templateName = "Blueprint";

        return new WikiTemplate(templateName, templateProperties);
    }
}
