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

namespace KenshiWikiValidator.WikiCategories.Locations.Rules
{
    internal class TownOverrideSectionRule : ContainsSectionRuleBase
    {
        private readonly IItemRepository itemRepository;
        private readonly IWikiTitleCache wikiTitleCache;

        public TownOverrideSectionRule(IItemRepository itemRepository, IWikiTitleCache wikiTitleCache)
        {
            this.itemRepository = itemRepository;
            this.wikiTitleCache = wikiTitleCache;
        }

        protected override WikiSectionBuilder? CreateSectionBuilder(ArticleData data)
        {
            var stringIds = data.StringIds;
            if (!stringIds.Any())
            {
                if (string.IsNullOrEmpty(data.PotentialStringId))
                {
                    return null;
                }
                else
                {
                    stringIds = new[] { data.PotentialStringId };
                }
            }

            var items = stringIds.Select(id => this.itemRepository.GetItemByStringId<Town>(id));
            var townOverrides = items
                .SelectMany(item => item.OverrideTown
                    .Select(townReference => townReference.Item))
                .ToList();

            if (!townOverrides.Any())
            {
                return null;
            }

            var pageTitle = this.wikiTitleCache.GetTitle(items.First());

            WikiSectionBuilder sectionBuilder;
            if (townOverrides.Count > 1)
            {
                sectionBuilder = this.CreateTabViewSectionBuilder(townOverrides, pageTitle);
            }
            else
            {
                sectionBuilder = this.CreateSingleOverrideSectionBuilder(townOverrides.Single());
            }

            return sectionBuilder;
        }

        private WikiSectionBuilder CreateTabViewSectionBuilder(List<Town> townOverrides, string pageTitle)
        {
            var sectionBuilder = new WikiSectionBuilder()
                .WithHeader("Town overrides")
                .WithParagraph($"'''{pageTitle}''' can be affected by multiple [[World States]] to produce the following [[Town Overrides]].")
                .WithLine("<tabview>");

            var overrideSubpages = townOverrides.Select(townOverride => this.wikiTitleCache.GetTitle(townOverride));
            var longestSubpageTitle = overrideSubpages.Max(subpage => subpage.Length);
            foreach (var subpage in overrideSubpages)
            {
                var tabviewHeader = string.Join(' ', subpage.Split('/').Skip(1));
                sectionBuilder.WithLine($"{subpage.PadRight(longestSubpageTitle)} | {tabviewHeader}");
            }

            sectionBuilder.WithLine("</tabview>");
            return sectionBuilder;
        }

        private WikiSectionBuilder CreateSingleOverrideSectionBuilder(Town townOverride)
        {
            var title = this.wikiTitleCache.GetTitle(townOverride);
            var sectionBuilder = new WikiSectionBuilder()
                .WithHeader("Town override")
                .WithLine($"{{{{Main|{title}}}}}")
                .WithParagraph("DESCRIPTION OF THE REQUIRED WORLD STATES.");

            return sectionBuilder;
        }
    }
}
