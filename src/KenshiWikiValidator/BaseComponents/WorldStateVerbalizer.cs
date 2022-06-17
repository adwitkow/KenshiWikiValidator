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

namespace KenshiWikiValidator.BaseComponents
{
    public class WorldStateVerbalizer
    {
        private static readonly string[] PossibleCharacterStates = new[] { "killed", "alive", "imprisoned" };

        public string Verbalize(ItemReference<WorldEventState> worldStateReference)
        {
            var worldState = worldStateReference.Item;
            var isNegated = worldStateReference.Value0 == 0;

            var npcIsComponents = CreateComponents(worldState.NpcIs, "is", isNegated);
            var npcIsNotComponents = CreateComponents(worldState.NpcIsNot, "is not", isNegated);

            var playerAllyComponents = CreateComponents(worldState.PlayerAlly, "is", isNegated, "allied to the player");
            var playerEnemyComponents = CreateComponents(worldState.PlayerEnemy, "is", isNegated, "an enemy of the player");

            return OxbridgeAnd(npcIsComponents.Select(comp => JoinSentence(comp))
                .Concat(npcIsNotComponents.Select(comp => JoinSentence(comp)))
                .Concat(playerAllyComponents.Select(comp => JoinSentence(comp)))
                .Concat(playerEnemyComponents.Select(comp => JoinSentence(comp))));
        }

        private static IEnumerable<WorldStateSentence> CreateComponents<T>(
            IEnumerable<ItemReference<T>> characterReferences,
            string predicate,
            bool isNegated,
            string baseState = "")
            where T : IItem
        {
            var itemsWithStates = characterReferences
                .ToDictionary(
                    reference => reference.Item,
                    reference => reference.Value0);

            var sentenceMap = new Dictionary<string, WorldStateSentence>();
            foreach (var itemPair in itemsWithStates)
            {
                var item = itemPair.Key;
                var itemState = itemPair.Value;

                string convertedState;
                if (item is Character)
                {
                    convertedState = ConvertCharacterState(isNegated, itemState);
                }
                else
                {
                    convertedState = ConvertFactionState(isNegated, itemState, baseState);
                }

                if (sentenceMap.TryGetValue(predicate + convertedState, out var sentence))
                {
                    sentence.Subjects.Add(item.Name);
                }
                else
                {
                    var newSentence = new WorldStateSentence(item.Name, predicate, convertedState);
                    sentenceMap.Add(predicate + convertedState, newSentence);
                }
            }

            foreach (var pair in sentenceMap)
            {
                var sentence = pair.Value;

                if (sentence.Subjects.Count > 1)
                {
                    sentence.Predicate = sentence.Predicate.Replace("is", "are");
                }
            }

            return sentenceMap.Values;
        }

        private static string ConvertFactionState(bool isNegated, int factionState, string baseState)
        {
            var state = factionState;
            if (isNegated)
            {
                state = Math.Abs(state - 1);
            }

            string output;
            if (state == 0)
            {
                output = "not " + baseState;
            }
            else
            {
                output = baseState;
            }

            return output;
        }

        private static string ConvertCharacterState(bool isNegated, int characterState)
        {
            if (isNegated)
            {
                var validStates = PossibleCharacterStates.ToList();
                validStates.RemoveAt(characterState);

                return string.Join(" or ", validStates);
            }
            else
            {
                return PossibleCharacterStates[characterState];
            }
        }

        private static string JoinSentence(WorldStateSentence sentence)
        {
            return $"{OxbridgeAnd(sentence.Subjects.Select(s => $"[[{s}]]"))} {sentence.Predicate} {sentence.State}";
        }

        private static string OxbridgeAnd(IEnumerable<string> collection)
        {
            // Modified version of https://stackoverflow.com/a/23151256
            if (!collection.Any())
            {
                return string.Empty;
            }

            if (collection.Count() == 1)
            {
                return collection.First();
            }

            var delimited = string.Join(", ", collection.Take(collection.Count() - 1));
            return string.Concat(delimited, " and ", collection.LastOrDefault());
        }

        private sealed class WorldStateSentence
        {
            public WorldStateSentence(string firstSubject, string predicate, string state)
            {
                this.Subjects = new List<string>()
                {
                    firstSubject,
                };
                this.Predicate = predicate;
                this.State = state;
            }

            public ICollection<string> Subjects { get; set; }

            public string Predicate { get; set; }

            public string State { get; set; }
        }
    }
}
