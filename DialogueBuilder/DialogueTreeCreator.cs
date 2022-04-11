using KenshiWikiValidator;
using KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.WikiSections;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace DialogueDumper
{
    internal class DialogueTreeCreator
    {
        private readonly IItemRepository itemRepository;
        private readonly DialogueBuilder dialogueBuilder;

        public DialogueTreeCreator(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            this.dialogueBuilder = new DialogueBuilder(itemRepository);
        }

        public string Create(DataItem character)
        {
            var results = new List<string>();

            var packages = character.GetReferenceItems(this.itemRepository, "dialogue package")
                .Concat(character.GetReferenceItems(this.itemRepository, "dialogue package player"));

            var dialogues = packages.SelectMany(package => this.dialogueBuilder.Build(package).Dialogues).ToList();

            var dialogueIdTocharacter = this.MapAllDialogues(character, ref dialogues);

            foreach (var dialogue in dialogues)
            {
                var sectionBuilder = new WikiSectionBuilder();
                sectionBuilder.WithHeader($"{dialogue.Name} ({string.Join(", ", dialogue.Events)})");

                var validCharacters = dialogueIdTocharacter[dialogue.StringId];
                var speakers = CreateSpeakersDictionary(dialogue, validCharacters);

                var dialogueStack = new Stack<DialogueLine>();
                var allLines = new List<string>();

                this.AddDialogueLines(allLines, 1, Enumerable.Empty<DialogueLine>(), dialogue.Lines, speakers, dialogueStack, character.Name, false);

                foreach (var line in allLines)
                {
                    sectionBuilder.WithLine(line);
                }

                results.Add(sectionBuilder.Build());
            }

            return string.Join(Environment.NewLine, results);
        }

        private static Dictionary<DialogueSpeaker, IEnumerable<string>> CreateSpeakersDictionary(Dialogue dialogue, List<string> validCharacters)
        {
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

            return speakers;
        }

        private Dictionary<string, List<string>> MapAllDialogues(DataItem character, ref List<Dialogue> dialogues)
        {
            var dialogueIdTocharacter = dialogues
                .ToDictionary(dialogue => dialogue.StringId, _ => new[] { character.Name }
                .ToList());
            var externalPackages = this.itemRepository
                .GetItems()
                .OfType<DialoguePackage>();

            foreach (var package in externalPackages)
            {
                var referencingItems = this.itemRepository
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

                    FindSpeakerDialoguesFromLines(character, dialogues, dialogue);
                }
            }

            dialogues = dialogues.Distinct().ToList();

            return dialogueIdTocharacter;
        }

        private static void FindSpeakerDialoguesFromLines(DataItem character, List<Dialogue> dialogues, Dialogue dialogue)
        {
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

        private bool AddDialogueLines(IList<string> allLines, int level, IEnumerable<DialogueLine> previousLines, IEnumerable<DialogueLine> dialogueLines, Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, Stack<DialogueLine> dialogueStack, string characterName, bool isSearchedCharactersLine)
        {
            var isSearchedCharactersLineResult = false;
            foreach (var line in dialogueLines)
            {
                var isSearchedCharactersLineInternal = false;
                if (dialogueStack.Contains(line))
                {
                    continue;
                }

                dialogueStack.Push(line);

                var newSpeakersMap = RecreateSpeakersMap(speakersMap, line);

                if (newSpeakersMap[line.Speaker].Contains(characterName) || isSearchedCharactersLine)
                {
                    isSearchedCharactersLineInternal = true;
                }

                var text = (string)line.Properties["text0"];
                var isInterjection = false;
                var lineIdToRemove = -1;
                string dialogueLine = null;
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

                    lineIdToRemove = allLines.Count;
                    dialogueLine = $"{new string('*', level)} '''{speakers}''': {text}";
                    allLines.Add(dialogueLine);
                }

                int nextLevel = CalculateNextLevel(level, previousLines.Count(), dialogueLines.Count(), line.Lines.Count(), isInterjection);

                var stackContainsCharacter = this.AddDialogueLines(allLines, nextLevel, dialogueLines, line.Lines, newSpeakersMap, dialogueStack, characterName, isSearchedCharactersLineInternal);

                if (stackContainsCharacter || isSearchedCharactersLineInternal)
                {
                    isSearchedCharactersLineResult = true;
                }

                if (!stackContainsCharacter && !isSearchedCharactersLineInternal && !isInterjection && allLines.Any())
                {
                    var lastLineId = allLines.Count - 1;
                    var lastLine = allLines[lineIdToRemove];

                    if (lastLine != dialogueLine)
                    {
                        throw new InvalidOperationException();
                    }

                    allLines.RemoveAt(lineIdToRemove);
                }

                dialogueStack.Pop();
            }

            return isSearchedCharactersLineResult;
        }

        private static Dictionary<DialogueSpeaker, IEnumerable<string>> RecreateSpeakersMap(Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, DialogueLine line)
        {
            var newSpeakersMap = speakersMap;
            if (line.SpeakerIsCharacter.Any())
            {
                var characterNames = line.SpeakerIsCharacter.Select(character => character.Name);
                newSpeakersMap = new Dictionary<DialogueSpeaker, IEnumerable<string>>(speakersMap);
                newSpeakersMap[line.Speaker] = characterNames;
            }

            return newSpeakersMap;
        }

        private static int CalculateNextLevel(int level, int previousLinesCount, int dialogueLinesCount, int nextLinesCount, bool isInterjection)
        {
            int nextLevel;
            if ((previousLinesCount > 1 || dialogueLinesCount > 1 || nextLinesCount > 1) && !isInterjection)
            {
                nextLevel = level + 1;
            }
            else
            {
                nextLevel = level;
            }

            return nextLevel;
        }
    }
}
