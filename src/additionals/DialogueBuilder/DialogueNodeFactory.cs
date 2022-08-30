using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;

namespace DialogueDumper
{
    public class DialogueNodeFactory
    {
        private readonly ConditionMap conditionMap;
        private readonly WorldStateVerbalizer worldStateVerbalizer;

        public DialogueNodeFactory()
        {
            this.conditionMap = new ConditionMap();
            this.worldStateVerbalizer = new WorldStateVerbalizer();
        }

        public DialogueNode Create(DialogueLine line, int level, IEnumerable<string> speakers, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap)
        {
            var text = line.Text0;

            return new DialogueNode()
            {
                Level = level,
                Line = text,
                Speakers = speakers,
                Conditions = this.ConvertConditions(line, speakerMap),
                Effects = this.ConvertEffects(line, speakerMap),
            };
        }

        private IEnumerable<string> ConvertEffects(DialogueLine line, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap)
        {
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

            return Enumerable.Empty<string>();
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
                throw new NotImplementedException("TargetFaction");
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

                var conditionDescription = this.conditionMap[condition.ConditionName];
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

        private sealed class ConditionMap
        {
            private static readonly Dictionary<DialogueCondition, IConditionDescription?> map = new()
            {
                { DialogueCondition.DC_NONE, null },
                { DialogueCondition.DC_RELATIONS, null },
                { DialogueCondition.DC_PLAYERMONEY, null },
                { DialogueCondition.DC_REPUTATION, null },
                { DialogueCondition.DC_CARRYING_BOUNTY_ALIVE, null },
                { DialogueCondition.DC_CARRYING_BOUNTY_DEAD, null },
                { DialogueCondition.DC_FACTION_VARIABLE, null },
                { DialogueCondition.DC_IMPRISONED_BY_TARGET, null },
                { DialogueCondition.DC_IMPRISONED_BY_OTHER, null },
                { DialogueCondition.DC_IS_A_TRADER, null },
                { DialogueCondition.DC_FACTION_RANK, null },
                { DialogueCondition.DC_BUILDING_IS_CLOSED_AND_SECURED, null },
                { DialogueCondition.DC_PLAYER_TECH_LEVEL, null },
                { DialogueCondition.DC_NUM_DIALOG_EVENT_REPEATS, null },
                { DialogueCondition.DC_IS_IMPRISONED, new BooleanDescription("{0} is imprisoned", "{0} is not imprisoned") },
                { DialogueCondition.DC_IMPRISONMENT_IS_DEATHROW, null },
                { DialogueCondition.DC_TARGET_IN_TALKING_RANGE, new BooleanDescription("{0} is nearby", "{0} is not nearby") },
                { DialogueCondition.DC_IN_MY_BUILDING, null },
                { DialogueCondition.DC_TARGET_LAST_SEEN_X_HOURS_AGO, null },
                { DialogueCondition.DC_IS_LEADER, null },
                { DialogueCondition.DC_MET_TARGET_BEFORE, null },
                { DialogueCondition.DC_WEAKER_THAN_ME, null },
                { DialogueCondition.DC_STRONGER_THAN_ME, null },
                { DialogueCondition.DC_HAS_TAG, null },
                { DialogueCondition.DC_IS_ALLY, null },
                { DialogueCondition.DC_IS_ENEMY, null },
                { DialogueCondition.DC_PERSONALITY_TAG, new TaggedBooleanDescription("{0} has {1} personality", "{0} does not have {1} personality", typeof(PersonalityTag))},
                { DialogueCondition.DC_BROKEN_LEG, null },
                { DialogueCondition.DC_BROKEN_ARM, null },
                { DialogueCondition.DC_DAMAGED_HEAD, null },
                { DialogueCondition.DC_NEARLY_KO, null },
                { DialogueCondition.DC_IN_A_NON_PLAYER_TOWN, new BooleanDescription("{0} is in a town (not including towns owned by the player)", "{0} is not in a town (not including towns owned by the player)") },
                { DialogueCondition.DC_IS_RUNNING, null },
                { DialogueCondition.DC_COPS_AROUND, null },
                { DialogueCondition.NULL_NULL_____DC_TARGET_SQUAD_SIZE, null },
                { DialogueCondition.DC_SQUAD_SIZE, new ConditionDescription("Player's squad size is exactly {2}", "Player's squad size is smaller than {2}", "Player's squad size is bigger than {2}")  },
                { DialogueCondition.DC_IS_PLAYER, null },
                { DialogueCondition.DC_NUM_BACKPACKS, null },
                { DialogueCondition.DC_SQUAD_ONLY_ANIMALS, null },
                { DialogueCondition.DC_IS_OUTNUMBERED, null },
                { DialogueCondition.DC_BOUNTY_AMOUNT_PERCEIVED, null },
                { DialogueCondition.DC_IS_KO, new BooleanDescription("{0} is unconscious", "{0} is not unconscious") },
                { DialogueCondition.DC_IS_NEARLY_KO, null },
                { DialogueCondition.DC_SQUAD_IS_DOWN, null },
                { DialogueCondition.DC_IS_DEAD, null },
                { DialogueCondition.DC_IS_FEMALE, new BooleanDescription("{0} is female", "{0} is male") },
                { DialogueCondition.DC_CARRYING_SOMEONE_TO_ENSLAVE, null },
                { DialogueCondition.DC_BOUNTY_AMOUNT_ACTUAL, null },
                { DialogueCondition.DC_IM_UNARMED, null },
                { DialogueCondition.DC_TOWN_HAS_FORTIFICATIONS_WALLS, null },
                { DialogueCondition.DC_TARGET_IS_MY_MISSION_TARGET, null },
                { DialogueCondition.DC_MY_MISSION_IS_FRIENDLY, null },
                { DialogueCondition.DC_I_LOVE_THIS_GUY, null },
                { DialogueCondition.DC_I_HATE_THIS_GUY, null },
                { DialogueCondition.DC_I_SHOULD_SCREW_THIS_GUY_OVER, null },
                { DialogueCondition.DC_I_SHOULD_HELP_THIS_GUY, null },
                { DialogueCondition.DC_IN_COMBAT, null },
                { DialogueCondition.DC_WITHIN_TOWN_WALLS, null },
                { DialogueCondition.DC_TOWN_WALLS_LOCKED_UP, null },
                { DialogueCondition.DC_IS_SLAVE, null },
                { DialogueCondition.DC_HAS_A_BASE_NEARBY, null },
                { DialogueCondition.DC_TARGET_IS_SLAVE_OF_MY_FACTION, null },
                { DialogueCondition.DC_IS_ESCAPED_SLAVE, null },
                { DialogueCondition.DC_IS_IN_LOCKED_CAGE, new BooleanDescription("{0} is locked in a cage", "{0} is not locked in a cage") },
                { DialogueCondition.DC_WEARING_LOCKED_SHACKLES, null },
                { DialogueCondition.DC_IS_SAME_RACE_AS_ME, null },
                { DialogueCondition.DC_CAN_AFFORD_BOUNTY, null },
                { DialogueCondition.DC_IS_SNEAKING, null },
                { DialogueCondition.DC_IS_INDOORS, null },
                { DialogueCondition.DC_HAS_ILLEGAL_ITEM, null },
                { DialogueCondition.DC_USING_MY_TRAINING_EQUIPMENT, null },
                { DialogueCondition.DC_STARVING, null },
                { DialogueCondition.DC_MIXED_GENDER_GROUP, null },
                { DialogueCondition.DC_TOWN_LEVEL_CURRENT_LOCATION, null },
                { DialogueCondition.DC_PLAYERS_BEST_TOWN_LEVEL, null },
                { DialogueCondition.DC_IN_A_PLAYER_TOWN, null },
                { DialogueCondition.DC_TARGET_CHARACTER_EXISTS, null },
                { DialogueCondition.DC_IS_RECRUITABLE, null },
                { DialogueCondition.DC_HAS_AI_CONTRACT, null },
                { DialogueCondition.DC_HAS_ROBOT_LIMBS, new BooleanDescription("{0} has robotic limbs attached", "{0} does not have robotic limbs attached") },
                { DialogueCondition.DC_END, null },
            };

            public IConditionDescription? this[DialogueCondition conditionName] => map[conditionName];
        }

        private sealed class TaggedBooleanDescription : BooleanDescription
        {
            private readonly Type enumType;

            public TaggedBooleanDescription(string trueDescription, string falseDescription, Type enumType)
                : base(trueDescription, falseDescription)
            {
                this.enumType = enumType;
            }

            public override string GetDescription(string speakers, int value, char compareOperator, object? tag)
            {
                return base.GetDescription(speakers, value, compareOperator, Enum.ToObject(this.enumType, tag));
            }
        }

        private class BooleanDescription : IConditionDescription
        {
            private readonly string trueDescription;
            private readonly string falseDescription;

            public BooleanDescription(string trueDescription, string falseDescription)
            {
                this.trueDescription = trueDescription;
                this.falseDescription = falseDescription;
            }

            public virtual string GetDescription(string speakers, int value, char compareOperator, object? tag)
            {
                var template =  GenerateBooleanDescription(value, compareOperator);

                return string.Format(template, speakers, tag);
            }

            protected string GenerateBooleanDescription(int value, char compareOperator)
            {
                var convertedValue = Convert.ToBoolean(value);

                if (compareOperator != '=')
                {
                    throw new InvalidOperationException("BooleanDescription cannot work with other operator than '='");
                }

                return convertedValue ? this.trueDescription : this.falseDescription;
            }
        }

        private class ConditionDescription : IConditionDescription
        {
            private readonly string equalDescription;
            private readonly string lessThanDescription;
            private readonly string moreThanDescription;

            public ConditionDescription(string equalDescription, string lessThanDescription, string moreThanDescription)
            {
                this.equalDescription = equalDescription;
                this.lessThanDescription = lessThanDescription;
                this.moreThanDescription = moreThanDescription;
            }

            public string GetDescription(string speakers, int value, char compareOperator, object? tag)
            {
                var description = compareOperator switch
                {
                    '=' => this.equalDescription,
                    '<' => this.lessThanDescription,
                    '>' => this.moreThanDescription,
                    _ => throw new ArgumentException("Invalid compare operator"),
                };

                return string.Format(description, speakers, tag, value);
            }
        }

        private interface IConditionDescription
        {
            public string GetDescription(string speakers, int value, char compareOperator, object? tag);
        }
    }
}
