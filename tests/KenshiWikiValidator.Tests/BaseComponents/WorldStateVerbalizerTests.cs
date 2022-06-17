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
        public void ShouldReturnEmptyForEmptyWorldState()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = new ItemReference<WorldEventState>(new WorldEventState("id", "name"), 0, 0, 0);
            var result = verbalizer.Verbalize(worldState);

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsAlive()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(TrueValue, AliveIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsAliveNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(FalseValue, AliveIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is killed or imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsKilled()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(TrueValue, KilledIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is killed";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsKilledNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(FalseValue, KilledIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is alive or imprisoned"; // doesn't really make sense
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsImprisoned()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(TrueValue, ImprisonedIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsImprisonedNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(FalseValue, ImprisonedIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is killed or alive"; // doesn't make sense at all
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereTwoNpcsAreAlive()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(TrueValue, AliveIdentifier, 2);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] and [[character1]] are alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereThreeNpcsAreAliveNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsWorldState(FalseValue, AliveIdentifier, 3);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]], [[character1]] and [[character2]] are killed or imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsNotAlive()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsNotWorldState(TrueValue, AliveIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is not alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneNpcIsNotAliveNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsNotWorldState(FalseValue, AliveIdentifier, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] is not killed or imprisoned";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereTwoNpcsAreNotAlive()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsNotWorldState(TrueValue, AliveIdentifier, 2);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]] and [[character1]] are not alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereThreeNpcsAreNotAlive()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupCharacterIsNotWorldState(TrueValue, AliveIdentifier, 3);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[character0]], [[character1]] and [[character2]] are not alive";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsAllied()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionAllyWorldState(TrueValue, TrueValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is allied to the player";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsAlliedNegated2()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionAllyWorldState(TrueValue, FalseValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is not allied to the player";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsAlliedNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionAllyWorldState(FalseValue, TrueValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is not allied to the player";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsAlliedDoubleNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionAllyWorldState(FalseValue, FalseValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is allied to the player";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsEnemy()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionEnemyWorldState(TrueValue, TrueValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is an enemy of the player";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsEnemyNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionEnemyWorldState(FalseValue, TrueValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is not an enemy of the player";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsEnemyNegated2()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionEnemyWorldState(TrueValue, FalseValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is not an enemy of the player";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldVerbalizeWorldStatesWhereOneFactionIsEnemyDoubleNegated()
        {
            var verbalizer = new WorldStateVerbalizer();
            var worldState = SetupFactionEnemyWorldState(FalseValue, FalseValue, 1);
            var result = verbalizer.Verbalize(worldState);

            var expected = $"[[faction0]] is an enemy of the player";
            Assert.AreEqual(expected, result);
        }

        private ItemReference<WorldEventState> SetupCharacterIsWorldState(int worldStateValue, int subjectStateValue, int characterCount)
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

        private ItemReference<WorldEventState> SetupCharacterIsNotWorldState(int worldStateValue, int subjectStateValue, int characterCount)
        {
            var worldState = new WorldEventState("worldstateid", "name");

            var characters = new List<ItemReference<Character>>();
            for (var i = 0; i < characterCount; i++)
            {
                var character = new Character($"characterid{i}", $"character{i}");
                characters.Add(new ItemReference<Character>(character, subjectStateValue, 0, 0));
            }

            worldState.NpcIsNot = characters;
            return new ItemReference<WorldEventState>(worldState, worldStateValue, 0, 0);
        }

        private ItemReference<WorldEventState> SetupFactionAllyWorldState(int worldStateValue, int subjectStateValue, int factionCount)
        {
            var worldState = new WorldEventState("worldstateid", "name");

            var factions = new List<ItemReference<Faction>>();
            for (var i = 0; i < factionCount; i++)
            {
                var faction = new Faction($"factionid{i}", $"faction{i}");
                factions.Add(new ItemReference<Faction>(faction, subjectStateValue, 0, 0));
            }

            worldState.PlayerAlly = factions;
            return new ItemReference<WorldEventState>(worldState, worldStateValue, 0, 0);
        }

        private ItemReference<WorldEventState> SetupFactionEnemyWorldState(int worldStateValue, int subjectStateValue, int factionCount)
        {
            var worldState = new WorldEventState("worldstateid", "name");

            var factions = new List<ItemReference<Faction>>();
            for (var i = 0; i < factionCount; i++)
            {
                var faction = new Faction($"factionid{i}", $"faction{i}");
                factions.Add(new ItemReference<Faction>(faction, subjectStateValue, 0, 0));
            }

            worldState.PlayerEnemy = factions;
            return new ItemReference<WorldEventState>(worldState, worldStateValue, 0, 0);
        }
    }
}
