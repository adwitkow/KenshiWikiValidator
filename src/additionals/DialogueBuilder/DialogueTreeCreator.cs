using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;

namespace DialogueDumper
{
    internal class DialogueTreeCreator
    {
        private readonly IItemRepository itemRepository;
        private readonly DialogueMapper dialogueMapper;
        private readonly NodeLevelCalculator levelCalculator;

        public DialogueTreeCreator(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            this.dialogueMapper = new DialogueMapper(itemRepository);
            this.levelCalculator = new NodeLevelCalculator();
        }

        public string Create(Character character)
        {
            var results = new List<string>();

            var packages = character.DialoguePackage.Concat(character.DialoguePackagePlayer)
                .Select(packageRef => packageRef.Item);

            var dialogueMap = this.itemRepository.GetItems<DialoguePackage>()
                .SelectMany(package => package.Dialogs)
                .ToLookup(dialogueRef => dialogueRef.Item, dialogueRef => dialogueRef);

            var dialogues = packages
                .SelectMany(package => package.Dialogs
                    .Select(dialogueRef => dialogueRef.Item))
                .ToList();

            var dialogueIdTocharacter = this.dialogueMapper.MapAllDialogues(character, ref dialogues);

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

                var lines = dialogue.Lines.Select(lineRef => lineRef.Item);
                var allLines = this.dialogueMapper.MapDialogueLines(lines, speakers, character.Name);

                var roots = allLines
                    .Where(node => !allLines
                        .Any(n => n.Children.Contains(node)))
                    .ToList();

                this.levelCalculator.CalculateLevels(1, roots);

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

                    foreach (var effect in line.Effects)
                    {
                        sectionBuilder.WithLine($"{new string('*', line.Level)} ''({effect})''");
                    }
                }

                results.Add(sectionBuilder.Build());
            }

            return string.Join(Environment.NewLine, results);
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
    }
}
