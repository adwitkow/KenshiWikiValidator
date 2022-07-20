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
using KenshiWikiValidator.Locations;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.MapItems.Rules
{
    public class RevealedLocationsSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly IWikiTitleCache wikiTitleCache;
        private readonly IZoneDataProvider zoneDataProvider;
        private readonly WeightedChanceCalculator chanceCalculator;

        public RevealedLocationsSectionRule(
            IItemRepository itemRepository,
            IWikiTitleCache wikiTitleCache,
            IZoneDataProvider zoneDataProvider)
        {
            this.chanceCalculator = new WeightedChanceCalculator();

            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
            this.zoneDataProvider = zoneDataProvider;
        }

        protected override WikiSectionBuilder? CreateSectionBuilder(ArticleData data)
        {
            var stringIds = data.GetAllPossibleStringIds();

            if (!stringIds.Any())
            {
                return null;
            }

            var sectionBuilder = new WikiSectionBuilder()
                .WithHeader("Revealed locations");

            var mapItem = this.itemRepository.GetItemByStringId<MapItem>(stringIds.Single());
            var unlockCount = mapItem.UnlockCount;

            if (unlockCount == 0)
            {
                sectionBuilder.WithParagraph("Upon use, this item will reveal every single location available in the table below.");
            }
            else if (unlockCount == 1)
            {
                sectionBuilder.WithParagraph("Upon use, this item will reveal a single location available in the table below &mdash; chosen randomly.");
            }
            else
            {
                sectionBuilder.WithParagraph($"Upon use, this item will reveal {unlockCount} locations available in the table below &mdash; chosen randomly.");
            }

            sectionBuilder.WithLine("{| class=\"wikitable sortable\" style=\"text-align: center;\"")
                .WithLine("! Location !! Amount !! Chance");

            var summedWeights = mapItem.Towns.Sum(townRef => townRef.Value1);
            var chanceMap = mapItem.Towns
                .ToDictionary(townRef => townRef, townRef => this.CalculateChance(townRef.Value1, summedWeights, unlockCount));

            var orderedChanceMap = chanceMap.OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key.Item.Name);
            foreach (var townChancePair in orderedChanceMap)
            {
                var townReference = townChancePair.Key;
                var chance = Math.Round(townChancePair.Value * 100, 2);

                var town = townReference.Item;
                var townWikiTitle = this.wikiTitleCache.GetTitle(town);

                var amount = townReference.Value0;
                var outputAmount = this.GetOutputAmount(amount, town.Name);

                sectionBuilder.WithLine("|-");
                sectionBuilder.WithLine($"| style=\"text-align:left;\" | [[{townWikiTitle}]] || {outputAmount} || {chance}%");
            }

            sectionBuilder.WithLine("|}");

            return sectionBuilder;
        }

        private double CalculateChance(int weight, int summedWeights, int unlockCount)
        {
            if (unlockCount == 0)
            {
                return 1;
            }

            var flatChance = (double)weight / summedWeights;

            return this.chanceCalculator.Calculate(unlockCount, 1, flatChance);
        }

        private string GetOutputAmount(int amount, string townName)
        {
            if (amount != 0)
            {
                return amount.ToString();
            }

            var townCount = this.zoneDataProvider.GetZones(townName).Count();

            if (townCount > 1)
            {
                return "All";
            }
            else
            {
                return "1";
            }
        }
    }
}
