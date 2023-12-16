using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.BaseComponents.Creators;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.Characters.Templates
{
    public class CharacterTemplateCreator : ITemplateCreator
    {
        private const string TemplateName = "Character";

        private static readonly Dictionary<AttachSlot, string> SlotMap = new Dictionary<AttachSlot, string>()
        {
            { AttachSlot.Hat, "headgear" },
            { AttachSlot.Body, "armour" },
            { AttachSlot.Shirt, "shirt" },
            { AttachSlot.Legs, "legwear" },
            { AttachSlot.Boots, "footwear" },
        };

        private static readonly Dictionary<string, string> RaceMap = new Dictionary<string, string>()
        {
            { "Greenlander", "Human" },
            { "Scorchlander", "Human" },
            { "Hive Prince", "Hive" },
            { "Hive Soldier Drone", "Hive" },
            { "Hive Worker Drone", "Hive" },
            { "Hive Queen", "Hive" },
            { "Screamer MkI", "Skeleton" },
            { "P4 Unit", "Skeleton" },
            { "Soldierbot", "Skeleton" },
            { "Skeleton P4MkII", "Skeleton" },
            { "Skeleton Log-Head MKII", "Skeleton" },
            { "Skeleton No-Head MkII", "Skeleton" },
            { "Hive Prince South Hive", "Southern Hive" },
            { "Hive Soldier Drone South Hive", "Southern Hive" },
            { "Hive Worker Drone South Hive", "Southern Hive" },
        };

        private readonly IItemRepository itemRepository;

        public CharacterTemplateCreator(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public Character? Character { get; set; }

        public WikiTemplate? Generate(ArticleData data)
        {
            if (this.Character is null)
            {
                return null;
            }

            var existingTemplates = data.WikiTemplates
                .Where(template => template.Name == TemplateName);

            var existingTemplate = existingTemplates.SingleOrDefault();

            string? title = null;
            var articleTitle = data.ArticleTitle;
            if (articleTitle != this.Character.Name)
            {
                title = this.Character.Name;
            }

            var parameters = new IndexedDictionary<string, string?>()
            {
                { "title1", title },
                { "image1", PullFromTemplate(existingTemplate, "image1") },
                { "caption1", PullFromTemplate(existingTemplate, "caption1") },
            };

            this.ProcessRaces(this.Character, parameters);
            ProcessGender(this.Character, parameters);

            parameters.Add("weapons", ProcessNullableReferences(this.Character, character => character.Weapons));

            ProcessArmour(this.Character, parameters);
            parameters.Add("inventory", ProcessNullableReferences(this.Character, character => character.Inventory));
            parameters.Add("backpack", ProcessNullableReferences(this.Character, character => character.Backpack));

            return new WikiTemplate(TemplateName, parameters);
        }

        private void ProcessRaces(Character character, IndexedDictionary<string, string?> parameters)
        {
            var raceReferences = character.Races;
            if (!raceReferences.Any())
            {
                var referringSquads = this.itemRepository.GetItems<Squad>()
                    .Where(squad => squad.ContainsCharacter(character));
                var squadRaces = referringSquads.SelectMany(squad => squad.RaceOverrides);

                raceReferences = raceReferences.Concat(squadRaces);

                if (!squadRaces.Any())
                {
                    var factions = referringSquads.SelectMany(squad => squad.Faction)
                        .Distinct();
                    var factionRaces = factions.SelectMany(faction => faction.Item.Races);

                    raceReferences = raceReferences.Concat(factionRaces);
                }
            }

            var races = raceReferences.Select(race => race.Item).Distinct();

            var formattedRaces = new HashSet<string>();
            var formattedSubraces = new HashSet<string>();
            foreach (var race in races)
            {
                var hasParent = RaceMap.TryGetValue(race.Name, out var parentRace);
                var trimmedName = race.Name.Trim();

                if (hasParent)
                {
                    formattedRaces.Add($"[[{parentRace}]]");
                    formattedSubraces.Add($"[[{trimmedName}]]");
                }
                else
                {
                    formattedRaces.Add($"[[{trimmedName}]]");
                }
            }

            parameters.Add("race", string.Join(", ", formattedRaces));
            parameters.Add("subrace", string.Join(", ", formattedSubraces));
        }

        private static void ProcessArmour(Character character, IndexedDictionary<string, string?> parameters)
        {
            foreach (var slotPair in SlotMap)
            {
                var slot = slotPair.Key;
                var target = slotPair.Value;

                var items = character.Clothing.Where(armour => armour.Item.Slot == slot)
                    .OrderByDescending(reference => reference.Value0)
                    .ThenBy(reference => reference.Item.Name)
                    .Select(reference => $"[[{reference.Item.Name}]]");
                parameters.Add(target, string.Join(", ", items));
            }
        }

        private static void ProcessGender(Character character, IndexedDictionary<string, string?> parameters)
        {
            string result;
            if (character.FemaleChance == 0)
            {
                result = "Male";
            }
            else if (character.FemaleChance >= 100)
            {
                result = "Female";
            }
            else
            {
                result = $"{character.FemaleChance}% Female chance";
            }

            parameters.Add("gender", result);
        }

        private static string? ProcessNullableReferences<T>(Character character, Func<Character, IEnumerable<ItemReference<T>>> func)
            where T : IItem
        {
            var itemRefs = func(character);
            if (!itemRefs.Any())
            {
                return null;
            }
            else if (itemRefs.Count() == 1)
            {
                return $"[[{itemRefs.Single().Item.Name}]]";
            }
            else
            {
                var items = itemRefs.OrderByDescending(reference => reference.Value0)
                    .ThenBy(reference => reference.Item.Name)
                    .Select(itemRef => $"[[{itemRef.Item.Name}]]");
                return string.Join(", ", items);
            }
        }

        private static string? PullFromTemplate(WikiTemplate? template, string parameter)
        {
            if (template is null)
            {
                return null;
            }

            template.Parameters.TryGetValue(parameter, out var result);

            return result;
        }
    }
}
