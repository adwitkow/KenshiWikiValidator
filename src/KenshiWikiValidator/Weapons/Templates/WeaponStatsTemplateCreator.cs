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

using System.Globalization;
using System.Text;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.BaseComponents.Creators;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Newtonsoft.Json.Linq;

namespace KenshiWikiValidator.Weapons.Templates
{
    internal class WeaponStatsTemplateCreator : ITemplateCreator
    {
        private static readonly IEnumerable<string> WeaponClasses = new[]
        {
            "Katana", "Sabre", "Blunt", "Heavy", "Hacker", "Unarmed", "Bow", "Turret", "Polearm"
        };

        private static readonly Dictionary<string, decimal> PriceMultipliers = new Dictionary<string, decimal>()
        {
            { "Rusted Junk", 18.85m },
            { "Rusted Blade", 20.65m },
            { "Mid-Grade Salvage", 23.3m },
            { "Old Refitted Blade", 27.05m },
            { "Refitted Blade", 32.275m },
            { "Catun No.1", 39.35m },
            { "Catun No.2", 48.55m },
            { "Catun No.3", 59.95m },
            { "Mk I", 87.5m },
            { "Mk II", 101.8m },
            { "Mk III", 115.025m },
            { "Edge Type 1", 135.625m },
            { "Edge Type 2", 142.7m },
            { "Edge Type 3", 147.9375m },
            { "Meitou", 157.3m },
        };

        private readonly IItemRepository itemRepository;

