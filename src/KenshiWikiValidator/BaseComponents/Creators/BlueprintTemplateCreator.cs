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

using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.OcsProxy.Models.Interfaces;

namespace KenshiWikiValidator.BaseComponents.Creators;

public class BlueprintTemplateCreator : ITemplateCreator
{
    private readonly IItemRepository itemRepository;

    public BlueprintTemplateCreator(IItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public WikiTemplate Generate(ArticleData data)
    {
        var stringId = data.StringIds.SingleOrDefault();
        if (string.IsNullOrEmpty(stringId))
        {
            return null!;
        }

        var item = this.itemRepository.GetItemByStringId(stringId);

        var research = this.GetUnlockingResearch(item);

        var color = item switch
        {
            Crossbow _ => "yellow",
            Armour _ => "green",
            _ => "blue",
        };

        var templateProperties = new IndexedDictionary<string, string?>();
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

            templateProperties.Add("color", color);
            templateProperties.Add("description", descriptive.Description);
            templateProperties.Add("level", "1");
            templateProperties.Add("name", item.Name!);
            templateProperties.Add("new items", item.Name!);
            templateProperties.Add("prerequisites", string.Empty);
            templateProperties.Add("sell value", "???");
            templateProperties.Add("value", "???");
        }
        else
        {
            var cost = research.Money;
            var requirements = research.Requirements;
            var newItems = this.JoinNewItems(research);

            if (cost != 0)
            {
                templateProperties.Add("color", color);
                templateProperties.Add("description", research.Description);
                templateProperties.Add("level", research.Level.GetValueOrDefault().ToString());
                templateProperties.Add("name", research.Name!);
                templateProperties.Add("new items", string.Join(", ", newItems.Select(newItem => $"[[{newItem.Name}]]")));
                templateProperties.Add("prerequisites", string.Join(", ", requirements.Select(req => $"[[{req.Item.Name} (Tech)]]")));
                templateProperties.Add("sell value", string.Format("{0:n0}", cost / 4));
                templateProperties.Add("value", string.Format("{0:n0}", cost));
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
        return this.itemRepository
            .GetItems<Research>()
            .SingleOrDefault(research => research.EnableWeaponTypes.ContainsItem(item));
    }

    private bool HasBlueprints(IItem item)
    {
        var vendorLists = this.itemRepository.GetItems<VendorList>()
                .Where(vendor => vendor.ArmourBlueprints.ContainsItem(item)
                || vendor.Blueprints.ContainsItem(item)
                || vendor.CrossbowBlueprints.ContainsItem(item));
        var squads = this.itemRepository.GetItems<Squad>()
            .Where(squad => squad.Vendors
                .Any(vendorRef => vendorLists.Contains(vendorRef.Item)));
        return squads.Any();
    }
}
