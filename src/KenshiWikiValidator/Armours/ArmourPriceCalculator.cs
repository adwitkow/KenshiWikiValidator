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
        private const float DefaultArmourMultiplier = 0.6f;
        private static readonly Dictionary<Slot, float> SlotMultipliers = new Dictionary<Slot, float>()
        {
            { Slot.Hat, 0.3f },
            { Slot.Body, 1.0f },
            { Slot.Legs, 0.5f },
            { Slot.Shirt, 1.5f },
            { Slot.Boots, 0.2f },
        };

        private static readonly Dictionary<ArmourGrade, int> GradeValues = new Dictionary<ArmourGrade, int>()
        {
            { ArmourGrade.Prototype, 5 },
            { ArmourGrade.Shoddy, 20 },
            { ArmourGrade.Standard, 40 },
            { ArmourGrade.High, 60 },
            { ArmourGrade.Specialist, 80 },
            { ArmourGrade.Masterwork, 95 },
        };

        private static readonly Dictionary<(ArmourClass, MaterialType), int> MinValues = new Dictionary<(ArmourClass, MaterialType), int>()
        {
            { (ArmourClass.Cloth, MaterialType.Cloth), 20 },
            { (ArmourClass.Cloth, MaterialType.Leather), 50 },
            { (ArmourClass.Cloth, MaterialType.Chain), 100 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate), 150 },
            { (ArmourClass.Light, MaterialType.Cloth), 80 },
            { (ArmourClass.Light, MaterialType.Leather), 200 },
            { (ArmourClass.Light, MaterialType.Chain), 400 },
            { (ArmourClass.Light, MaterialType.MetalPlate), 600 },
            { (ArmourClass.Medium, MaterialType.Cloth), 160 },
            { (ArmourClass.Medium, MaterialType.Leather), 400 },
            { (ArmourClass.Medium, MaterialType.Chain), 800 },
            { (ArmourClass.Medium, MaterialType.MetalPlate), 1200 },
            { (ArmourClass.Heavy, MaterialType.Cloth), 80 },
            { (ArmourClass.Heavy, MaterialType.Leather), 200 },
            { (ArmourClass.Heavy, MaterialType.Chain), 400 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate), 600 },
        };

        private static readonly Dictionary<(ArmourClass, MaterialType), int> MaxValues = new Dictionary<(ArmourClass, MaterialType), int>()
        {
            { (ArmourClass.Cloth, MaterialType.Cloth), 4_000 },
            { (ArmourClass.Cloth, MaterialType.Leather), 5_000 },
            { (ArmourClass.Cloth, MaterialType.Chain), 7_500 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate), 7_500 },
            { (ArmourClass.Light, MaterialType.Cloth), 24_000 },
            { (ArmourClass.Light, MaterialType.Leather), 30_000 },
            { (ArmourClass.Light, MaterialType.Chain), 45_000 },
            { (ArmourClass.Light, MaterialType.MetalPlate), 45_000 },
            { (ArmourClass.Medium, MaterialType.Cloth), 28_000 },
            { (ArmourClass.Medium, MaterialType.Leather), 35_000 },
            { (ArmourClass.Medium, MaterialType.Chain), 52_500 },
            { (ArmourClass.Medium, MaterialType.MetalPlate), 52_500 },
            { (ArmourClass.Heavy, MaterialType.Cloth), 32_000 },
            { (ArmourClass.Heavy, MaterialType.Leather), 40_000 },
            { (ArmourClass.Heavy, MaterialType.Chain), 60_000 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate), 60_000 },
        };

        public int CalculatePrice(Armour armour, ArmourGrade grade, float armourMultiplier = DefaultArmourMultiplier)
        {
            var slotMultiplier = SlotMultipliers[armour.Slot];

            var material = armour.MaterialType;
            var armourClass = armour.Class;

            var minValue = MinValues[(armourClass, material)];
            var maxValue = MaxValues[(armourClass, material)];
            var gradeValue = GradeValues[grade];

            var qualityMultiplier = Math.Pow((double)gradeValue / 100, 2);
            var maxMinDifference = maxValue - minValue;
            var multipliedDifference = (int)(maxMinDifference * qualityMultiplier);
            var basePrice = minValue + multipliedDifference;

            var withArmourMultiplier = (int)(basePrice * armourMultiplier);
            var withSlotMultiplier = (int)(withArmourMultiplier * slotMultiplier);
            var withRelativeMultiplier = (int)(withSlotMultiplier * armour.RelativePriceMult);

            return withRelativeMultiplier;
        }
    }
}
