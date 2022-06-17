using System.Collections.Generic;
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
            var worldState = SetupCharacterWorldState(TrueValue, AliveIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsAliveNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterWorldState(FalseValue, AliveIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is killed or imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsKilled()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterWorldState(TrueValue, KilledIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is killed";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsKilledNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterWorldState(FalseValue, KilledIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is alive or imprisoned"; // doesn't really make sense
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsImprisoned()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterWorldState(TrueValue, ImprisonedIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsImprisonedNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterWorldState(FalseValue, ImprisonedIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is killed or alive"; // doesn't make sense at all
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereTwoNpcsAreAlive()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterWorldState(TrueValue, AliveIdentifier, 2);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] and [[character1]] are alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereThreeNpcsAreAliveNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterWorldState(FalseValue, AliveIdentifier, 3);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]], [[character1]] and [[character2]] are killed or imprisoned";
            Assert.AreEqual(expected, result);
        }

        private ItemReference<WorldEventState> SetupCharacterWorldState(int worldStateValue, int subjectStateValue, int characterCount)
        {
            var worldState = new WorldEventState("worldstateid", "name");

            var characters = new List<ItemReference<Character>>();
            for (var i = 0; i < characterCount; i++)
            {
                var character = new Character($"characterid{i}", $"character{i}");
                characters.Add(new ItemReference<Character>(character, subjectStateValue, 0, 0));
            }

            worldState.NpcIs = characters;
            return new ItemReference<WorldEventState>(worldState, worldStateValue, 0, 0);
        }
    }
}
