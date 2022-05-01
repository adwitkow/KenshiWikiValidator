using KenshiWikiValidator.OcsProxy.DialogueComponents;

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
            var text = line.Properties["text0"].ToString()!;

            return new DialogueNode()
            {
                Level = level,
                Line = text,
                Speakers = speakers,
                Conditions = this.ConvertConditions(line.Conditions, speakerMap),
            };
        }

        private IEnumerable<string> ConvertConditions(IEnumerable<DialogueCondition> conditions, Dictionary<DialogueSpeaker, IEnumerable<string>> speakerMap)
        {
            var results = new List<string>();

            foreach (var condition in conditions)
            {
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

                var result = $"If {validSpeakers}: {this.conditionMap[condition.ConditionName]}";

                results.Add(result);
            }

            return results;
        }

        private class ConditionMap
        {
            private static readonly Dictionary<DialogueCondition, string> map = new()
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
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
                { DialogueCondition.DC_IS_IMPRISONED, "is imprisoned" },
                { DialogueCondition.DC_IMPRISONMENT_IS_DEATHROW, null },
                { DialogueCondition.DC_TARGET_IN_TALKING_RANGE, "is nearby" },
                { DialogueCondition.DC_IN_MY_BUILDING, null },
                { DialogueCondition.DC_TARGET_LAST_SEEN_X_HOURS_AGO, null },
                { DialogueCondition.DC_IS_LEADER, null },
                { DialogueCondition.DC_MET_TARGET_BEFORE, null },
                { DialogueCondition.DC_WEAKER_THAN_ME, null },
                { DialogueCondition.DC_STRONGER_THAN_ME, null },
                { DialogueCondition.DC_HAS_TAG, null },
                { DialogueCondition.DC_IS_ALLY, null },
                { DialogueCondition.DC_IS_ENEMY, null },
                { DialogueCondition.DC_PERSONALITY_TAG, "has specified personality" },
                { DialogueCondition.DC_BROKEN_LEG, null },
                { DialogueCondition.DC_BROKEN_ARM, null },
                { DialogueCondition.DC_DAMAGED_HEAD, null },
                { DialogueCondition.DC_NEARLY_KO, null },
                { DialogueCondition.DC_IN_A_NON_PLAYER_TOWN, "is in a town (not including towns owned by the player)" },
                { DialogueCondition.DC_IS_RUNNING, null },
                { DialogueCondition.DC_COPS_AROUND, null },
                { DialogueCondition.NULL_NULL_____DC_TARGET_SQUAD_SIZE, null },
                { DialogueCondition.DC_SQUAD_SIZE, null },
                { DialogueCondition.DC_IS_PLAYER, null },
                { DialogueCondition.DC_NUM_BACKPACKS, null },
                { DialogueCondition.DC_SQUAD_ONLY_ANIMALS, null },
                { DialogueCondition.DC_IS_OUTNUMBERED, null },
                { DialogueCondition.DC_BOUNTY_AMOUNT_PERCEIVED, null },
                { DialogueCondition.DC_IS_KO, "is KO" },
                { DialogueCondition.DC_IS_NEARLY_KO, null },
                { DialogueCondition.DC_SQUAD_IS_DOWN, null },
                { DialogueCondition.DC_IS_DEAD, null },
                { DialogueCondition.DC_IS_FEMALE, "is female" },
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
                { DialogueCondition.DC_IS_IN_LOCKED_CAGE, "is in locked cage" },
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
                { DialogueCondition.DC_HAS_ROBOT_LIMBS, "has robotic limbs attached" },
                { DialogueCondition.DC_END, null },
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            };

            public string this[DialogueCondition conditionName]
            {
                get
                {
                    var result = map[conditionName];

                    if (result == null)
                    {
                        result = "Unknown condition";
                    }

                    return result;
                }
            }
        }
    }
}
