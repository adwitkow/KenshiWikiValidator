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

namespace KenshiWikiValidator.Armours
{
    public class BaseArmourValueProvider
    {
        private static readonly Dictionary<(ArmourClass, MaterialType, ArmourGrade), int> BaseValues = new Dictionary<(ArmourClass, MaterialType, ArmourGrade), int>()
        {
            // Base values provided by FrankieWuzHere#3423
            { (ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Prototype), 159 },
            { (ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Prototype), 299 },
            { (ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Prototype), 549 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Prototype), 748 },
            { (ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Prototype), 229 },
            { (ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Prototype), 486 },
            { (ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Prototype), 929 },
            { (ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Prototype), 1328 },
            { (ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Prototype), 139 },
            { (ArmourClass.Light, MaterialType.Leather, ArmourGrade.Prototype), 274 },
            { (ArmourClass.Light, MaterialType.Chain, ArmourGrade.Prototype), 511 },
            { (ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Prototype), 711 },
            { (ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Prototype), 29 },
            { (ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Prototype), 62 },
            { (ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Prototype), 118 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Prototype), 168 },

            { (ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Shoddy), 1356 },
            { (ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Shoddy), 1791 },
            { (ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Shoddy), 2783 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Shoddy), 2975 },
            { (ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Shoddy), 1273 },
            { (ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Shoddy), 1783 },
            { (ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Shoddy), 2867 },
            { (ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Shoddy), 3251 },
            { (ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Shoddy), 1036 },
            { (ArmourClass.Light, MaterialType.Leather, ArmourGrade.Shoddy), 1391 },
            { (ArmourClass.Light, MaterialType.Chain, ArmourGrade.Shoddy), 2183 },
            { (ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Shoddy), 2375 },
            { (ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Shoddy), 179 },
            { (ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Shoddy), 247 },
            { (ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Shoddy), 395 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Shoddy), 443 },

            { (ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Standard), 5187 },
            { (ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Standard), 6567 },
            { (ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Standard), 9935 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Standard), 10103 },
            { (ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Standard), 4614 },
            { (ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Standard), 5935 },
            { (ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Standard), 9071 },
            { (ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Standard), 9407 },
            { (ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Standard), 3907 },
            { (ArmourClass.Light, MaterialType.Leather, ArmourGrade.Standard), 4967 },
            { (ArmourClass.Light, MaterialType.Chain, ArmourGrade.Standard), 7535 },
            { (ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Standard), 7703 },
            { (ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Standard), 656 },
            { (ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Standard), 841 },
            { (ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Standard), 1283 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Standard), 1325 },

            { (ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.High), 11571 },
            { (ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.High), 14527 },
            { (ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.High), 21855 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.High), 21983 },
            { (ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.High), 10182 },
            { (ArmourClass.Medium, MaterialType.Leather, ArmourGrade.High), 12855 },
            { (ArmourClass.Medium, MaterialType.Chain, ArmourGrade.High), 19411 },
            { (ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.High), 19667 },
            { (ArmourClass.Light, MaterialType.Cloth, ArmourGrade.High), 8691 },
            { (ArmourClass.Light, MaterialType.Leather, ArmourGrade.High), 10927 },
            { (ArmourClass.Light, MaterialType.Chain, ArmourGrade.High), 16455 },
            { (ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.High), 16583 },
            { (ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.High), 1452 },
            { (ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.High), 1831 },
            { (ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.High), 2763 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.High), 2795 },

            { (ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Specialist), 20508 },
            { (ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Specialist), 25671 },
            { (ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Specialist), 38543 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Specialist), 38615 },
            { (ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Specialist), 17977 },
            { (ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Specialist), 22543 },
            { (ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Specialist), 33887 },
            { (ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Specialist), 34031 },
            { (ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Specialist), 15388 },
            { (ArmourClass.Light, MaterialType.Leather, ArmourGrade.Specialist), 19271 },
            { (ArmourClass.Light, MaterialType.Chain, ArmourGrade.Specialist), 28943 },
            { (ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Specialist), 29015 },
            { (ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Specialist), 2567 },
            { (ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Specialist), 3217 },
            { (ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Specialist), 4835 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Specialist), 4853 },

            { (ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Masterwork), 28887 },
            { (ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Masterwork), 36119 },
            { (ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Masterwork), 54190 },
            { (ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Masterwork), 54208 },
            { (ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Masterwork), 25285 },
            { (ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Masterwork), 31626 },
            { (ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Masterwork), 47459 },
            { (ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Masterwork), 47498 },
            { (ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Masterwork), 21667 },
            { (ArmourClass.Light, MaterialType.Leather, ArmourGrade.Masterwork), 27094 },
            { (ArmourClass.Light, MaterialType.Chain, ArmourGrade.Masterwork), 40651 },
            { (ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Masterwork), 40971 },
            { (ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Masterwork), 3611 },
            { (ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Masterwork), 4517 },
            { (ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Masterwork), 6778 },
            { (ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Masterwork), 6783 },
        };

        public int GetBaseValue(ArmourClass armourClass, MaterialType type, ArmourGrade grade)
        {
            return BaseValues[(armourClass, type, grade)];
        }
    }
}
