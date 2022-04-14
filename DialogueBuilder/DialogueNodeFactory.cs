using KenshiWikiValidator.Features.CharacterValidation.CharacterDialogue;

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
            private static Dictionary<DialogueConditionName, string> map = new Dictionary<DialogueConditionName, string>()
            {
                { DialogueConditionName.DC_NONE, null },
                { DialogueConditionName.DC_RELATIONS, null },
                { DialogueConditionName.DC_PLAYERMONEY, null },
                { DialogueConditionName.DC_REPUTATION, null },
                { DialogueConditionName.DC_CARRYING_BOUNTY_ALIVE, null },
                { DialogueConditionName.DC_CARRYING_BOUNTY_DEAD, null },
                { DialogueConditionName.DC_FACTION_VARIABLE, null },
                { DialogueConditionName.DC_IMPRISONED_BY_TARGET, null },
                { DialogueConditionName.DC_IMPRISONED_BY_OTHER, null },
                { DialogueConditionName.DC_IS_A_TRADER, null },
                { DialogueConditionName.DC_FACTION_RANK, null },
                { DialogueConditionName.DC_BUILDING_IS_CLOSED_AND_SECURED, null },
                { DialogueConditionName.DC_PLAYER_TECH_LEVEL, null },
                { DialogueConditionName.DC_NUM_DIALOG_EVENT_REPEATS, null },
                { DialogueConditionName.DC_IS_IMPRISONED, "is imprisoned" },
                { DialogueConditionName.DC_IMPRISONMENT_IS_DEATHROW, null },
                { DialogueConditionName.DC_TARGET_IN_TALKING_RANGE, "is nearby" },
                { DialogueConditionName.DC_IN_MY_BUILDING, null },
                { DialogueConditionName.DC_TARGET_LAST_SEEN_X_HOURS_AGO, null },
                { DialogueConditionName.DC_IS_LEADER, null },
                { DialogueConditionName.DC_MET_TARGET_BEFORE, null },
                { DialogueConditionName.DC_WEAKER_THAN_ME, null },
                { DialogueConditionName.DC_STRONGER_THAN_ME, null },
                { DialogueConditionName.DC_HAS_TAG, null },
                { DialogueConditionName.DC_IS_ALLY, null },
                { DialogueConditionName.DC_IS_ENEMY, null },
                { DialogueConditionName.DC_PERSONALITY_TAG, "has specified personality" },
                { DialogueConditionName.DC_BROKEN_LEG, null },
                { DialogueConditionName.DC_BROKEN_ARM, null },
                { DialogueConditionName.DC_DAMAGED_HEAD, null },
                { DialogueConditionName.DC_NEARLY_KO, null },
                { DialogueConditionName.DC_IN_A_NON_PLAYER_TOWN, "is in a town (not including towns owned by the player)" },
                { DialogueConditionName.DC_IS_RUNNING, null },
                { DialogueConditionName.DC_COPS_AROUND, null },
                { DialogueConditionName.NULL_NULL_____DC_TARGET_SQUAD_SIZE, null },
                { DialogueConditionName.DC_SQUAD_SIZE, null },
                { DialogueConditionName.DC_IS_PLAYER, null },
                { DialogueConditionName.DC_NUM_BACKPACKS, null },
                { DialogueConditionName.DC_SQUAD_ONLY_ANIMALS, null },
                { DialogueConditionName.DC_IS_OUTNUMBERED, null },
                { DialogueConditionName.DC_BOUNTY_AMOUNT_PERCEIVED, null },
                { DialogueConditionName.DC_IS_KO, "is KO" },
                { DialogueConditionName.DC_IS_NEARLY_KO, null },
                { DialogueConditionName.DC_SQUAD_IS_DOWN, null },
                { DialogueConditionName.DC_IS_DEAD, null },
                { DialogueConditionName.DC_IS_FEMALE, "is female" },
                { DialogueConditionName.DC_CARRYING_SOMEONE_TO_ENSLAVE, null },
                { DialogueConditionName.DC_BOUNTY_AMOUNT_ACTUAL, null },
                { DialogueConditionName.DC_IM_UNARMED, null },
                { DialogueConditionName.DC_TOWN_HAS_FORTIFICATIONS_WALLS, null },
                { DialogueConditionName.DC_TARGET_IS_MY_MISSION_TARGET, null },
                { DialogueConditionName.DC_MY_MISSION_IS_FRIENDLY, null },
                { DialogueConditionName.DC_I_LOVE_THIS_GUY, null },
                { DialogueConditionName.DC_I_HATE_THIS_GUY, null },
                { DialogueConditionName.DC_I_SHOULD_SCREW_THIS_GUY_OVER, null },
                { DialogueConditionName.DC_I_SHOULD_HELP_THIS_GUY, null },
                { DialogueConditionName.DC_IN_COMBAT, null },
                { DialogueConditionName.DC_WITHIN_TOWN_WALLS, null },
                { DialogueConditionName.DC_TOWN_WALLS_LOCKED_UP, null },
                { DialogueConditionName.DC_IS_SLAVE, null },
                { DialogueConditionName.DC_HAS_A_BASE_NEARBY, null },
                { DialogueConditionName.DC_TARGET_IS_SLAVE_OF_MY_FACTION, null },
                { DialogueConditionName.DC_IS_ESCAPED_SLAVE, null },
                { DialogueConditionName.DC_IS_IN_LOCKED_CAGE, "is in locked cage" },
                { DialogueConditionName.DC_WEARING_LOCKED_SHACKLES, null },
                { DialogueConditionName.DC_IS_SAME_RACE_AS_ME, null },
                { DialogueConditionName.DC_CAN_AFFORD_BOUNTY, null },
                { DialogueConditionName.DC_IS_SNEAKING, null },
                { DialogueConditionName.DC_IS_INDOORS, null },
                { DialogueConditionName.DC_HAS_ILLEGAL_ITEM, null },
                { DialogueConditionName.DC_USING_MY_TRAINING_EQUIPMENT, null },
                { DialogueConditionName.DC_STARVING, null },
                { DialogueConditionName.DC_MIXED_GENDER_GROUP, null },
                { DialogueConditionName.DC_TOWN_LEVEL_CURRENT_LOCATION, null },
                { DialogueConditionName.DC_PLAYERS_BEST_TOWN_LEVEL, null },
                { DialogueConditionName.DC_IN_A_PLAYER_TOWN, null },
                { DialogueConditionName.DC_TARGET_CHARACTER_EXISTS, null },
                { DialogueConditionName.DC_IS_RECRUITABLE, null },
                { DialogueConditionName.DC_HAS_AI_CONTRACT, null },
                { DialogueConditionName.DC_HAS_ROBOT_LIMBS, "has robotic limbs attached" },
                { DialogueConditionName.DC_END, null },
            };

            public string this[DialogueConditionName conditionName]
            {
                get
                {
                    var result = map[conditionName];

                    if (result == null)
                    {
                        throw new ArgumentException();
                    }

                    return result;
                }
            }
        }
    }
}
