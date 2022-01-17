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
        public string DamageVersusSpider { get; set; }
        public string DamageVersusSmallSpider { get; set; }
        public string DamageVersusBonedog { get; set; }
        public string DamageVersusSkimmer { get; set; }
        public string DamageVersusBeakThing { get; set; }
        public string DamageVersusGorillo { get; set; }
        public string DamageVersusLeviathan { get; set; }
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
| dmg_vs_spider = {DamageVersusSpider}
| dmg_vs_small_spider = {DamageVersusSmallSpider}
| dmg_vs_bonedog = {DamageVersusBonedog}
| dmg_vs_skimmer = {DamageVersusSkimmer}
| dmg_vs_beak_thing = {DamageVersusBeakThing}
| dmg_vs_gorillo = {DamageVersusGorillo}
| dmg_vs_leviathan = {DamageVersusLeviathan}
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
