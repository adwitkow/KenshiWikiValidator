using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Tests.BaseComponents
{
    [TestClass]
    public class WorldStateVerbalizerTests
    {
        private const int TrueValue = 1;
        private const int FalseValue = 0;
        private const int KilledIdentifier = 0;
        private const int AliveIdentifier = 1;
        private const int ImprisonedIdentifier = 2;

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsAlive()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = new WorldEventState("worldstateid", "name");

            var worldStateReference = new ItemReference<WorldEventState>(worldState, TrueValue, 0, 0);
            var character = new Character("characterid", "character");

            worldState.NpcIs = new[] { new ItemReference<Character>(character, AliveIdentifier, 0, 0) };

            var result = verbalizer.Verbalize(worldStateReference);

            var expected = $"[[{character.Name}]] is alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsAliveNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = new WorldEventState("worldstateid", "name");

            var worldStateReference = new ItemReference<WorldEventState>(worldState, FalseValue, 0, 0);
            var character = new Character("characterid", "character");

            worldState.NpcIs = new[] { new ItemReference<Character>(character, AliveIdentifier, 0, 0) };

            var result = verbalizer.Verbalize(worldStateReference);

            var expected = $"[[{character.Name}]] is killed or imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsKilled()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = new WorldEventState("worldstateid", "name");

            var worldStateReference = new ItemReference<WorldEventState>(worldState, TrueValue, 0, 0);
            var character = new Character("characterid", "character");

            worldState.NpcIs = new[] { new ItemReference<Character>(character, KilledIdentifier, 0, 0) };

            var result = verbalizer.Verbalize(worldStateReference);

            var expected = $"[[{character.Name}]] is killed";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsKilledNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = new WorldEventState("worldstateid", "name");

            var worldStateReference = new ItemReference<WorldEventState>(worldState, FalseValue, 0, 0);
            var character = new Character("characterid", "character");

            worldState.NpcIs = new[] { new ItemReference<Character>(character, KilledIdentifier, 0, 0) };

            var result = verbalizer.Verbalize(worldStateReference);

            var expected = $"[[{character.Name}]] is alive or imprisoned"; // doesn't really make sense
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsImprisoned()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = new WorldEventState("worldstateid", "name");

            var worldStateReference = new ItemReference<WorldEventState>(worldState, TrueValue, 0, 0);
            var character = new Character("characterid", "character");

            worldState.NpcIs = new[] { new ItemReference<Character>(character, ImprisonedIdentifier, 0, 0) };

            var result = verbalizer.Verbalize(worldStateReference);

            var expected = $"[[{character.Name}]] is imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsImprisonedNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = new WorldEventState("worldstateid", "name");

            var worldStateReference = new ItemReference<WorldEventState>(worldState, FalseValue, 0, 0);
            var character = new Character("characterid", "character");

            worldState.NpcIs = new[] { new ItemReference<Character>(character, ImprisonedIdentifier, 0, 0) };

            var result = verbalizer.Verbalize(worldStateReference);

            var expected = $"[[{character.Name}]] is killed or alive"; // doesn't make sense at all
            Assert.AreEqual(expected, result);
        }
    }
}
