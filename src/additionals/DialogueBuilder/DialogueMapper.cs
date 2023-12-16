using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;

namespace DialogueDumper
{
    public class DialogueMapper
    {
        private readonly IItemRepository itemRepository;
        private readonly DialogueNodeFactory nodeFactory;
        private readonly Stack<DialogueLine> dialogueStack;

        public DialogueMapper(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            this.nodeFactory = new DialogueNodeFactory();
            this.dialogueStack = new Stack<DialogueLine>();
        }

        public Dictionary<string, List<string>> MapAllDialogues(Character character, ref List<Dialogue> dialogues)
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

        public IList<DialogueNode> MapDialogueLines(IEnumerable<DialogueLine> dialogueLines, Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, string characterName, IEnumerable<DialogueEvent> dialogueEvents)
        {
            var allLines = new List<DialogueNode>();

            this.AddDialogueLines(allLines, null, dialogueLines, speakersMap, characterName, false, dialogueEvents);

            return allLines;
        }

        private bool AddDialogueLines(IList<DialogueNode> allLines, DialogueNode? previousNode, IEnumerable<DialogueLine> dialogueLines, Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, string characterName, bool isSearchedCharactersLine, IEnumerable<DialogueEvent> dialogueEvents)
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

                if (isInterjection)
                {
                    var children = line.Lines.Select(reference => reference.Item);
                    foreach (var child in children)
                    {
                        child.CopyReferencesFrom(line);
                    }
                }

                var currentNode = GetCurrentNode(allLines, previousNode, line, newSpeakersMap, isInterjection, dialogueEvents);

                var lines = line.Lines.Select(lineRef => lineRef.Item);
                var stackContainsCharacter = this.AddDialogueLines(allLines, currentNode, lines, newSpeakersMap, characterName, isSearchedCharactersLineInternal, dialogueEvents);

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

        private DialogueNode? GetCurrentNode(IList<DialogueNode> allLines, DialogueNode? previousNode, DialogueLine line, Dictionary<DialogueSpeaker, IEnumerable<string>> newSpeakersMap, bool isInterjection, IEnumerable<DialogueEvent> dialogueEvents)
        {
            DialogueNode? currentNode;
            if (isInterjection)
            {
                currentNode = previousNode;
            }
            else
            {
                currentNode = this.CreateNewNode(allLines, previousNode, line, newSpeakersMap, dialogueEvents);
            }

            return currentNode;
        }

        private DialogueNode CreateNewNode(IList<DialogueNode> allLines, DialogueNode? previousNode, DialogueLine line, Dictionary<DialogueSpeaker, IEnumerable<string>> newSpeakersMap, IEnumerable<DialogueEvent> dialogueEvents)
        {
            var validSpeakers = newSpeakersMap[line.Speaker];
            var currentNode = this.nodeFactory.Create(line, validSpeakers, newSpeakersMap, dialogueEvents);

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
    }
}
