// See https://aka.ms/new-console-template for more information

using KenshiWikiValidator;
using KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiSections;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

if (Directory.Exists("characters"))
{
    Directory.Delete("characters", true);
}

var repository = new ItemRepository();
repository.Load();

var dialogueBuilder = new DialogueBuilder(repository);
var characters = repository.GetDataItemsByType(ItemType.Character);
var beep = characters.Single(character => character.Name == "Beep");

WriteCharacterDialogue(repository, dialogueBuilder, beep);

void WriteDialogueTree(WikiSectionBuilder sectionBuilder, int level, IEnumerable<DialogueLine> dialogueLines, Dictionary<DialogueSpeaker, string> speakers, Stack<DialogueLine> dialogueStack)
{
    foreach (var line in dialogueLines)
    {
        if (dialogueStack.Contains(line))
        {
            continue;
        }

        dialogueStack.Push(line);

        var text = (string)line.Properties["text0"];
        var isInterjection = false;
        if (string.IsNullOrEmpty(text))
        {
            isInterjection = true;
        }
        else
        {
            sectionBuilder.WithLine($"{new string('*', level)} '''{speakers[line.Speaker]}''': {text}");
        }

        int nextLevel;
        if ((line.Lines.Count() > 1 || dialogueLines.Count() > 1) && !isInterjection)
        {
            nextLevel = level + 1;
        }
        else
        {
            nextLevel = level;
        }

        WriteDialogueTree(sectionBuilder, nextLevel, line.Lines, speakers, dialogueStack);

        dialogueStack.Pop();
    }
}

void WriteCharacterDialogue(ItemRepository repository, DialogueBuilder dialogueBuilder, DataItem character)
{
    var packages = character.GetReferenceItems(repository, "dialogue package")
            .Concat(character.GetReferenceItems(repository, "dialogue package player"));

    var dialogues = packages.SelectMany(package => dialogueBuilder.Build(package).Dialogues).ToList();

    var externalDialogues = repository.GetItems().OfType<DialoguePackage>()
        .SelectMany(package => package.Dialogues);

    foreach (var dialogue in externalDialogues)
    {
        if (dialogue.SpeakerIsCharacter.Any(speaker => speaker.StringId == character.StringId))
        {
            dialogues.Add(dialogue);
            continue;
        }

        var linesQueue = new Queue<DialogueLine>(dialogue.Lines);
        var processedLines = new HashSet<string>();
        while (linesQueue.TryDequeue(out var dequeued))
        {
            foreach (var line in dequeued.Lines)
            {
                if (!processedLines.Contains(line.StringId) && !linesQueue.Contains(line))
                {
                    linesQueue.Enqueue(line);
                }
            }

            processedLines.Add(dequeued.StringId);

            if (dequeued.SpeakerIsCharacter.Any(speaker => speaker.StringId == character.StringId))
            {
                dialogues.Add(dialogue);
            }
        }
    }

    dialogues = dialogues.Distinct().ToList();

    foreach (var dialogue in dialogues)
    {
        var sectionBuilder = new WikiSectionBuilder();
        sectionBuilder.WithHeader($"{dialogue.Name} ({string.Join(", ", dialogue.Events)})");

        var speakers = new Dictionary<DialogueSpeaker, string>()
        {
            { DialogueSpeaker.Me, character.Name },
            { DialogueSpeaker.Target, "Target" },
            { DialogueSpeaker.TargetIfPlayer, "Player" },
            { DialogueSpeaker.Interjector1, "Interjector #1" },
            { DialogueSpeaker.Interjector2, "Interjector #2" },
            { DialogueSpeaker.Interjector3, "Interjector #3" },
            { DialogueSpeaker.TargetWithRace, "TODO: TargetWithRace" },
            { DialogueSpeaker.WholeSquad, "Whole Squad" },
        };

        if (dialogue.Events.Count() == 1 && dialogue.Events.First() == DialogueEvent.EV_PLAYER_TALK_TO_ME)
        {
            speakers[DialogueSpeaker.Target] = "Player";
        }

        var dialogueStack = new Stack<DialogueLine>();
        WriteDialogueTree(sectionBuilder, 1, dialogue.Lines, speakers, dialogueStack);

        var text = sectionBuilder.Build() + Environment.NewLine;

        var path = Path.Combine("characters", $"{character.Name}.txt");
        Directory.CreateDirectory("characters");
        File.AppendAllText(path, text);
    }
}