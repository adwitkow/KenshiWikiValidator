using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;

namespace DialogueDumper
{
    internal class DialogueTreeCreator
    {
        private readonly Stack<DialogueLine> dialogueStack;
        private readonly IItemRepository itemRepository;
        private readonly DialogueNodeFactory dialogueNodeFactory;

        public DialogueTreeCreator(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            this.dialogueNodeFactory = new DialogueNodeFactory();
            this.dialogueStack = new Stack<DialogueLine>();
        }

        public string Create(Character character)
        {
            var results = new List<string>();
            this.dialogueStack.Clear();

            var packages = character.DialoguePackage.Concat(character.DialoguePackagePlayer)
                .Select(packageRef => packageRef.Item);

            var dialogueMap = this.itemRepository.GetItems<DialoguePackage>()
                .SelectMany(package => package.Dialogs)
                .ToLookup(dialogueRef => dialogueRef.Item, dialogueRef => dialogueRef);

            var dialogues = packages
                .SelectMany(package => package.Dialogs
                    .Select(dialogueRef => dialogueRef.Item))
                .ToList();

            var dialogueIdTocharacter = this.MapAllDialogues(character, ref dialogues);

            foreach (var dialogue in dialogues)
            {
                var dialogueRefs = dialogueMap[dialogue];

                var allEvents = new List<DialogueEvent>();
                foreach (var dialogueRef in dialogueRefs)
                {
                    allEvents.Add((DialogueEvent)dialogueRef.Value0);
                    allEvents.Add((DialogueEvent)dialogueRef.Value1);
                    allEvents.Add((DialogueEvent)dialogueRef.Value2);
                }

                var events = allEvents.Distinct().Except(new[] { DialogueEvent.EV_NONE });

                if (!events.Any())
                {
                    events = new[] { DialogueEvent.EV_NONE };
                }

                var sectionBuilder = new WikiSectionBuilder();
                sectionBuilder.WithHeader($"{dialogue.Name} ({string.Join(", ", events)})");

                var validCharacters = dialogueIdTocharacter[dialogue.StringId];
                var speakers = CreateSpeakersDictionary(events, validCharacters);

                var allLines = new List<DialogueNode>();
                var lines = dialogue.Lines.Select(lineRef => lineRef.Item);
                this.AddDialogueLines(allLines, null, 1, lines, speakers, character.Name, false);

                var roots = allLines
                    .Where(node => !allLines
                        .Any(n => n.Children.Contains(node)))
                    .ToList();
                RecalculateLevels(1, roots);

                foreach (var line in allLines)
                {
                    if (line.Conditions.Any())
                    {
                        sectionBuilder.WithLine($"{new string('*', line.Level)} ''If all of the following conditions are '''true''':''");
                        foreach (var condition in line.Conditions)
                        {
                            sectionBuilder.WithLine($"{new string('*', line.Level + 1)} ''{condition}''");
                        }
                    }

                    sectionBuilder.WithLine(line.ToString());
                }

                results.Add(sectionBuilder.Build());
            }

            return string.Join(Environment.NewLine, results);
        }

        private void RecalculateLevels(int level, ICollection<DialogueNode> nodes)
        {
            foreach (var node in nodes)
            {
                node.Level = level;

                var nextLevel = CalculateNextLevel(level, nodes.Count, node.Children.Count, false);
                RecalculateLevels(nextLevel, node.Children);
            }
        }

        private static Dictionary<DialogueSpeaker, IEnumerable<string>> CreateSpeakersDictionary(IEnumerable<DialogueEvent> events, List<string> validCharacters)
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

            if (events.Count() == 1 && events.First() == DialogueEvent.EV_PLAYER_TALK_TO_ME)
            {
                speakers[DialogueSpeaker.Target] = new[] { "Player" };
            }

            return speakers;
        }

        private Dictionary<string, List<string>> MapAllDialogues(Character character, ref List<Dialogue> dialogues)
        {
            var dialogueIdTocharacter = dialogues
                .Distinct()
                .ToDictionary(dialogue => dialogue.StringId, _ => new[] { character.Name }
                .ToList());
            var externalPackages = this.itemRepository
                .GetItems()
                .OfType<DialoguePackage>();

            foreach (var package in externalPackages)
            {
                var characters = this.itemRepository.GetItems<Character>()
                    .Where(character => character.DialoguePackage
                        .Any(characterPackage => characterPackage.Item == package))
                    .Concat(this.itemRepository.GetItems<Character>()
                        .Where(character => character.DialoguePackagePlayer
                            .Any(characterPackage => characterPackage.Item == package)));

                string? packageOwnerName = GetPackageOwnerName(characters);

                var externalDialogues = package.Dialogs
                    .Select(dialogue => dialogue.Item);
                foreach (var dialogue in externalDialogues)
                {
                    MapDialogueItem(character, dialogues, dialogueIdTocharacter, dialogue, packageOwnerName);
                }
            }

            var parentlessDialogues = this.itemRepository
                .GetItems<Dialogue>()
                .Except(dialogues);
            foreach (var dialogue in parentlessDialogues)
            {
                MapDialogueItem(character, dialogues, dialogueIdTocharacter, dialogue, null);
            }

            dialogues = dialogues.Distinct().ToList();

            return dialogueIdTocharacter;
        }

        private static string? GetPackageOwnerName(IEnumerable<Character> referencingItems)
        {
            string? packageOwnerName;
            if (referencingItems.Count() > 1)
            {
                packageOwnerName = null;
            }
            else
            {
                packageOwnerName = referencingItems.SingleOrDefault()?.Name;
            }

            return packageOwnerName;
        }

