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

namespace KenshiWikiValidator.Armours.Rules
{
    internal class ArmourTemplateRule : IValidationRule
    {
        private readonly ArmourPriceCalculator priceCalculator;
        private readonly IItemRepository repository;

        public ArmourTemplateRule(IItemRepository repository)
        {
            var valueProvider = new BaseArmourValueProvider();
            this.priceCalculator = new ArmourPriceCalculator(valueProvider);
            this.repository = repository;
        }

        public RuleResult Execute(string title, string content, ArticleData data)
        {
            var result = new RuleResult();
            var armourTemplates = data.WikiTemplates.Where(template => template.Name == "Armour");

            var stringId = data.GetAllPossibleStringIds().Single();
            var armour = this.repository.GetItemByStringId<Armour>(stringId);

            foreach (var template in armourTemplates)
            {
                var gradeString = template.Parameters["Grade"];
                var valueString = template.Parameters["value"];

                if (string.IsNullOrEmpty(gradeString) || string.IsNullOrEmpty(valueString))
                {
                    result.AddIssue("One of the Armour templates does not contain a grade or value.");
                    continue;
                }

                var price = int.Parse(valueString.Replace(",", string.Empty).Replace(".", string.Empty));

                var grade = (ArmourGrade)Enum.Parse(typeof(ArmourGrade), gradeString);
                var calculated = this.priceCalculator.CalculatePrice(armour, grade);
                var calculatedFloored = (int)calculated;

                if (price != calculatedFloored)
                {
                    result.AddIssue($"Price mismatch (Grade: {grade}, Class: {armour.Class}, Material: {armour.MaterialType}) - calculated: {calculated}, actual: {price}, difference: {Math.Abs(calculatedFloored - price)}");
                }
            }

            return result;
        }
    }
}
