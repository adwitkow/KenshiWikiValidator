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
using KenshiWikiValidator.BaseComponents.SharedRules;
using KenshiWikiValidator.Locations.Rules;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.Locations
{
    public class LocationsArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        private readonly ContainsTownTemplateRule containsTownTemplateRule;
        private readonly IItemRepository itemRepository;

        public LocationsArticleValidator(IItemRepository itemRepository, IZoneDataProvider zoneDataProvider, IWikiTitleCache wikiTitles)
            : base(itemRepository, wikiTitles, typeof(Town))
        {
            this.containsTownTemplateRule = new ContainsTownTemplateRule(itemRepository, wikiTitles, zoneDataProvider);

            this.rules = new List<IValidationRule>()
            {
                new ContainsTemplateRule("Town"),
                this.containsTownTemplateRule,
                new TownOverrideSectionRule(itemRepository, wikiTitles),
            };
            this.itemRepository = itemRepository;
        }

        public override string CategoryName => "Locations";

        public override IEnumerable<IValidationRule> Rules => this.rules;

        public override IEnumerable<RuleResult> AfterValidations()
        {
            var ignoredTypes = new[]
            {
                TownType.Nest,
                TownType.Null,
                TownType.NestMarker,
            };

            var results = new List<RuleResult>();

            foreach (var stringId in this.StringIds)
            {
                var town = this.itemRepository.GetItemByStringId<Town>(stringId);
                var name = town.Name;
                var data = new ArticleData()
                {
                    StringIds = new[] { stringId },
                };

                if (ignoredTypes.Contains(town.TownType))
                {
                    continue;
                }

                results.Add(this.containsTownTemplateRule.Execute($@"{stringId}-{name}", string.Empty, data));
            }

            return results;
        }
    }
}