        private static void MapDialogueItem(Character character, List<Dialogue> dialogues, Dictionary<string, List<string>> dialogueIdTocharacter, Dialogue dialogue, string? packageOwnerName)
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

            if (dialogue.IsCharacter.Any())
            {
                dialogueIdTocharacter[dialogue.StringId].Add(dialogue.IsCharacter.Single().Item.Name);
            }

            if (dialogue.IsCharacter.Any(speaker => speaker.Item.StringId == character.StringId))
            {
                dialogues.Add(dialogue);
                return;
            }

            FindSpeakerDialoguesFromLines(character, dialogues, dialogue);
        }

        private static void FindSpeakerDialoguesFromLines(Character character, List<Dialogue> dialogues, Dialogue dialogue)
        {
            var startingLines = dialogue.Lines.Select(lineRef => lineRef.Item);
            var linesQueue = new Queue<DialogueLine>(startingLines);
            var processedLines = new HashSet<string>();
            while (linesQueue.TryDequeue(out var dequeued))
            {
                var dequeuedLines = dequeued.Lines.Where(line => !processedLines.Contains(line.Item.StringId) && !linesQueue.Contains(line.Item));
                foreach (var line in dequeuedLines)
                {
                    linesQueue.Enqueue(line.Item);
                }

                processedLines.Add(dequeued.StringId);

                if (dequeued.IsCharacter.Any(speaker => speaker.Item.StringId == character.StringId))
                {
                    dialogues.Add(dialogue);
                }
            }
        }

        private bool AddDialogueLines(IList<DialogueNode> allLines, DialogueNode? previousNode, int level, IEnumerable<DialogueLine> dialogueLines, Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, string characterName, bool isSearchedCharactersLine)
        {
            var isSearchedCharactersLineResult = false;
            foreach (var line in dialogueLines)
            {
                var isSearchedCharactersLineInternal = false;
                if (this.dialogueStack.Contains(line))
                {
                    continue;
                }

                this.dialogueStack.Push(line);

                var newSpeakersMap = RecreateSpeakersMap(speakersMap, line);

                if (newSpeakersMap[line.Speaker].Contains(characterName) || isSearchedCharactersLine)
                {
                    isSearchedCharactersLineInternal = true;
                }

                var text = line.Text0;
                var isInterjection = string.IsNullOrEmpty(text);
                var lineIdToRemove = isInterjection ? -1 : allLines.Count;

                var currentNode = GetCurrentNode(allLines, previousNode, level, line, newSpeakersMap, isInterjection);

                var lines = line.Lines.Select(lineRef => lineRef.Item);
                var stackContainsCharacter = this.AddDialogueLines(allLines, currentNode, level + 1, lines, newSpeakersMap, characterName, isSearchedCharactersLineInternal);

                if (stackContainsCharacter || isSearchedCharactersLineInternal)
                {
                    isSearchedCharactersLineResult = true;
                }

                if (!stackContainsCharacter && !isSearchedCharactersLineInternal && !isInterjection && allLines.Any())
                {
                    RemoveCurrentNode(allLines, previousNode, lineIdToRemove, currentNode);
                }

                this.dialogueStack.Pop();
            }

            return isSearchedCharactersLineResult;
        }

        private static void RemoveCurrentNode(IList<DialogueNode> allLines, DialogueNode? previousNode, int lineIdToRemove, DialogueNode? currentNode)
        {
            allLines.RemoveAt(lineIdToRemove);

            if (previousNode is not null && currentNode is not null)
            {
                previousNode.Children.Remove(currentNode);
            }
        }

        private DialogueNode? GetCurrentNode(IList<DialogueNode> allLines, DialogueNode? previousNode, int level, DialogueLine line, Dictionary<DialogueSpeaker, IEnumerable<string>> newSpeakersMap, bool isInterjection)
        {
            DialogueNode? currentNode;
            if (isInterjection)
            {
                currentNode = previousNode;
            }
            else
            {
                currentNode = this.CreateNewNode(allLines, previousNode, level, line, newSpeakersMap);
            }

            return currentNode;
        }

        private DialogueNode CreateNewNode(IList<DialogueNode> allLines, DialogueNode? previousNode, int level, DialogueLine line, Dictionary<DialogueSpeaker, IEnumerable<string>> newSpeakersMap)
        {
            var validSpeakers = newSpeakersMap[line.Speaker];
            var currentNode = this.dialogueNodeFactory.Create(line, level, validSpeakers, newSpeakersMap);

            if (previousNode != null)
            {
                previousNode.Children.Add(currentNode);
            }

            allLines.Add(currentNode);

            return currentNode;
        }

        private static Dictionary<DialogueSpeaker, IEnumerable<string>> RecreateSpeakersMap(Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, DialogueLine line)
        {
            var newSpeakersMap = speakersMap;
            if (line.IsCharacter.Any())
            {
                var characterNames = line.IsCharacter.Select(character => character.Item.Name);
                newSpeakersMap = new Dictionary<DialogueSpeaker, IEnumerable<string>>(speakersMap)
                {
                    [line.Speaker] = characterNames
                };
            }

            return newSpeakersMap;
        }

        private static int CalculateNextLevel(int level, int dialogueLinesCount, int nextLinesCount, bool isInterjection)
        {
            int nextLevel;

            if ((dialogueLinesCount > 1 || nextLinesCount > 1) && !isInterjection)
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
