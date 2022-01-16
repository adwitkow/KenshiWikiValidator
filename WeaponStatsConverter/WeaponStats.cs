using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeaponStatsConverter
{
    internal class WeaponStats
    {
        public string Class { get; set; }
        public string Manufacturer { get; set; }
        public string Grade { get; set; }
        public string Weight { get; set; }
        public string CuttingDamage { get; set; }
        public string BluntDamage { get; set; }
        public string BloodLoss { get; set; }
        public string ArmourPenetration { get; set; }
        public string AttackModifier { get; set; }
        public string DefenceModifier { get; set; }
        public string IndoorsModifier { get; set; }
        public string DamageVersusAnimals { get; set; }
        public string DamageVersusRobots { get; set; }
        public string DamageVersusHumans { get; set; }
        public string RequiredStrength { get; set; }
        public string BuyValue { get; set; }
        public string SellValue { get; set; }

        public override string ToString()
        {
            return @$"{{{{WeaponStats
| class = {Class}
| cutting_dmg = {CuttingDamage}
| blunt_dmg = {BluntDamage}
| blood_loss = {BloodLoss}
| armour_pen = {ArmourPenetration}
| attack_bonus = {AttackModifier}
| defence_bonus = {DefenceModifier}
| indoors_bonus = {IndoorsModifier}
| dmg_vs_animals = {DamageVersusAnimals}
| dmg_vs_robots = {DamageVersusRobots}
| dmg_vs_humans = {DamageVersusHumans}
| str_required = {RequiredStrength}
| weight = {Weight}
| value = {BuyValue}
| sell_val = {SellValue}
| grade = {Grade}
| homemade = {(Manufacturer == "Homemade" ? "sure" : "")}
}}}}";
        }
    }
}
