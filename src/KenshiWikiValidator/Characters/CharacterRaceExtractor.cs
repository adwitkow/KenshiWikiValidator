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
        private readonly Race greenlanderRace;

        public CharacterRaceExtractor(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;

            this.greenlanderRace = this.itemRepository.GetItemByStringId<Race>(GreenlanderId);
        }

        public IEnumerable<Race> Extract(Character character)
        {
            var referringSquads = this.itemRepository.GetItems<Squad>()
                    .Where(squad => squad.ContainsCharacter(character));

            var races = new List<Race>();
            foreach (var squad in referringSquads)
            {
                var squadRaces = this.GetPossibleRacesForSquad(character, squad)
                    .Select(r => r.Item);
                races.AddRange(squadRaces);
            }

            return races
                .Distinct()
                .OrderBy(race => Array.IndexOf(racesOrdered, race.Name.ToLower().Trim()));
        }

        public IEnumerable<ItemReference<Race>> GetPossibleRacesForSquad(Character character, Squad squad)
        {
            var results = new List<ItemReference<Race>>();

            var overrides = squad.RaceOverrides;
            if (overrides.Any())
            {
                // races from squads override all
                results.AddRange(overrides);

                return results;
            }

            if (character.Races.Any())
            {
                // second priority is character race
                results.AddRange(character.Races);

                return results;
            }

            var squadFactionRaces = squad.Factions.SelectMany(faction => faction.Item.Races);

            if (squadFactionRaces.Any())
            {
                // then faction squads
                results.AddRange(squadFactionRaces);

                return results;
            }

            if (!squad.Factions.Any())
            {
                // and town squads
                var towns = this.itemRepository.GetItems<Town>()
                    .Where(town => town.ContainsSquad(squad));
                var townRaces = towns.SelectMany(town => town.Factions)
                    .SelectMany(faction => faction.Item.Races)
                    .Distinct();

                if (towns.Any() && townRaces.Any())
                {
                    results.AddRange(townRaces);

                    return results;
                }
            }

            // in case everything fails, the default is greenlander
            results.Add(new ItemReference<Race>(this.greenlanderRace));

            return results;
        }
    }
}
