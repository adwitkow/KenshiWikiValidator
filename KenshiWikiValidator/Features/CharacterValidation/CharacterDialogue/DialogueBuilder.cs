using System.Collections.Concurrent;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Builders;
using OpenConstructionSet.Data.Models;

namespace KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue
{
    public class DialogueBuilder : ItemBuilderBase<DialoguePackage>
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

            this.lineCache = new ConcurrentDictionary<string, DialogueLine>();
            this.dialogueCache = new ConcurrentDictionary<string, Dialogue>();
        }

        public override DialoguePackage Build(DataItem baseItem)
        {
            var inheritedItems = baseItem.GetReferenceItems(this.itemRepository, "inheritsFrom"); // do not forget about the inheritance
            var dialogueItems = baseItem.GetReferenceItems(this.itemRepository, "dialogs");

            var dialogues = this.BuildDialogues(dialogueItems);

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
                var dialogue = this.ConvertDialogue(dialogueItem);

                dialogues.Add(dialogue);
            }

            return dialogues;
        }

        private Dialogue ConvertDialogue(DataItem dialogueItem)
        {
            Dialogue result;
            if (this.dialogueCache.ContainsKey(dialogueItem.StringId))
            {
                result = this.dialogueCache[dialogueItem.StringId];
            }
            else
            {
                result = new Dialogue()
                {
                    StringId = dialogueItem.StringId,
                    Name = dialogueItem.Name,
                    Properties = dialogueItem.Values,
                };

                this.dialogueCache.TryAdd(dialogueItem.StringId, result);

                result.Conditions = this.ConvertConditions(dialogueItem);
                result.Lines = this.ConvertLines(dialogueItem);
                result.WorldStates = this.ConvertWorldStates(dialogueItem);
                result.TargetItems = this.ConvertTargetItems(dialogueItem);
                result.TargetFactions = this.ConvertTargetedFaction(dialogueItem);
                result.TargetRaces = this.ConvertTargetedRaces(dialogueItem);
                result.SpeakerIsCharacter = this.ConvertCharacter(dialogueItem);
                result.InTownOfFactions = this.ConvertInTownOf(dialogueItem);
            }

            return result;
        }

        private IEnumerable<DialogueLine> ConvertLines(DataItem dialogueItem)
        {
            var lines = new List<DialogueLine>();
            var lineItems = dialogueItem.GetReferenceItems(this.itemRepository, "lines");

            foreach (var lineItem in lineItems)
            {
                DialogueLine line;
                if (!this.lineCache.ContainsKey(lineItem.StringId))
                {
                    line = new DialogueLine()
                    {
                        StringId = lineItem.StringId,
                        Name = lineItem.Name,
                        Properties = lineItem.Values,
                    };

                    this.lineCache.TryAdd(line.StringId, line);

                    line.Lines = this.ConvertLines(lineItem);
                    line.Conditions = this.ConvertConditions(lineItem);
                    line.Effects = this.ConvertEffects(lineItem);
                    line.UnlockedDialogues = this.ConvertUnlocks(lineItem);
                    line.RelationChanges = this.ConvertRelationChanges(lineItem);
                    line.TargetedFactions = this.ConvertTargetedFaction(lineItem);
                    line.TargetedRaces = this.ConvertTargetedRaces(lineItem);
                    line.CharactersCarriedByTarget = this.ConvertCarriedCharacters(lineItem);
                    line.WorldStates = this.ConvertWorldStates(lineItem);
                    line.InTownOfFactions = this.ConvertInTownOf(lineItem);
                    line.CrowdTriggers = this.ConvertCrowdTriggers(lineItem);
                    line.SpeakerRaces = this.ConvertSpeakerRace(lineItem);
                    line.SpeakerFactions = this.ConvertSpeakerFactions(lineItem);
                    line.TargetItems = this.ConvertTargetItems(lineItem);
                    line.SpeakerSubraces = this.ConvertSpeakerSubrace(lineItem);
                    line.SpeakerIsCharacter = this.ConvertCharacter(lineItem);
                    line.GivenItem = this.ConvertGivenItem(lineItem);
                    line.TriggeredCampaigns = this.ConvertTriggeredCampaigns(lineItem);
                }
                else
                {
                    line = this.lineCache[lineItem.StringId];
                }

                lines.Add(line);
            }

            return lines;
        }

        private IEnumerable<DataItem> ConvertTriggeredCampaigns(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "trigger campaign");
        }

        private IEnumerable<DataItem> ConvertGivenItem(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "give item");
        }

        private IEnumerable<DataItem> ConvertCharacter(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "is character");
        }

        private IEnumerable<DataItem> ConvertTargetItems(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "target has item type")
                .Concat(lineItem.GetReferenceItems(this.itemRepository, "target has item"));
        }

        private IEnumerable<DataItem> ConvertSpeakerFactions(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "my faction");
        }

        private IEnumerable<DataItem> ConvertSpeakerSubrace(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "my subrace");
        }

        private IEnumerable<DataItem> ConvertSpeakerRace(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "my race");
        }

        private IEnumerable<Dialogue> ConvertCrowdTriggers(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "crowd trigger")
                .Select(item => this.ConvertDialogue(item));
        }

        private IEnumerable<DataItem> ConvertInTownOf(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "in town of");
        }

        private IEnumerable<DataItem> ConvertWorldStates(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "world state");
        }

        private IEnumerable<DataItem> ConvertCarriedCharacters(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "target carrying character");
        }

        private IEnumerable<DataItem> ConvertTargetedFaction(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "target faction");
        }

        private IEnumerable<DataItem> ConvertTargetedRaces(DataItem lineItem)
        {
            return lineItem.GetReferenceItems(this.itemRepository, "target race");
        }

        private IEnumerable<DialogueEffect> ConvertEffects(DataItem item)
        {
            var effects = new List<DialogueEffect>();
            var effectItems = item.GetReferenceItems(this.itemRepository, "effects");

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
            var conditionItems = item.GetReferenceItems(this.itemRepository, "conditions");

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
                .Select(item => this.itemRepository.GetDataItemByStringId(item.TargetId));

            foreach (var dialogueItem in dialogueItems)
            {
                var dialogue = this.ConvertDialogue(dialogueItem);
                dialogues.Add(dialogue);
            }

            return dialogues;
        }

        private Dictionary<string, int> ConvertRelationChanges(DataItem item)
        {
            return item.GetReferences("change relations")
                .ToDictionary(reference => this.itemRepository.GetDataItemByStringId(reference.TargetId).Name, reference => reference.Value0);
        }
    }
}
