// See https://aka.ms/new-console-template for more information

using KenshiWikiValidator;
using KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiSections;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

var repository = new ItemRepository();
repository.Load();

var dialogueBuilder = new DialogueBuilder(repository);
var characters = repository.GetDataItemsByType(ItemType.Character);
var beep = characters.Single(character => character.Name == "Beep");

WriteCharacterDialogue(repository, dialogueBuilder, beep);

void WriteDialogueTree(WikiSectionBuilder sectionBuilder, int level, IEnumerable<DialogueLine> dialogueLines)
{
    foreach (var line in dialogueLines)
    {
        sectionBuilder.WithLine($"{new string('*', level)} {line.Properties["text0"]}");
        var nextLevel = level + 1;
        WriteDialogueTree(sectionBuilder, nextLevel, line.Lines);
    }
}

void WriteCharacterDialogue(ItemRepository repository, DialogueBuilder dialogueBuilder, DataItem character)
{
    var dialogues = character.GetReferenceItems(repository, "dialogue package")
            .Concat(character.GetReferenceItems(repository, "dialogue package player"))
            .SelectMany(package => dialogueBuilder.Build(package).Dialogues);

    foreach (var dialogue in dialogues)
    {
        var sectionBuilder = new WikiSectionBuilder();
        sectionBuilder.WithHeader(dialogue.Name);

        WriteDialogueTree(sectionBuilder, 1, dialogue.Lines);

        Console.WriteLine(sectionBuilder.Build());
    }
}