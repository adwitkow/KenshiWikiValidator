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
        if (!line.Effects.Any(effect => effect.Item.ActionName == DialogueEffect.DA_JOIN_SQUAD_WITH_EDIT || effect.Item.ActionName == DialogueEffect.DA_JOIN_SQUAD_FAST))
        {
            continue;
        }

        var usedPackages = dialogueToPackage[dialogue.StringId];

        var characters = usedPackages.SelectMany(package =>
            repository.GetItems<Character>()
                .Where(character => character.DialoguePackage.ContainsItem(package))
            .Concat(repository.GetItems<Character>()
                .Where(character => character.DialoguePackagePlayer.ContainsItem(package)))
            .Concat(repository.GetItems<Character>()
                .Where(character => character.Dialogue.ContainsItem(dialogue)))
            .Concat(repository.GetItems<Character>()
                .Where(character => character.AnnouncementDialogue.ContainsItem(dialogue))))
            .Select(item => item.Name)
            .Distinct()
            .ToList();

        foreach (var character in characters)
        {
            characterNames.Add($"{character} (Dialogue: {dialogue.Name})");
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
        foreach (var dialogueId in package.Dialogs.Select(dialogue => dialogue.Item.StringId))
        {
            if (result.ContainsKey(dialogueId))
            {
                result[dialogueId].Add(package);
            }
            else
            {
                result.Add(dialogueId, new List<DialoguePackage>()
                {
                    package
                });
            }
        }
    }
    return result;
}