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
using KenshiWikiValidator.Characters.Rules;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.Characters.Rules;

namespace KenshiWikiValidator.Characters
{
    public class CharactersArticleValidator : ArticleValidatorBase
    {
        private readonly IEnumerable<IValidationRule> rules;
        private readonly IItemRepository itemRepository;
        private readonly WikiTitleCache wikiTitles;

        public CharactersArticleValidator(IItemRepository itemRepository, WikiTitleCache wikiTitles)
            : base(itemRepository, wikiTitles)
        {
            this.rules = new List<IValidationRule>()
            {
                new ContainsTemplateRule("Character"),
                new CharacterRule(itemRepository, wikiTitles),
                new CharacterStatsRule(itemRepository),
                new EquipmentSectionRule(itemRepository, wikiTitles),
            };
            this.itemRepository = itemRepository;
            this.wikiTitles = wikiTitles;
        }

        public override string CategoryName => "Characters";

        public override IEnumerable<IValidationRule> Rules => this.rules;

        public override bool ShouldValidate(string title, string content)
        {
            if (!this.ArticleDataMap.ContainsKey(title))
            {
                this.CachePageData(title, content);
            }

            var data = this.ArticleDataMap[title];

            if (data.Categories.Any(cat => cat is "Lore" or "Animals"))
            {
                return false;
            }

            return true;
        }

        public override IEnumerable<RuleResult> AfterValidations()
        {
            this.GenerateCharacterTable();

            return base.AfterValidations();
        }

        private void GenerateCharacterTable()
        {
            var lines = new List<string>
            {
                "Automatically generated.",
                string.Empty,
                "{| class=\"article-table sortable\"",
                "! String ID",
                "! Article title (or FCS name)",
                "! Missing or implicit string ID",
            };

            foreach (var character in this.itemRepository.GetItems<Character>().OrderBy(ch => ch.Name.Trim()))
            {
                var isUniqueReplacement = this.itemRepository.GetItems<Character>().Any(r => r.UniqueReplacementSpawn.ContainsItem(character));
                var isSpecialLeader = this.itemRepository.GetItems<FactionCampaign>().Any(r => r.SpecialLeader.ContainsItem(character));
                var isNewGameCharacter = this.itemRepository.GetItems<NewGameStartoff>().Any(r => r.Characters.ContainsItem(character));
                var isInSquad = this.itemRepository.GetItems<Squad>().Any(r => r.ContainsCharacter(character));
                var isInUniqueSquad = this.itemRepository.GetItems<UniqueSquadTemplate>().Any(r => r.Leader.ContainsItem(character) || r.Squad.ContainsItem(character));

                if (isUniqueReplacement || isSpecialLeader || isNewGameCharacter
                    || isInSquad || isInUniqueSquad)
                {
                    lines.Add("|-");
                    lines.Add($"| {character.StringId.Trim()}");
                    var name = this.wikiTitles.GetTitle(character);
                    lines.Add($"| [[{name.Trim()}]]");

                    if (this.wikiTitles.HasArticle(character))
                    {
                        lines.Add("| No");
                    }
                    else
                    {
                        lines.Add("| Yes");
                    }
                }
            }

            lines.Add("|}");

            var path = Path.Combine("output", "character-table.txt");
            File.WriteAllLines(path, lines);
        }
    }
}
