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
using KenshiWikiValidator.WikiSections;
using KenshiWikiValidator.WikiTemplates.Creators;

namespace KenshiWikiValidator.WikiCategories.Weapons.Rules
{
    public class ContainsWeaponCraftingSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;

        public ContainsWeaponCraftingSectionRule(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        protected override WikiSectionBuilder CreateSectionBuilder(ArticleData data)
        {
            var stringId = data.StringIds.SingleOrDefault();
            if (string.IsNullOrEmpty(stringId))
            {
                return null!;
            }

            var weapon = this.itemRepository.GetItemByStringId<Weapon>(stringId);

            if (weapon is null)
            {
                throw new InvalidOperationException($"Could not find a weapon with string id '{stringId}'");
            }

            var cost = weapon.MaterialCost;

            var builder = new WikiSectionBuilder()
                .WithHeader("Crafting");

            var research = this.GetUnlockingResearch(weapon);

            if (research is null)
            {
                builder.WithParagraph("This item cannot be crafted.");
                return builder;
            }

            var buildings = research.EnableBuildings
                .Select(building => building.Item.Name.Equals("Weapon Smith") ? "Weapon Smithing Bench" : building.Item.Name);
            var items = research.EnableWeaponTypes.Select(weapon => weapon.Item.Name);
            var prerequisites = research.Requirements.Select(tech => $"{tech.Item.Name} (Tech)");
            var requiredFor = this.itemRepository.GetItems<Research>()
                .Where(r => r.Requirements
                    .Any(requirement => requirement.Item == research))
                .Select(tech => $"{tech.Name} (Tech)")
                .ToArray();

            var craftingListIntro = "This item can be crafted in various qualities using different levels of [[Weapon Smithing Bench]]";
            var hasBlueprints = this.HasBlueprints(research);
            if (hasBlueprints)
            {
                craftingListIntro += " after learning the appropriate [[Blueprints|blueprint]].";
            }
            else
            {
                var researchInfoTemplateCreator = new ResearchInfoTemplateCreator()
                {
                    Costs = research.Costs.Select(costRef => $"{costRef.Value0} [[{costRef.Item.Name}]]s"),
                    Description = research.Description,
                    NewBuildings = buildings,
                    NewItems = items,
                    Prerequisites = prerequisites,
                    RequiredFor = requiredFor,
                    ResearchName = research.Name,
                    TechLevel = research.Level.GetValueOrDefault(),
                    Time = research.Time.GetValueOrDefault(),
                };

                var template = researchInfoTemplateCreator.Generate();

                builder.WithTemplate(template)
                    .WithNewline();

                craftingListIntro += ".";
            }

            builder.WithParagraph(craftingListIntro);

            var craftingTemplateCreator = new CraftingTemplateCreator()
            {
                Output = weapon.Name,
                ImageSettings = "96px",
                Collapsed = true,
            };

            craftingTemplateCreator.BuildingName = "Weapon Smith I";
            craftingTemplateCreator.Input1 = ("Iron Plates", cost.GetValueOrDefault());

            builder.WithTemplate(craftingTemplateCreator.Generate());

            craftingTemplateCreator.BuildingName = "Weapon Smith II";
            craftingTemplateCreator.Input1 = ("Iron Plates", cost.GetValueOrDefault());
            craftingTemplateCreator.Input2 = ("Fabrics", cost.GetValueOrDefault());

            builder.WithTemplate(craftingTemplateCreator.Generate());

            craftingTemplateCreator.BuildingName = "Weapon Smith III";
            craftingTemplateCreator.Input1 = ("Steel Bars", cost.GetValueOrDefault());
            craftingTemplateCreator.Input2 = ("Fabrics", cost.GetValueOrDefault());

            builder.WithTemplate(craftingTemplateCreator.Generate());

            return builder;
        }

        private bool HasBlueprints(Research research)
        {
            var vendorLists = this.itemRepository.GetItems<VendorList>()
                .Where(vendor => vendor.Blueprints.ContainsItem(research));
            var squads = this.itemRepository.GetItems<Squad>()
                .Where(squad => squad.Vendors
                    .Any(vendorRef => vendorLists.Contains(vendorRef.Item)));
            return squads.Any();
        }

        private Research? GetUnlockingResearch(Weapon weapon)
        {
            return this.itemRepository.GetItems<Research>()
                .SingleOrDefault(research => research.EnableWeaponTypes.ContainsItem(weapon));
        }
    }
}
