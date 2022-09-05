using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;

namespace DialogueDumper
{
    internal class DialogueComponentConverter
    {
        private readonly ConditionMap conditionMap;
        private readonly EffectMap effectMap;

        public DialogueComponentConverter()
        {
            this.conditionMap = new ConditionMap();
            this.effectMap = new EffectMap();
        }

        public IConditionDescription? ConvertCondition(DialogAction action)
        {
            return this.conditionMap[action.ConditionName];
        }

        public IEffectDescription? ConvertEffect(DialogAction action)
        {
            return this.effectMap[action.ActionName];
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

        private sealed class EffectMap
        {
            private static readonly Dictionary<DialogueEffect, IEffectDescription?> map = new()
            {
                { DialogueEffect.DA_NONE, null },
                { DialogueEffect.DA_TRADE, null },
                { DialogueEffect.DA_TALK_TO_LEADER, null },
                { DialogueEffect.DA_JOIN_SQUAD_WITH_EDIT, new JoinSquadDescription("{0} joins the squad. Character creator screen opens.") },
                { DialogueEffect.DA_AFFECT_RELATIONS, null },
                { DialogueEffect.DA_AFFECT_REPUTATION, null },
                { DialogueEffect.DA_ATTACK_CHASE_FOREVER, null },
                { DialogueEffect.DA_GO_HOME, null },
                { DialogueEffect.DA_TAKE_MONEY, null },
                { DialogueEffect.DA_GIVE_MONEY, null },
                { DialogueEffect.DA_PAY_BOUNTY, null },
                { DialogueEffect.DA_CHARACTER_EDITOR, null },
                { DialogueEffect.DA_FORCE_SPEECH_TIMER, null },
                { DialogueEffect.DA_DECLARE_WAR, null },
                { DialogueEffect.DA_END_WAR, null },
                { DialogueEffect.DA_CLEAR_AI, null },
                { DialogueEffect.DA_FOLLOW_WHILE_TALKING, null },
                { DialogueEffect.DA_THUG_HUNTER, null },
                { DialogueEffect.DA_JOIN_SQUAD_FAST, new JoinSquadDescription("{0} joins the squad, omitting the character creator screen") },
                { DialogueEffect.DA_REMEMBER_CHARACTER, null },
                { DialogueEffect.DA_FLAG_TEMP_ALLY, null },
                { DialogueEffect.DA_FLAG_TEMP_ENEMY, null },
                { DialogueEffect.DA_MATES_KILL_ME, null },
                { DialogueEffect.DA_MAKE_TARGET_RUN_FASTER, null },
                { DialogueEffect.DA_GIVE_TARGET_MY_SLAVES, null },
                { DialogueEffect.DA_TAG_ESCAPED_SLAVE, null },
                { DialogueEffect.DA_FREE_TARGET_SLAVE, null },
                { DialogueEffect.DA_MERGE_WITH_SIMILAR_SQUADS, null },
                { DialogueEffect.DA_SEPARATE_TO_MY_OWN_SQUAD, null },
                { DialogueEffect.DA_ARREST_TARGET, null },
                { DialogueEffect.DA_ARREST_TARGETS_CARRIED_PERSON, null },
                { DialogueEffect.DA_ATTACK_TOWN, null },
                { DialogueEffect.DA_ASSIGN_BOUNTY, null },
                { DialogueEffect.DA_CRIME_ALARM, null },
                { DialogueEffect.DA_RUN_AWAY, null },
                { DialogueEffect.DA_INCREASE_FACTION_RANK, null },
                { DialogueEffect.DA_LOCK_THIS_DIALOG, null },
                { DialogueEffect.DA_ASSAULT_PHASE, null },
                { DialogueEffect.DA_RETREAT_PHASE, null },
                { DialogueEffect.DA_VICTORY_PHASE, null },
                { DialogueEffect.DA_ENSLAVE_TARGETS_CARRIED_PERSON, null },
                { DialogueEffect.CHOOSE_SLAVES_SELLING, null },
                { DialogueEffect.CHOOSE_SLAVES_BUYING, null },
                { DialogueEffect.CHOOSE_PRISONER_BAIL, null },
                { DialogueEffect.CHOOSE_CONSCRIPTION, null },
                { DialogueEffect.CHOOSE_RECRUITING, null },
                { DialogueEffect.CHOOSE_HIRING_CONTRACT, null },
                { DialogueEffect.SURRENDER_NON_HUMANS, null },
                { DialogueEffect.CHOOSE_ANIMALS_BUYING, null },
                { DialogueEffect.DA_CLEAR_BOUNTY, null },
                { DialogueEffect.DA_PLAYER_SELL_PRISONERS, null },
                { DialogueEffect.DA_PLAYER_SURRENDER_MEMBER_DIFFERENT_RACE, null },
                { DialogueEffect.DA_SUMMON_MY_SQUAD, null },
                { DialogueEffect.DA_REMOVE_SLAVE_STATUS, null },
                { DialogueEffect.DA_OPEN_NEAREST_GATE, null },
                { DialogueEffect.DA_ATTACK_STAY_NEAR_HOME, null },
                { DialogueEffect.DA_MASSIVE_ALARM, null },
                { DialogueEffect.DA_ATTACK_IF_NO_COEXIST, null },
                { DialogueEffect.DA_KNOCKOUT, null },
                { DialogueEffect.DA_END, null },
            };

            public IEffectDescription? this[DialogueEffect effectName] => map[effectName];
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
                var template = GenerateBooleanDescription(value, compareOperator);

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

        private class EffectDescription : IEffectDescription
        {
            private readonly string description;

            public EffectDescription(string description)
            {
                this.description = description;
            }

            public virtual string GetDescription(Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, DialogueSpeaker speaker, int? value, IEnumerable<DialogueEvent> _)
            {
                return string.Format(this.description, speakersMap[speaker].ToCommaSeparatedListOr(), value);
            }
        }

        private class JoinSquadDescription : EffectDescription
        {
            public JoinSquadDescription(string description)
                : base(description)
            {
            }

            public override string GetDescription(Dictionary<DialogueSpeaker, IEnumerable<string>> speakersMap, DialogueSpeaker speaker, int? value, IEnumerable<DialogueEvent> dialogueEvents)
            {
                if (dialogueEvents.Count() == 1 && dialogueEvents.Single() == DialogueEvent.EV_PLAYER_TALK_TO_ME)
                {
                    speaker = DialogueSpeaker.Me;
                }

                return base.GetDescription(speakersMap, speaker, value, dialogueEvents);
            }
        }
    }
}
