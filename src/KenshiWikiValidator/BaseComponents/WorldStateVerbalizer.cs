using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;

namespace KenshiWikiValidator.BaseComponents
{
    public class WorldStateVerbalizer
    {
        private static readonly string[] States = new[] { "killed", "alive", "imprisoned" };

        //public IEnumerable<string> Verbalize(IEnumerable<ItemReference<WorldEventState>> worldStateReferences)
        //{
        //    var mappedWorldStates = worldStateReferences
        //        .ToDictionary(
        //            reference => reference.Item,
        //            reference => Convert.ToBoolean(reference.Value0));

        //    foreach (var mappedWorldState in mappedWorldStates)
        //    {
        //        return CreateVerbalization()
        //    }
        //}

        public string Verbalize(ItemReference<WorldEventState> worldStateReference)
        {
            var worldState = worldStateReference.Item;
            var isNegated = worldStateReference.Value0 == 0;

            var npcIsItems = worldState.NpcIs
                .ToDictionary(
                reference => reference.Item,
                reference => reference.Value0);
            var npcIsNotItems = worldState.NpcIsNot
                .ToDictionary(
                    reference => reference.Item,
                    reference => reference.Value0);
            var playerAllyItems = worldState.PlayerAlly
                .ToDictionary(
                    reference => reference.Item,
                    reference => Convert.ToBoolean(reference.Value0));

            foreach (var itemPair in npcIsItems)
            {
                var npc = itemPair.Key;
                var npcState = itemPair.Value;

                if (isNegated)
                {
                    var validStates = States.ToList();
                    validStates.RemoveAt(npcState);

                    return $"[[{npc.Name}]] is {string.Join(" or ", validStates)}";
                }
                else
                {
                    return $"[[{npc.Name}]] is {States[npcState]}";
                }
            }

            throw new InvalidOperationException("World states with no details cannot be verbalized.");
        }
    }
}
