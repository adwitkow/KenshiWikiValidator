using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;

namespace DialogueDumper
{
    public class DialogueNodeFactory
    {
        private readonly DialogueComponentConverter componentConverter;
        private readonly WorldStateVerbalizer worldStateVerbalizer;

        public DialogueNodeFactory()
        {
            this.componentConverter = new DialogueComponentConverter();
            this.worldStateVerbalizer = new WorldStateVerbalizer();
        }

        public DialogueNode Create(DialogueLine line, IEnumerable<string> speakers, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap, IEnumerable<DialogueEvent> dialogueEvents)
        {
            var text = line.Text0;

            return new DialogueNode()
            {
                Line = text,
                Speakers = speakers,
                Conditions = this.ConvertConditions(line, speakerMap),
                Effects = this.ConvertEffects(line, speakerMap, dialogueEvents),
            };
        }

        private IEnumerable<string> ConvertEffects(DialogueLine line, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap, IEnumerable<DialogueEvent> dialogueEvents)
        {
            var results = new List<string>();

            var speakers = speakerMap[line.Speaker].ToCommaSeparatedListOr();

            if (line.AiContract.Any())
            {
                results.Add($"{speakers}'s contract gets changed to '{line.AiContract.Single().Item.Name}'");
            }

            if (line.ChangeAi.Any())
            {
                results.Add($"{speakers}'s AI package gets changed to '{line.ChangeAi.Single().Item.Name}'");
            }

            if (line.ChangeRelations.Any())
            {
                foreach (var change in line.ChangeRelations)
                {
                    results.Add($"The relations with '{change.Item.Name}' are changed by {change.Value0}.");
                }
            }

            if (line.CrowdTrigger.Any())
            {
                results.Add($"'{line.CrowdTrigger.Single()}' dialogue gets triggered for all squad members of {speakers}.");
            }

            if (line.GiveItem.Any())
            {
                results.Add($"{speakers} gives {line.GiveItem.ToCommaSeparatedListAnd()} to the target.");
            }

            if (line.Interrupt.Any())
            {
                // I don't really care about this, to be honest.
            }

            if (line.LockCampaign.Any())
            {
                throw new NotImplementedException("LockCampaign");
            }

            if (line.Locks.Any())
            {
                results.Add($"'{line.Unlocks.ToCommaSeparatedListAnd()}' dialogue(s) gets locked for {speakers}.");
            }

            if (line.TriggerCampaign.Any())
            {
                results.Add($"'{line.TriggerCampaign.ToCommaSeparatedListAnd()}' campaign gets triggered.");
            }

            if (line.UnlockButKeepMe.Any())
            {
                results.Add($"'{line.UnlockButKeepMe.ToCommaSeparatedListAnd()}' dialogue(s) gets unlocked for {speakers}.");
            }

            if (line.Unlocks.Any())
            {
                results.Add($"'{line.Unlocks.ToCommaSeparatedListAnd()}' dialogue(s) gets unlocked for {speakers}.");
            }

            var effectRefs = line.Effects;
            foreach (var effectRef in effectRefs)
            {
                var effect = effectRef.Item;
                var effectValue = effectRef.Value0;

                var effectDescription = this.componentConverter.ConvertEffect(effect);
                if (effectDescription is null)
                {
                    results.Add($"EFFECT DESCRIPTION '{effect.ActionName}' NOT SET");
                    continue;
                }

                var description = effectDescription.GetDescription(speakerMap, line.Speaker, effectValue, dialogueEvents);

                results.Add($"{description} ({effect.ActionName}, {effectValue})");
            }

            return results;
        }

        private IEnumerable<string> ConvertConditions(DialogueLine line, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap)
        {
            var results = new List<string>();

            var speakers = speakerMap[line.Speaker].ToCommaSeparatedListOr();

            if (line.HasPackage.Any())
            {
                results.Add($"{speakers} has package {line.HasPackage.ToCommaSeparatedListOr()}");
            }

            if (line.InTownOf.Any())
            {
                results.Add($"{speakers} is in a location that belongs to {line.MyFaction.ToCommaSeparatedListOr()}");
            }

            if (line.IsCharacter.Any())
            {
                if (!speakers.Equals(line.IsCharacter.ToCommaSeparatedListOr()))
                {
                    throw new InvalidOperationException("Speakers list is different from the 'is character' condition");
                }
            }

            if (line.MyFaction.Any())
            {
                results.Add($"{speakers}'s faction is {line.MyFaction.ToCommaSeparatedListOr()}");
            }

            if (line.MyRace.Any())
            {
                results.Add($"{speakers}'s race is {line.MyRace.ToCommaSeparatedListOr()}");
            }

            if (line.MySubrace.Any())
            {
                results.Add($"{speakers}'s subrace is {line.MySubrace.ToCommaSeparatedListOr()}");
            }

            if (line.TargetCarryingCharacter.Any())
            {
                results.Add($"Target is carrying {line.TargetCarryingCharacter.ToCommaSeparatedListOr()}");
            }

            if (line.TargetFaction.Any())
            {
                results.Add($"Target's faction is {line.TargetFaction.ToCommaSeparatedListOr()}");
            }

            if (line.TargetHasItem.Any())
            {
                results.Add($"Target has {line.TargetHasItem.ToCommaSeparatedListOr()}");
            }

            if (line.TargetHasItemType.Any())
            {
                results.Add($"Target owns an item of the same type as {line.TargetHasItemType.ToCommaSeparatedListOr()}");
            }

            if (line.TargetRace.Any())
            {
                results.Add($"Target's race is {line.TargetRace.ToCommaSeparatedListOr()}");
            }

            if (line.WorldState.Any())
            {
                results.Add(this.worldStateVerbalizer.Verbalize(line.WorldState));
            }

            var conditionReferences = line.Conditions;
            foreach (var conditionRef in conditionReferences)
            {
                var condition = conditionRef.Item;
                var conditionValue = conditionRef.Value0;

                var conditionSpeakers = speakerMap[condition.Who];

                string validSpeakers;
                if (conditionSpeakers.Count() > 1)
                {
                    validSpeakers = conditionSpeakers.ToCommaSeparatedListOr();
                }
                else
                {
                    validSpeakers = conditionSpeakers.Single();
                }

                var conditionDescription = this.componentConverter.ConvertCondition(condition);
                if (conditionDescription is null)
                {
                    results.Add($"CONDITION DESCRIPTION '{condition.ConditionName}' NOT SET");
                    continue;
                }

                var verbalizedDescription = conditionDescription.GetDescription(validSpeakers, conditionValue, condition.CompareBy, condition.Tag);

                results.Add($"{verbalizedDescription} ({condition.ConditionName}, {conditionValue})");
            }

            return results;
        }
    }
}