        public WeaponStatsTemplateCreator(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public WikiTemplate? Generate(ArticleData data)
        {
            throw new NotImplementedException("This is unnecessary at the moment.");
        }

        public WikiTemplate[] Generate(
            Weapon weapon,
            WeaponManufacturer manufacturer,
            IEnumerable<WikiTemplate> articleTemplates,
            bool homemade)
        {
            var results = new List<WikiTemplate>();
            var models = manufacturer.WeaponModels
                .OrderBy(modelRef => modelRef.Value0)
                .Select(modelRef => (Model: modelRef.Item, Level: modelRef.Value0));

            var bluntModifier = (decimal)manufacturer.BluntDamageMod * (decimal)weapon.BluntDamageMultiplier;
            var cutModifier = (decimal)manufacturer.CutDamageMod * (decimal)weapon.CutDamageMultiplier;
            var priceModifier = (decimal)manufacturer.PriceMod;
            var minCutDamage = (decimal)manufacturer.MinCutDamage;

            foreach (var modelPair in models)
            {
                IDictionary<string, string?> baseParameters;
                var baseTemplate = articleTemplates.FirstOrDefault(template => IsBaseTemplate(template, modelPair.Model, homemade));
                if (baseTemplate is not null)
                {
                    baseParameters = baseTemplate.Parameters;
                }
                else
                {
                    baseParameters = new IndexedDictionary<string, string?>();
                }

                var parameters = new IndexedDictionary<string, string?>();

                SetClass(weapon, parameters);
                SetCutDamage(cutModifier, modelPair, parameters);
                decimal bluntDamage = SetBluntDamage(bluntModifier, modelPair, parameters);
                SetBloodLoss(weapon, parameters);
                SetArmourPenetration(weapon, parameters);
                SetAttackAndDefence(weapon, modelPair.Level, parameters);
                SetIndoorsBonus(weapon, parameters);
                SetBaseRacialMultipliers(weapon, parameters);
                SetReferencedRacialMultipliers(weapon, parameters);
                SetRequiredStrength(parameters, bluntDamage, (decimal)weapon.WeightKg);
                SetWeight(weapon, manufacturer, parameters, bluntDamage);

                foreach (var pair in baseParameters)
                {
                    if (!parameters.ContainsKey(pair.Key))
                    {
                        if (pair.Key == "blood_loss")
                        {
                            parameters.Insert(3, pair.Key, pair.Value);
                        }
                        else
                        {
                            parameters[pair.Key] = pair.Value;
                        }
                    }
                }

                if (!parameters.ContainsKey("value"))
                {
                    parameters["value"] = "?";
                }

                if (!parameters.ContainsKey("sell_val"))
                {
                    parameters["sell_val"] = "?";
                }

                parameters["grade"] = modelPair.Model.Name;

                if (homemade && !parameters.ContainsKey("homemade"))
                {
                    parameters["homemade"] = homemade ? "yes" : null;
                }

                var template = new WikiTemplate("WeaponStats", parameters);

                results.Add(template);
            }

            return results.ToArray();
        }

        private static void SetReferencedRacialMultipliers(Weapon weapon, IndexedDictionary<string, string?> parameters)
        {
            SetReferencedRacialMultiplier(weapon, "dmg_vs_spider", "Spider", parameters);
            SetReferencedRacialMultiplier(weapon, "dmg_vs_small_spider", "Small Spider", parameters);
            SetReferencedRacialMultiplier(weapon, "dmg_vs_bonedog", "Bonedog", parameters);
            SetReferencedRacialMultiplier(weapon, "dmg_vs_skimmer", "Skimmer", parameters);
            SetReferencedRacialMultiplier(weapon, "dmg_vs_beak_thing", "Beak Thing", parameters);
            SetReferencedRacialMultiplier(weapon, "dmg_vs_gorillo", "Gorillo", parameters);
            SetReferencedRacialMultiplier(weapon, "dmg_vs_leviathan", "Leviathan", parameters);
        }

        private static void SetBaseRacialMultipliers(Weapon weapon, IndexedDictionary<string, string?> parameters)
        {
            SetPlusMinusValue(parameters, "dmg_vs_animals", ConvertMultiplierToPercentage(weapon.AnimalDamageMultiplier - 1));
            SetPlusMinusValue(parameters, "dmg_vs_robots", ConvertMultiplierToPercentage(weapon.RobotDamageMultiplier - 1));
            SetPlusMinusValue(parameters, "dmg_vs_humans", ConvertMultiplierToPercentage(weapon.HumanDamageMulitplier - 1));
        }

        private static void SetIndoorsBonus(Weapon weapon, IndexedDictionary<string, string?> parameters)
        {
            SetPlusMinusValue(parameters, "indoors_bonus", weapon.IndoorsModifier);
        }

        private static void SetAttackAndDefence(Weapon weapon, int modelLevel, IndexedDictionary<string, string?> parameters)
        {
            var attack = weapon.AttackModifier;
            var defence = weapon.DefenceModifier;
            if (modelLevel < 10)
            {
                attack--;
                defence--;
            }
            SetPlusMinusValue(parameters, "attack_bonus", attack);
            SetPlusMinusValue(parameters, "defence_bonus", defence);
        }

        private static void SetArmourPenetration(Weapon weapon, IndexedDictionary<string, string?> parameters)
        {
            SetPlusMinusValue(parameters, "armour_pen", ConvertMultiplierToPercentage(weapon.ArmourPenetration));
        }

        private static void SetBloodLoss(Weapon weapon, IndexedDictionary<string, string?> parameters)
        {
            if (weapon.BleedMultiplier == 1)
            {
                return;
            }

            parameters["blood_loss"] = weapon.BleedMultiplier.ToString("N2");
        }

        private static void SetClass(Weapon weapon, IndexedDictionary<string, string?> parameters)
        {
            var weaponClass = WeaponClasses.ElementAt(weapon.SkillCategory);
            parameters["class"] = weaponClass;
        }

        private static void SetCutDamage(decimal cutModifier, (MaterialSpecsWeapon Model, int Level) modelPair, IndexedDictionary<string, string?> parameters)
        {
            var cutDamage = 0.017m * modelPair.Level;
            cutDamage += 0.3m;
            cutDamage *= cutModifier;
            SetParameter(parameters, "cutting_dmg", cutDamage);
        }

        private static decimal SetBluntDamage(decimal bluntModifier, (MaterialSpecsWeapon Model, int Level) modelPair, IndexedDictionary<string, string?> parameters)
        {
            var bluntDamage = modelPair.Level / 50m;
            bluntDamage *= bluntModifier;
            SetParameter(parameters, "blunt_dmg", bluntDamage);
            return bluntDamage;
        }

        private static void SetWeight(Weapon weapon, WeaponManufacturer manufacturer, IndexedDictionary<string, string?> parameters, decimal bluntDamage)
        {
            var bluntWeight = 20m * bluntDamage;
            bluntWeight *= (decimal)weapon.WeightMult;

            var weightKg = (decimal)weapon.WeightKg;
            decimal weight;
            if (bluntWeight < weightKg * (decimal)manufacturer.WeightMod)
            {
                weight = weightKg * (decimal)manufacturer.WeightMod;
            }
            else
            {
                weight = bluntWeight;
            }

            SetParameter(parameters, "weight", weight);
        }

        private static void SetRequiredStrength(IndexedDictionary<string, string?> parameters, decimal bluntDamage, decimal weightKg)
        {
            var requiredStrength = Math.Max(bluntDamage * 40m, weightKg);
            SetParameter(parameters, "str_required", requiredStrength);
        }

        private static void SetReferencedRacialMultiplier(Weapon weapon, string key, string race, IndexedDictionary<string, string?> parameters)
        {
            var exists = weapon.RaceDamage.Any(reference => reference.Item.Name == race);
            if (exists)
            {
                // dealing with generic default struct is a bigger headache than just doing the lookup twice
                // (even though it breaks my soul)
                var raceDamage = weapon.RaceDamage.FirstOrDefault(reference => reference.Item.Name == race);
                SetPlusMinusValue(parameters, key, raceDamage.Value0 - 100);
            }
        }

        private static int ConvertMultiplierToPercentage(float multiplier)
        {
            return (int)Math.Round(multiplier * 100);
        }

        private static void SetPlusMinusValue(IndexedDictionary<string, string?> parameters, string key, float value)
        {
            if (value == 0)
            {
                return;
            }

            string stringValue;
            if (value > 0)
            {
                stringValue = $"+{value}";
            }
            else
            {
                stringValue = value.ToString();
            }

            parameters[key] = stringValue;
        }

        private static bool IsBaseTemplate(WikiTemplate template, MaterialSpecsWeapon model, bool homemade)
        {
            var isWeaponStats = template.Name == "WeaponStats";
            var isCorrectGrade = template.Parameters.Any(param => param.Key == "grade" && param.Value == model.Name);

            if (!isWeaponStats || !isCorrectGrade)
            {
                return false;
            }

            template.Parameters.TryGetValue("homemade", out var homemadeValue);
            var isHomemadeTemplate = !string.IsNullOrEmpty(homemadeValue);
            return homemade ? isHomemadeTemplate : !isHomemadeTemplate;
        }

        private static void SetParameter(IDictionary<string, string?> parameters, string key, decimal value)
        {
            parameters[key] = value.ToString("0.00#################", CultureInfo.InvariantCulture);
        }
    }
}
