using System.Collections.Concurrent;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Builders;
using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    internal class DialogueBuilder : ItemBuilderBase<DialoguePackage>
    {
        private readonly IItemRepository itemRepository;

        private readonly ConcurrentDictionary<string, DialogueLine> lineCache;
        private readonly ConcurrentDictionary<string, Dialogue> dialogueCache;

        // compare by
        //  0: ==
        //  1: <
        //  2: >
        // who
        //  0: me
        //  1: target
        //  2: interjector1
        //  3: interjector2
        //  4: interjector3
        //  5: whole squad
        //  6: target with race

        public DialogueBuilder(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            lineCache = new ConcurrentDictionary<string, DialogueLine>();
            dialogueCache = new ConcurrentDictionary<string, Dialogue>();
        }

        public override DialoguePackage Build(DataItem baseItem)
        {
            var inheritedItems = baseItem.GetReferenceItems(itemRepository, "inheritsFrom"); // do not forget about the inheritance
            var dialogueItems = baseItem.GetReferenceItems(itemRepository, "dialogs");

            var dialogues = BuildDialogues(dialogueItems);

            var resultPackage = new DialoguePackage()
            {
                StringId = baseItem.StringId,
                Name = baseItem.Name,
                Properties = baseItem.Values,
                Dialogues = dialogues,
            };

            return resultPackage;
        }

        private IEnumerable<Dialogue> BuildDialogues(IEnumerable<DataItem> dialogueItems)
        {
            var dialogues = new List<Dialogue>();
            foreach (var dialogueItem in dialogueItems)
            {
                var dialogue = ConvertDialogue(dialogueItem);

                dialogues.Add(dialogue);
            }

            return dialogues;
        }

        private Dialogue ConvertDialogue(DataItem dialogueItem)
        {
            Dialogue result;
            if (dialogueCache.ContainsKey(dialogueItem.StringId))
            {
                result = dialogueCache[dialogueItem.StringId];
            }
            else
            {
                result = new Dialogue()
                {
                    StringId = dialogueItem.StringId,
                    Name = dialogueItem.Name,
                    Properties = dialogueItem.Values,
                };

                dialogueCache.TryAdd(dialogueItem.StringId, result);

                result.Conditions = ConvertConditions(dialogueItem);
                result.Lines = ConvertLines(dialogueItem);
                result.WorldStates = ConvertWorldStates(dialogueItem);
                result.TargetItems = ConvertTargetItems(dialogueItem);
                result.TargetFactions = ConvertTargetedFaction(dialogueItem);
                result.TargetRaces = ConvertTargetedRaces(dialogueItem);
                result.SpeakerIsCharacter = ConvertCharacter(dialogueItem);
                result.InTownOfFactions = ConvertInTownOf(dialogueItem);
            }

            return result;
        }

        private IEnumerable<DialogueLine> ConvertLines(DataItem dialogueItem)
        {
            var lines = new List<DialogueLine>();
            var lineItems = dialogueItem.GetReferenceItems(itemRepository, "lines");

            foreach (var lineItem in lineItems)
            {
                DialogueLine line;
                if (!lineCache.ContainsKey(lineItem.StringId))
                {
                    line = new DialogueLine()
                    {
                        StringId = lineItem.StringId,
                        Name = lineItem.Name,
                        Properties = lineItem.Values,
                    };

                    lineCache.TryAdd(line.StringId, line);

                    line.Lines = ConvertLines(lineItem);
                    line.Conditions = ConvertConditions(lineItem);
                    line.Effects = ConvertEffects(lineItem);
                    line.UnlockedDialogues = ConvertUnlocks(lineItem);
                    line.RelationChanges = ConvertRelationChanges(lineItem);
                    line.TargetedFactions = ConvertTargetedFaction(lineItem);
                    line.TargetedRaces = ConvertTargetedRaces(lineItem);
                    line.CharactersCarriedByTarget = ConvertCarriedCharacters(lineItem);
                    line.WorldStates = ConvertWorldStates(lineItem);
                    line.InTownOfFactions = ConvertInTownOf(lineItem);
                    line.CrowdTriggers = ConvertCrowdTriggers(lineItem);
                    line.SpeakerRaces = ConvertSpeakerRace(lineItem);
                    line.SpeakerFactions = ConvertSpeakerFactions(lineItem);
                    line.TargetItems = ConvertTargetItems(lineItem);
                    line.SpeakerSubraces = ConvertSpeakerSubrace(lineItem);
                    line.SpeakerIsCharacter = ConvertCharacter(lineItem);
                    line.GivenItem = ConvertGivenItem(lineItem);
                    line.TriggeredCampaigns = ConvertTriggeredCampaigns(lineItem);
                }
                else
                {
                    line = lineCache[lineItem.StringId];
                }

                lines.Add(line);
            }

            return lines;
        }

        private IEnumerable<DataItem> ConvertTriggeredCampaigns(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "trigger campaign");
        }

        private IEnumerable<DataItem> ConvertGivenItem(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "give item");
        }

        private IEnumerable<DataItem> ConvertCharacter(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "is character");
        }

        private IEnumerable<DataItem> ConvertTargetItems(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "target has item type")
                .Concat(lineItem.GetReferenceItems(itemRepository, "target has item"));
        }

        private IEnumerable<DataItem> ConvertSpeakerFactions(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "my faction");
        }

        private IEnumerable<DataItem> ConvertSpeakerSubrace(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "my subrace");
        }

        private IEnumerable<DataItem> ConvertSpeakerRace(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "my race");
        }

        private IEnumerable<Dialogue> ConvertCrowdTriggers(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "crowd trigger")
                .Select(item => ConvertDialogue(item));
        }

        private IEnumerable<DataItem> ConvertInTownOf(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "in town of");
        }

        private IEnumerable<DataItem> ConvertWorldStates(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "world state");
        }

        private IEnumerable<DataItem> ConvertCarriedCharacters(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "target carrying character");
        }

        private IEnumerable<DataItem> ConvertTargetedFaction(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "target faction");
        }

        private IEnumerable<DataItem> ConvertTargetedRaces(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(itemRepository, "target race");
        }

        private IEnumerable<DialogueEffect> ConvertEffects(DataItem item)
        {
            var effects = new List<DialogueEffect>();
            var effectItems = item.GetReferenceItems(itemRepository, "effects");

            foreach (var effectItem in effectItems)
            {
                var effectName = (DialogueEffectName)effectItem.Values["action name"];
                var effect = new DialogueEffect()
                {
                    StringId = effectItem.StringId,
                    Name = effectItem.Name,
                    Properties = effectItem.Values,
                    EffectName = effectName,
                };
                effects.Add(effect);
            }

            return effects;
        }

        private IEnumerable<DialogueCondition> ConvertConditions(DataItem item)
        {
            var conditions = new List<DialogueCondition>();
            var conditionItems = item.GetReferenceItems(itemRepository, "conditions");

            foreach (var conditionItem in conditionItems)
            {
                var conditionName = (DialogueConditionName)conditionItem.Values["condition name"];
                var condition = new DialogueCondition()
                {
                    StringId = conditionItem.StringId,
                    Name = conditionItem.Name,
                    Properties = conditionItem.Values,
                    ConditionName = conditionName,
                };
                conditions.Add(condition);
            }

            return conditions;
        }

        private IEnumerable<Dialogue> ConvertUnlocks(DataItem item)
        {
            var dialogues = new List<Dialogue>();
            var dialogueItems = item.GetReferences("unlocks")
                .Concat(item.GetReferences("interrupt"))
                .Concat(item.GetReferences("unlock but keep me"))
                .Distinct()
                .Select(item => itemRepository.GetDataItemByStringId(item.TargetId));

            foreach (var dialogueItem in dialogueItems)
            {
                var dialogue = ConvertDialogue(dialogueItem);
                dialogues.Add(dialogue);
            }

            return dialogues;
        }

        private Dictionary<string, int> ConvertRelationChanges(DataItem item)
        {
            return item.GetReferences("change relations")
                .ToDictionary(reference => itemRepository.GetDataItemByStringId(reference.TargetId).Name, reference => reference.Value0);
        }
    }
}
