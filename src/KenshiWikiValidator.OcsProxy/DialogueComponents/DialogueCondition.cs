﻿// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
// Copyright (C) 2021  Adam Witkowski <https://github.com/adwitkow/>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

namespace KenshiWikiValidator.OcsProxy.DialogueComponents
{
    public enum DialogueCondition
    {
        DC_NONE,
        DC_RELATIONS,
        DC_PLAYERMONEY,
        DC_REPUTATION,
        DC_CARRYING_BOUNTY_ALIVE,
        DC_CARRYING_BOUNTY_DEAD,
        DC_FACTION_VARIABLE,
        DC_IMPRISONED_BY_TARGET,
        DC_IMPRISONED_BY_OTHER,
        DC_IS_A_TRADER,
        DC_FACTION_RANK,
        DC_BUILDING_IS_CLOSED_AND_SECURED,
        DC_PLAYER_TECH_LEVEL,
        DC_NUM_DIALOG_EVENT_REPEATS,
        DC_IS_IMPRISONED,
        DC_IMPRISONMENT_IS_DEATHROW,
        DC_TARGET_IN_TALKING_RANGE,
        DC_IN_MY_BUILDING,
        DC_TARGET_LAST_SEEN_X_HOURS_AGO,
        DC_IS_LEADER,
        DC_MET_TARGET_BEFORE,
        DC_WEAKER_THAN_ME,
        DC_STRONGER_THAN_ME,
        DC_HAS_TAG,
        DC_IS_ALLY,
        DC_IS_ENEMY,
        DC_PERSONALITY_TAG,
        DC_BROKEN_LEG,
        DC_BROKEN_ARM,
        DC_DAMAGED_HEAD,
        DC_NEARLY_KO,
        DC_IN_A_NON_PLAYER_TOWN,
        DC_IS_RUNNING,
        DC_COPS_AROUND,
        NULL_NULL_____DC_TARGET_SQUAD_SIZE,
        DC_SQUAD_SIZE,
        DC_IS_PLAYER,
        DC_NUM_BACKPACKS,
        DC_SQUAD_ONLY_ANIMALS,
        DC_IS_OUTNUMBERED,
        DC_BOUNTY_AMOUNT_PERCEIVED,
        DC_IS_KO,
        DC_IS_NEARLY_KO,
        DC_SQUAD_IS_DOWN,
        DC_IS_DEAD,
        DC_IS_FEMALE,
        DC_CARRYING_SOMEONE_TO_ENSLAVE,
        DC_BOUNTY_AMOUNT_ACTUAL,
        DC_IM_UNARMED,
        DC_TOWN_HAS_FORTIFICATIONS_WALLS,
        DC_TARGET_IS_MY_MISSION_TARGET,
        DC_MY_MISSION_IS_FRIENDLY,
        DC_I_LOVE_THIS_GUY,
        DC_I_HATE_THIS_GUY,
        DC_I_SHOULD_SCREW_THIS_GUY_OVER,
        DC_I_SHOULD_HELP_THIS_GUY,
        DC_IN_COMBAT,
        DC_WITHIN_TOWN_WALLS,
        DC_TOWN_WALLS_LOCKED_UP,
        DC_IS_SLAVE,
        DC_HAS_A_BASE_NEARBY,
        DC_TARGET_IS_SLAVE_OF_MY_FACTION,
        DC_IS_ESCAPED_SLAVE,
        DC_IS_IN_LOCKED_CAGE,
        DC_WEARING_LOCKED_SHACKLES,
        DC_IS_SAME_RACE_AS_ME,
        DC_CAN_AFFORD_BOUNTY,
        DC_IS_SNEAKING,
        DC_IS_INDOORS,
        DC_HAS_ILLEGAL_ITEM,
        DC_USING_MY_TRAINING_EQUIPMENT,
        DC_STARVING,
        DC_MIXED_GENDER_GROUP,
        DC_TOWN_LEVEL_CURRENT_LOCATION,
        DC_PLAYERS_BEST_TOWN_LEVEL,
        DC_IN_A_PLAYER_TOWN,
        DC_TARGET_CHARACTER_EXISTS,
        DC_IS_RECRUITABLE,
        DC_HAS_AI_CONTRACT,
        DC_HAS_ROBOT_LIMBS,
        DC_END,
    }
}
