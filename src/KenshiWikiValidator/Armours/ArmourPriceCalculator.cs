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

using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.Armours
{
    public class ArmourPriceCalculator
    {
        private const float ArmourMultiplier = 0.6f;
        private static readonly Dictionary<Slot, float> SlotMultipliers = new Dictionary<Slot, float>()
        {
            // Multipliers provided by FrankieWuzHere#3423
            { Slot.Hat, 0.3f },
            { Slot.Body, 1.0f },
            { Slot.Legs, 0.5f },
            { Slot.Shirt, 1.5f },
            { Slot.Boots, 0.2f },
        };

        private readonly BaseArmourValueProvider valueProvider;

        public ArmourPriceCalculator(BaseArmourValueProvider valueProvider)
        {
            this.valueProvider = valueProvider;
        }

        public float CalculatePrice(Armour armour, ArmourGrade grade)
        {
            var hasCoverage = armour.PartCoverage.Any(coverageRef => coverageRef.Value0 > 0);
            var slotMultiplier = SlotMultipliers[armour.Slot];

            float price;
            if (hasCoverage)
            {
                var baseValue = this.valueProvider.GetBaseValue(armour.Class, armour.MaterialType, grade);
                price = baseValue * slotMultiplier * ArmourMultiplier * armour.RelativePriceMult.GetValueOrDefault();
            }
            else
            {
                price = armour.Value.GetValueOrDefault() * slotMultiplier * armour.RelativePriceMult.GetValueOrDefault();
            }

            return price;
        }
    }
}
