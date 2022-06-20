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
using KenshiWikiValidator.WikiCategories.Locations.Rules;
using KenshiWikiValidator.WikiCategories.SharedRules;

namespace KenshiWikiValidator.WikiCategories.Locations
{
    public class LocationsArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;

        private readonly ContainsTownTemplateRule containsTownTemplateRule;

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
        }

        public override string CategoryName => "Locations";

        public override IEnumerable<IValidationRule> Rules => this.rules;

        public override void AfterValidations()
        {
            foreach (var stringId in this.StringIds)
            {
                var data = new ArticleData()
                {
                    StringIds = new[] { stringId },
                };

                this.containsTownTemplateRule.Execute(stringId, string.Empty, data);
            }
        }
    }
}
