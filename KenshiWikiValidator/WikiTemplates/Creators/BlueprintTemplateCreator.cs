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
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.OcsProxy.Models.Interfaces;
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

        var research = this.GetUnlockingResearch(item);

        var color = item.Type switch
        {
            ItemType.Crossbow => "yellow",
            ItemType.Armour => "green",
            _ => "blue",
        };

        var templateProperties = new SortedList<string, string?>();
        if (research is null)
        {
            if (!this.HasBlueprints(item))
            {
                return null!;
            }

            if (item is not IDescriptive descriptive)
            {
                throw new InvalidOperationException("This creator should be called only for items that contain descriptions.");
            }

            templateProperties.Add("name", item.Name!);
            templateProperties.Add("color", color);
            templateProperties.Add("description", descriptive.Description);
            templateProperties.Add("level", "1");
            templateProperties.Add("value", "???");
            templateProperties.Add("prerequisites", string.Empty);
            templateProperties.Add("sell value", "???");
            templateProperties.Add("new items", item.Name!);
        }
        else
        {
            var cost = research.Money;
            var requirements = research.Requirements;
            var newItems = this.JoinNewItems(research);

            if (cost != 0)
            {
                templateProperties.Add("name", research.Name!);
                templateProperties.Add("color", color);
                templateProperties.Add("description", research.Description);
                templateProperties.Add("level", research.Level.GetValueOrDefault().ToString());
                templateProperties.Add("value", string.Format("{0:n0}", cost));
                templateProperties.Add("prerequisites", string.Join(", ", requirements.Select(req => $"[[{req.Item.Name} (Tech)]]")));
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

    private IEnumerable<IItem> JoinNewItems(Research research)
    {
        // Holy crap, this is god damn ugly.
        return research.EnableArmour
            .Select(reference => (IItem)reference.Item)
            .Concat(research.EnableBuildings
                .Select(reference => (IItem)reference.Item))
            .Concat(research.EnableCrossbow
                .Select(reference => (IItem)reference.Item))
            .Concat(research.EnableItem
                .Select(reference => (IItem)reference.Item))
            .Concat(research.EnableRobotics
                .Select(reference => (IItem)reference.Item))
            .Concat(research.EnableWeaponModel
                .Select(reference => (IItem)reference.Item))
            .Concat(research.EnableWeaponTypes
                .Select(reference => (IItem)reference.Item));
    }

    private Research? GetUnlockingResearch(IItem item)
    {
        var items = this.itemRepository.GetItems<Research>();
        return items
            .SingleOrDefault(research => research.EnableWeaponTypes
                .Any(weaponTypeRef => weaponTypeRef.Item == item));
    }

    private bool HasBlueprints(IItem item)
    {
        var vendorLists = this.itemRepository.GetItems<VendorList>()
                .Where(vendor => vendor.ArmourBlueprints.Any(armourBlueprintRef => armourBlueprintRef.Item == item)
                || vendor.Blueprints.Any(blueprintRef => blueprintRef.Item == item)
                || vendor.CrossbowBlueprints.Any(crossbowBlueprintRef => crossbowBlueprintRef.Item == item));
        var squads = this.itemRepository.GetItems<Squad>()
            .Where(squad => squad.Vendors
                .Any(vendorRef => vendorLists.Contains(vendorRef.Item)));
        return squads.Any();
    }
}
