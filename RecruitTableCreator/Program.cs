#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high
// See https://aka.ms/new-console-template for more information

using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;
using OpenConstructionSet.Models;

var repository = new ItemRepository();
repository.Load();

var characterNames = new List<string>();

var packages = repository.GetItems().OfType<DialoguePackage>();
var dialogueToPackage = MapDialoguesToPackages(packages);

var dialogues = packages
    .SelectMany(package => package.Dialogs
        .Select(dialogueRef => dialogueRef.Item))
    .DistinctBy(dialogue => dialogue.StringId);

foreach (var dialogue in dialogues)
{
    var allLines = dialogue.GetAllLines();
    foreach (var line in allLines)
    {
        if (line.Effects.Any(effect => effect.Item.ActionName == DialogueEffect.DA_JOIN_SQUAD_WITH_EDIT || effect.Item.ActionName == DialogueEffect.DA_JOIN_SQUAD_FAST))
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
        foreach (var dialogue in package.Dialogs)
        {
            if (result.ContainsKey(dialogue.Item.StringId))
            {
                result[dialogue.Item.StringId].Add(package);
            }
            else
            {
                result.Add(dialogue.Item.StringId, new List<DialoguePackage>()
                {
                    package
                });
            }
        }
    }
    return result;
}
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high