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

void WriteDialogueTree(WikiSectionBuilder sectionBuilder, int level, IEnumerable<DialogueLine> previousLines, IEnumerable<DialogueLine> dialogueLines, Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, Stack<DialogueLine> dialogueStack)
{
    foreach (var line in dialogueLines)
    {
        if (dialogueStack.Contains(line))
        {
            continue;
        }

        dialogueStack.Push(line);

        var newSpeakersMap = speakersMap;
        if (line.SpeakerIsCharacter.Any())
        {
            var characterNames = line.SpeakerIsCharacter.Select(character => character.Name);
            newSpeakersMap = new Dictionary<DialogueSpeaker, IEnumerable<string>>(speakersMap);
            newSpeakersMap[line.Speaker] = characterNames;
        }

        var text = (string)line.Properties["text0"];
        var isInterjection = false;
        if (string.IsNullOrEmpty(text))
        {
            isInterjection = true;
        }
        else
        {
            var validSpeakers = newSpeakersMap[line.Speaker];
            string speakers;
            if (validSpeakers.Count() > 1)
            {
                speakers = string.Join(", ", validSpeakers.SkipLast(1)) + " or " + validSpeakers.TakeLast(1).Single();
            }
            else
            {
                speakers = validSpeakers.Single();
            }

            sectionBuilder.WithLine($"{new string('*', level)} '''{speakers}''': {text}");
        }

        int nextLevel;
        if ((line.Lines.Count() > 1 || dialogueLines.Count() > 1 || previousLines.Count() > 1) && !isInterjection)
        {
            nextLevel = level + 1;
        }
        else
        {
            nextLevel = level;
        }

        WriteDialogueTree(sectionBuilder, nextLevel, dialogueLines, line.Lines, newSpeakersMap, dialogueStack);

        dialogueStack.Pop();
    }
}

void WriteCharacterDialogue(ItemRepository repository, DialogueBuilder dialogueBuilder, DataItem character)
{
    var packages = character.GetReferenceItems(repository, "dialogue package")
            .Concat(character.GetReferenceItems(repository, "dialogue package player"));

    var dialogues = packages.SelectMany(package => dialogueBuilder.Build(package).Dialogues).ToList();

    var externalPackages = repository.GetItems().OfType<DialoguePackage>();

    var dialogueIdTocharacter = dialogues.ToDictionary(dialogue => dialogue.StringId, _ => new[] { character.Name }.ToList());

    foreach (var package in externalPackages)
    {
        var referencingItems = repository
            .GetReferencingDataItemsFor(package.StringId);
        var packageOwnerName = referencingItems.FirstOrDefault(item => item.Type == ItemType.Character)?.Name;

        var externalDialogues = package.Dialogues;
        foreach (var dialogue in externalDialogues)
        {
            if (!dialogueIdTocharacter.TryGetValue(dialogue.StringId, out var list))
            {
                list = new List<string>();
                dialogueIdTocharacter.Add(dialogue.StringId, list);
            }

            if (!string.IsNullOrEmpty(packageOwnerName) && !list.Contains(packageOwnerName))
            {
                list.Add(packageOwnerName);
            }

            if (dialogue.SpeakerIsCharacter.Any())
            {
                dialogueIdTocharacter[dialogue.StringId].Add(dialogue.SpeakerIsCharacter.Single().Name);
            }

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
    }
    
    dialogues = dialogues.Distinct().ToList();

    foreach (var dialogue in dialogues)
    {
        var sectionBuilder = new WikiSectionBuilder();
        sectionBuilder.WithHeader($"{dialogue.Name} ({string.Join(", ", dialogue.Events)})");

        var validCharacters = dialogueIdTocharacter[dialogue.StringId];
        IEnumerable<string> mainSpeakers;
        if (validCharacters.Any())
        {
            mainSpeakers = validCharacters;
        }
        else
        {
            mainSpeakers = new[] { "Character" };
        }

        var speakers = new Dictionary<DialogueSpeaker, IEnumerable<string>>()
        {
            { DialogueSpeaker.Me, mainSpeakers },
            { DialogueSpeaker.Target, new[] { "Target" } },
            { DialogueSpeaker.TargetIfPlayer, new[] { "Player" } },
            { DialogueSpeaker.Interjector1, new[] { "Interjector #1" } },
            { DialogueSpeaker.Interjector2, new[] { "Interjector #2" } },
            { DialogueSpeaker.Interjector3, new[] { "Interjector #3" } },
            { DialogueSpeaker.TargetWithRace, new[] { "TODO: TargetWithRace" } },
            { DialogueSpeaker.WholeSquad, new[] { "Whole Squad" } },
        };

        if (dialogue.Events.Count() == 1 && dialogue.Events.First() == DialogueEvent.EV_PLAYER_TALK_TO_ME)
        {
            speakers[DialogueSpeaker.Target] = new[] { "Player" };
        }

        var dialogueStack = new Stack<DialogueLine>();
        WriteDialogueTree(sectionBuilder, 1, Enumerable.Empty<DialogueLine>(), dialogue.Lines, speakers, dialogueStack);

        var text = sectionBuilder.Build() + Environment.NewLine;

        var path = Path.Combine("characters", $"{character.Name}.txt");
        Directory.CreateDirectory("characters");
        File.AppendAllText(path, text);
    }
}