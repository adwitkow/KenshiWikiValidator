// This file is part of KenshiWikiValidator project <https://github.com/adwitkow/KenshiWikiValidator>
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

using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.Characters
{
    public class CharacterRaceExtractor
    {
        private const string GreenlanderId = "17-gamedata.quack";

        private static string[] racesOrdered = ["greenlander", "scorchlander", "shek",
            "hive prince", "hive soldier drone", "hive worker drone",
            "hive prince south hive", "hive soldier drone south hive",
            "hive worker drone south hive", "hive queen", "deadhive prince",
            "deadhive soldier", "deadhive worker", "p4 unit", "skeleton p4mkii",
            "screamer mki", "skeleton mkii screamer", "skeleton",
            "skeleton log-head mkii", "skeleton no-head mkii", "soldierbot",
            "cannibal", "cannibal skav", "fishman", "alpha fishman"];

        private readonly IItemRepository itemRepository;

        public CharacterRaceExtractor(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IEnumerable<Race> Extract(Character character)
        {
            var raceReferences = character.Races;

            var referringSquads = this.itemRepository.GetItems<Squad>()
                    .Where(squad => squad.ContainsCharacter(character));
            var squadRaces = referringSquads.SelectMany(squad => squad.RaceOverrides);

            var towns = this.itemRepository.GetItems<Town>()
                .Where(town => town.BarSquads.Any(squad => squad.Item.ContainsCharacter(character))
                    || town.RoamingSquads.Any(squad => squad.Item.ContainsCharacter(character))
                    || town.Residents.Any(squad => squad.Item.ContainsCharacter(character)));

            var factions = referringSquads.SelectMany(squad => squad.Faction)
                .Concat(towns.SelectMany(town => town.Factions));
            var factionRaces = factions.SelectMany(faction => faction.Item.Races)
                .Distinct();

            if (squadRaces.Any())
            {
                raceReferences = squadRaces; // squads override all
            }
            else if (factionRaces.Any() && !raceReferences.Any())
            {
                raceReferences = factionRaces; // faction squads are last resort
            }

            var races = raceReferences.Select(race => race.Item).Distinct();

            if (!races.Any())
            {
                // greenlander is the default if no race is set at all
                var greenlander = this.itemRepository.GetItemByStringId<Race>(GreenlanderId);
                races = [greenlander];
            }

            return races.OrderBy(race => Array.IndexOf(racesOrdered, race.Name.ToLower().Trim()));
        }
    }
}
