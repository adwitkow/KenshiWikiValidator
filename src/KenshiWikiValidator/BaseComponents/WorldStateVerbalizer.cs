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

            var npcIsComponents = CreateCharacterComponents(worldState.NpcIs, "is", isNegated);
            //var npcIsNotComponents = CreateCharacterComponents(worldState.NpcIsNot, "is not", isNegated);
            //var playerAllyItems = worldState.PlayerAlly
            //    .ToDictionary(
            //        reference => reference.Item,
            //        reference => Convert.ToBoolean(reference.Value0));

            return OxbridgeAnd(npcIsComponents.Select(comp => JoinSentence(comp)));
        }

        private static IEnumerable<WorldStateSentence> CreateCharacterComponents(
            IEnumerable<ItemReference<Character>> characterReferences,
            string predicate,
            bool isNegated)
        {
            var itemsWithStates = characterReferences
                .ToDictionary(
                    reference => reference.Item,
                    reference => reference.Value0);

            var sentenceMap = new Dictionary<string, WorldStateSentence>();
            foreach (var itemPair in itemsWithStates)
            {
                var character = itemPair.Key;
                var characterState = itemPair.Value;

                string convertedState;
                if (isNegated)
                {
                    var validStates = PossibleCharacterStates.ToList();
                    validStates.RemoveAt(characterState);

                    convertedState = string.Join(" or ", validStates);
                }
                else
                {
                    convertedState = PossibleCharacterStates[characterState];
                }

                if (sentenceMap.TryGetValue(predicate + convertedState, out var sentence))
                {
                    sentence.Subjects.Add(character.Name);
                }
                else
                {
                    var newSentence = new WorldStateSentence(character.Name, predicate, convertedState);
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

        private static string JoinSentence(WorldStateSentence sentence)
        {
            return $"{OxbridgeAnd(sentence.Subjects.Select(s => $"[[{s}]]"))} {sentence.Predicate} {sentence.State}";
        }

        private static string OxbridgeAnd(IEnumerable<string> collection)
        {
            // Modified version of https://stackoverflow.com/a/23151256
            var output = string.Empty;

            if (collection == null)
            {
                return output;
            }

            var list = collection.ToList();

            if (!list.Any())
            {
                return output;
            }

            if (list.Count == 1)
            {
                return list.First();
            }

            var delimited = string.Join(", ", list.Take(list.Count - 1));

            output = string.Concat(delimited, " and ", list.LastOrDefault());

            return output;
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
