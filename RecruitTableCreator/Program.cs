// See https://aka.ms/new-console-template for more information

using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using OpenConstructionSet.Models;

var repository = new ItemRepository();
repository.Load();

var characterNames = new List<string>();

var packages = repository.GetItems().OfType<DialoguePackage>();
var dialogueToPackage = MapDialoguesToPackages(packages);

var dialogues = packages.SelectMany(package => package.Dialogues)
    .DistinctBy(dialogue => dialogue.StringId);

foreach (var dialogue in dialogues)
{
    var allLines = dialogue.GetAllLines();
    foreach (var line in allLines)
    {
        if (line.Effects.Any(effect => effect.EffectName == DialogueEffectName.DA_JOIN_SQUAD_WITH_EDIT || effect.EffectName == DialogueEffectName.DA_JOIN_SQUAD_FAST))
        {
            var usedPackages = dialogueToPackage[dialogue.StringId];

            var characters = usedPackages.SelectMany(package => repository.GetReferencingDataItemsFor(package.StringId))
                .Concat(repository.GetReferencingDataItemsFor(dialogue.StringId))
                .Where(item => item.Type == ItemType.Character)
                .Select(item => item.Name)
                .Distinct()
                .ToList();

            foreach (var character in characters)
            {
                characterNames.Add($"{character} (Dialogue: {dialogue.Name})");
            }
        }
    }
}

characterNames.Sort();

foreach (var name in characterNames.Distinct())
{
    Console.WriteLine(name);
}

static Dictionary<string, ICollection<DialoguePackage>> MapDialoguesToPackages(IEnumerable<DialoguePackage> packages)
{
    var result = new Dictionary<string, ICollection<DialoguePackage>>();
    foreach (var package in packages)
    {
        foreach (var dialogue in package.Dialogues)
        {
            if (result.ContainsKey(dialogue.StringId))
            {
                result[dialogue.StringId].Add(package);
            }
            else
            {
                result.Add(dialogue.StringId, new List<DialoguePackage>()
                {
                    package
                });
            }
        }
    }
    return result;
}
