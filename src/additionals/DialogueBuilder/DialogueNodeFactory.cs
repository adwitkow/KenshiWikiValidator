using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;

namespace DialogueDumper
{
    public class DialogueNodeFactory
    {
        private readonly ConditionMap conditionMap;

        public DialogueNodeFactory()
        {
            this.conditionMap = new ConditionMap();
        }

        public DialogueNode Create(DialogueLine line, int level, IEnumerable<string> speakers, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap)
        {
            var text = line.Text0;

            return new DialogueNode()
            {
                Level = level,
                Line = text,
                Speakers = speakers,
                Conditions = this.ConvertConditions(line.Conditions, speakerMap),
            };
        }

        private IEnumerable<string> ConvertConditions(IEnumerable<ItemReference<DialogAction>> conditionReferences, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap)
        {
            var results = new List<string>();

            foreach (var conditionRef in conditionReferences)
            {
                var condition = conditionRef.Item;
                var conditionValue = conditionRef.Value0;

                var speakers = speakerMap[condition.Who];

                string validSpeakers;
                if (speakers.Count() > 1)
                {
                    validSpeakers = string.Join(", ", speakers.SkipLast(1)) + " or " + speakers.TakeLast(1).Single();
                }
                else
                {
                    validSpeakers = speakers.Single();
                }

                var conditionDescription = this.conditionMap[condition.ConditionName, conditionValue, condition.CompareBy, condition.Tag];
                var result = $"{validSpeakers} {conditionDescription}";

                results.Add(result);
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
                { DialogueCondition.DC_IS_IMPRISONED, new BooleanDescription("is imprisoned", "is not imprisoned") },
                { DialogueCondition.DC_IMPRISONMENT_IS_DEATHROW, null },
                { DialogueCondition.DC_TARGET_IN_TALKING_RANGE, new BooleanDescription("is nearby", "is not nearby") },
                { DialogueCondition.DC_IN_MY_BUILDING, null },
                { DialogueCondition.DC_TARGET_LAST_SEEN_X_HOURS_AGO, null },
                { DialogueCondition.DC_IS_LEADER, null },
                { DialogueCondition.DC_MET_TARGET_BEFORE, null },
                { DialogueCondition.DC_WEAKER_THAN_ME, null },
                { DialogueCondition.DC_STRONGER_THAN_ME, null },
                { DialogueCondition.DC_HAS_TAG, null },
                { DialogueCondition.DC_IS_ALLY, null },
                { DialogueCondition.DC_IS_ENEMY, null },
                { DialogueCondition.DC_PERSONALITY_TAG, new TaggedBooleanDescription("has {0} personality", "does not have {0} personality", typeof(PersonalityTag))},
                { DialogueCondition.DC_BROKEN_LEG, null },
                { DialogueCondition.DC_BROKEN_ARM, null },
                { DialogueCondition.DC_DAMAGED_HEAD, null },
                { DialogueCondition.DC_NEARLY_KO, null },
                { DialogueCondition.DC_IN_A_NON_PLAYER_TOWN, new BooleanDescription("is in a town (not including towns owned by the player)", "is not in a town (not including towns owned by the player)") },
                { DialogueCondition.DC_IS_RUNNING, null },
                { DialogueCondition.DC_COPS_AROUND, null },
                { DialogueCondition.NULL_NULL_____DC_TARGET_SQUAD_SIZE, null },
                { DialogueCondition.DC_SQUAD_SIZE, null },
                { DialogueCondition.DC_IS_PLAYER, null },
                { DialogueCondition.DC_NUM_BACKPACKS, null },
                { DialogueCondition.DC_SQUAD_ONLY_ANIMALS, null },
                { DialogueCondition.DC_IS_OUTNUMBERED, null },
                { DialogueCondition.DC_BOUNTY_AMOUNT_PERCEIVED, null },
                { DialogueCondition.DC_IS_KO, new BooleanDescription("is unconscious", "is not unconscious") },
                { DialogueCondition.DC_IS_NEARLY_KO, null },
                { DialogueCondition.DC_SQUAD_IS_DOWN, null },
                { DialogueCondition.DC_IS_DEAD, null },
                { DialogueCondition.DC_IS_FEMALE, new BooleanDescription("is female", "is male") },
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
                { DialogueCondition.DC_IS_IN_LOCKED_CAGE, new BooleanDescription("is locked in a cage", "is not locked in a cage") },
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
                { DialogueCondition.DC_HAS_ROBOT_LIMBS, new BooleanDescription("has robotic limbs attached", "does not have robotic limbs attached") },
                { DialogueCondition.DC_END, null },
            };

            public string this[DialogueCondition conditionName, int value, char compareOperator, int? tag]
            {
                get
                {
                    var description = map[conditionName];

                    if (description is null)
                    {
                        return $"CONDITION DESCRIPTION '{conditionName}' NOT SET";
                    }

                    var result = description.GetDescription(value, compareOperator, tag);

                    return result;
                }
            }
        }

        private sealed class TaggedBooleanDescription : BooleanDescription
        {
            private readonly Type enumType;

            public TaggedBooleanDescription(string trueDescription, string falseDescription, Type enumType)
                : base(trueDescription, falseDescription)
            {
                this.enumType = enumType;
            }

            public override string GetDescription(int value, char compareOperator, int? tag)
            {
                var template = GenerateBooleanDescription(value, compareOperator);

                if (tag is null)
                {
                    return template;
                }

                string result;
                if (enumType is null)
                {
                    result = string.Format(template, tag);
                }
                else
                {
                    result = string.Format(template, Enum.ToObject(enumType, tag));
                }
                return result;
            }
        }

        private class BooleanDescription : IConditionDescription
        {
            protected readonly string trueDescription;
            protected readonly string falseDescription;

            public BooleanDescription(string trueDescription, string falseDescription)
            {
                this.trueDescription = trueDescription;
                this.falseDescription = falseDescription;
            }

            public virtual string GetDescription(int value, char compareOperator, int? tag)
            {
                return GenerateBooleanDescription(value, compareOperator);
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

        private interface IConditionDescription
        {
            public string GetDescription(int value, char compareOperator, int? tag);
        }
    }
}
