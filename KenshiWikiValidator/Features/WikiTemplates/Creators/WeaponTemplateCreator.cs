using System.Text.RegularExpressions;
using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.DataItemConversion;

namespace KenshiWikiValidator.Features.WikiTemplates.Creators
{
    internal class WeaponTemplateCreator : ITemplateCreator
    {
        private const string WikiTemplateName = "Weapon";

        private readonly IItemRepository itemRepository;
        private readonly ArticleData data;

        private readonly Dictionary<int, string> skillToClassMap;

        public WeaponTemplateCreator(IItemRepository itemRepository, ArticleData data)
        {
            this.itemRepository = itemRepository;
            this.data = data;

            this.skillToClassMap = new Dictionary<int, string>()
            {
                { 0, "Katana" },
                { 1, "Sabre" },
                { 2, "Blunt weapon" },
                { 3, "Heavy weapons" },
                { 4, "Hacker" },
                { 8, "Polearm" },
            };
        }

        public WikiTemplate Generate()
        {
            var stringId = this.data.StringId;
            if (string.IsNullOrEmpty(stringId))
            {
                return null!;
            }

            var item = this.itemRepository.GetDataItemByStringId(stringId);

            var armourPenetration = FormatArmourPenetration(item.GetDecimal("armour penetration"));
            var bloodLoss = FormatMultiplier(item.GetDecimal("bleed mult"));
            var attack = FormatIntStat(item.GetInt("attack mod"));
            var defence = FormatIntStat(item.GetInt("defence mod"));
            var indoors = FormatIntStat(item.GetInt("indoors mod"));
            var robotDamage = FormatMultiplier(item.GetDecimal("robot damage mult"), true);
            var humanDamage = FormatMultiplier(item.GetDecimal("human damage mult"), true);
            var animalDamage = FormatMultiplier(item.GetDecimal("animal damage mult"), true);

            var reach = item.Values["length"].ToString()!;
            var description = item.Values["description"].ToString()!;
            description = Regex.Replace(description, @"\r\n|\r|\n", "<br />");

            var skillCategory = (int)item.Values["skill category"];
            var weaponClass = this.skillToClassMap[skillCategory];

            string spiderDamage = null;
            string smallSpiderDamage = null;
            string bonedogDamage = null;
            string skimmerDamage = null;
            string beakThingDamage = null;
            string gorilloDamage = null;
            string leviathanDamage = null;

            var raceDamageReferences = item.GetReferences("race damage");
            foreach (var reference in raceDamageReferences)
            {
                var name = this.itemRepository.GetDataItemByStringId(reference.TargetId).Name;

                var damage = FormatIntStat(reference.Value0 - 100);

                switch (name)
                {
                    case "Spider":
                        spiderDamage = damage;
                        break;
                    case "Small Spider":
                        smallSpiderDamage = damage;
                        break;
                    case "Bonedog":
                        bonedogDamage = damage;
                        break;
                    case "Skimmer":
                        skimmerDamage = damage;
                        break;
                    case "Beak Thing":
                        beakThingDamage = damage;
                        break;
                    case "Gorillo":
                        gorilloDamage = damage;
                        break;
                    case "Leviathan":
                        leviathanDamage = damage;
                        break;
                    default:
                        throw new InvalidOperationException("Unexpected race damage: " + name);
                }
            }

            var properties = new SortedList<string, string?>()
            {
                { "class", weaponClass },
                { "blood loss", bloodLoss },
                { "armour penetration", armourPenetration },
                { "attack", attack },
                { "defence", defence },
                { "indoors", indoors },
                { "damage_robots", robotDamage },
                { "damage_humans", humanDamage },
                { "damage_animals", animalDamage },
                { "damage_spider", spiderDamage },
                { "damage_small spider", smallSpiderDamage },
                { "damage_bonedog", bonedogDamage },
                { "damage_skimmer", skimmerDamage },
                { "damage_beak thing", beakThingDamage },
                { "damage_gorillo", gorilloDamage },
                { "damage_leviathan", leviathanDamage },
                { "reach", reach },
                { "description", description },
                { "string id", stringId },
            };

            return new WikiTemplate(WikiTemplateName, properties);
        }

        private static string? FormatArmourPenetration(decimal input)
        {
            return FormatMultiplier(input + 1, true);
        }

        private static string? FormatMultiplier(decimal input, bool isPercentage = false)
        {
            if (input == 1m)
            {
                return null;
            }

            if (isPercentage)
            {
                input = (input - 1) * 100m;
                return FormatIntStat((int)input);
            }

            return input.ToString("F2");
        }

        private static string? FormatIntStat(int input)
        {
            if (input == 0)
            {
                return null;
            }

            string result;
            if (input > 0)
            {
                result = $"+{input}";
            }
            else
            {
                result = input.ToString();
            }

            return result;
        }
    }
}
