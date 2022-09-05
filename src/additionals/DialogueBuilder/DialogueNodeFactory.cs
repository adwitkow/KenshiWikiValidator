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

            if (line.AiContract.Any())
            {
                throw new NotImplementedException("AiContract");
            }

            if (line.ChangeAi.Any())
            {
                throw new NotImplementedException("ChangeAi");
            }

            if (line.ChangeRelations.Any())
            {
                throw new NotImplementedException("ChangeRelations");
            }

            if (line.CrowdTrigger.Any())
            {
                throw new NotImplementedException("CrowdTrigger");
            }

            if (line.GiveItem.Any())
            {
                throw new NotImplementedException("GiveItem");
            }

            if (line.Interrupt.Any())
            {
                throw new NotImplementedException("Interrupt");
            }

            if (line.LockCampaign.Any())
            {
                throw new NotImplementedException("LockCampaign");
            }

            if (line.Locks.Any())
            {
                throw new NotImplementedException("Locks");
            }

            if (line.TriggerCampaign.Any())
            {
                throw new NotImplementedException("TriggerCampaign");
            }

            if (line.UnlockButKeepMe.Any())
            {
                throw new NotImplementedException("UnlockButKeepMe");
            }

            if (line.Unlocks.Any())
            {
                throw new NotImplementedException("Unlocks");
            }

            var effects = line.Effects.SelectItems();
            foreach (var effect in effects)
            {
                var effectValue = effect.ActionValue;

                var effectDescription = this.componentConverter.ConvertEffect(effect);
                if (effectDescription is null)
                {
                    results.Add($"EFFECT DESCRIPTION '{effect.ActionName}' NOT SET");
                    continue;
                }

                var description = effectDescription.GetDescription(speakerMap, line.Speaker, effectValue, dialogueEvents);

                results.Add(description);
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
                throw new NotImplementedException("InTownOf");
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
                throw new NotImplementedException("MyFaction");
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
                throw new NotImplementedException("TargetCarryingCharacter");
            }

            if (line.TargetFaction.Any())
            {
                results.Add($"Target's faction is {line.TargetFaction.ToCommaSeparatedListOr()}");
            }

            if (line.TargetHasItem.Any())
            {
                throw new NotImplementedException("TargetHasItem");
            }

            if (line.TargetHasItemType.Any())
            {
                throw new NotImplementedException("TargetHasItemType");
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

                results.Add(verbalizedDescription);
            }

            return results;
        }
    }
}
