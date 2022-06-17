using System.Collections.Generic;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.Locations.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.WikiCategories.Locations.Rules
{
    [TestClass]
    public class TownOverrideSectionRuleTests
    {
        [TestMethod]
        public void ShouldNotThrowIfStringIdsAreEmpty()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var titleCacheMock = new Mock<IWikiTitleCache>();

            var rule = new TownOverrideSectionRule(repositoryMock.Object, titleCacheMock.Object);

            var result = rule.Execute("Title", "Content", new ArticleData());

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldNotThrowIfTownOverridesAreEmpty()
        {
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Town>("2608-gamedata.base"))
                .Returns(new Town("2608-gamedata.base", "Admag"));
            var titleCacheMock = new Mock<IWikiTitleCache>();

            var rule = new TownOverrideSectionRule(repositoryMock.Object, titleCacheMock.Object);

            var articleData = new ArticleData()
            {
                StringIds = new[] { "2608-gamedata.base" }
            };
            var result = rule.Execute("Title", "Content", articleData);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldGenerateSingleOverrideSection()
        {
            var worldState = SetupCharacterIsWorldState(1);
            var parentTown = new Town("2608-gamedata.base", "Admag");
            var firstChildTown = new Town("1533314-__world reactions Shek.mod", "Admag (override) Seto");
            firstChildTown.WorldStates = new[] { worldState };
            parentTown.OverrideTown = new[] { new ItemReference<Town>(firstChildTown, 0, 0, 0) };

            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Town>("2608-gamedata.base"))
                .Returns(parentTown);
            var titleCacheMock = new Mock<IWikiTitleCache>();
            titleCacheMock
                .Setup(titleCache => titleCache.GetTitle(It.IsAny<IItem>()))
                .Returns("Result");

            var rule = new TownOverrideSectionRule(repositoryMock.Object, titleCacheMock.Object);

            var articleData = new ArticleData()
            {
                StringIds = new[] { "2608-gamedata.base" }
            };

            var expectedText = @"== Town override ==
{{Main|Result}}
[[character0]] is alive or imprisoned
";
            var result = rule.Execute("Title", expectedText, articleData);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldGenerateTabViewSection()
        {
            var parentTown = new Town("2608-gamedata.base", "Admag");
            var firstChildTown = new Town("1533314-__world reactions Shek.mod", "Admag/Weakened");
            var secondChildTown = new Town("1533317-__world reactions Shek.mod", "Admag/Berserkers");
            parentTown.OverrideTown = new[]
            {
                new ItemReference<Town>(firstChildTown, 0, 0, 0),
                new ItemReference<Town>(secondChildTown, 0, 0, 0),
            };

            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Town>("2608-gamedata.base"))
                .Returns(parentTown);
            var titleCacheMock = new Mock<IWikiTitleCache>();
            titleCacheMock
                .Setup(titleCache => titleCache.GetTitle(It.IsAny<IItem>()))
                .Returns<IItem>(result => result.Name);

            var rule = new TownOverrideSectionRule(repositoryMock.Object, titleCacheMock.Object);

            var articleData = new ArticleData()
            {
                StringIds = new[] { "2608-gamedata.base" }
            };

            var expectedContent = @"== Town overrides ==
'''Admag''' can be affected by multiple [[World States]] to produce the following [[Town Overrides]].

<tabview>
Admag/Weakened   | Weakened
Admag/Berserkers | Berserkers
</tabview>";
            var result = rule.Execute("Title", expectedContent, articleData);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldOrderTabViewOverridesByTheAmountOfWorldStateConditions()
        {
            var worldState3 = SetupCharacterIsWorldState(3);
            var worldState2 = SetupCharacterIsWorldState(2);
            var worldState4 = SetupCharacterIsWorldState(4);
            var worldState1 = SetupCharacterIsWorldState(1);
            var worldState5 = SetupCharacterIsWorldState(5);

            var worldStatesLower = new[]
            {
                worldState3, worldState4,
            };

            var worldStatesHigher = new[]
            {
                worldState2, worldState5, worldState1
            };

            var parentTown = new Town("2608-gamedata.base", "Admag");
            var firstChildTown = new Town("1533314-__world reactions Shek.mod", "Admag/Weakened");
            firstChildTown.WorldStates = worldStatesLower;
            var secondChildTown = new Town("1533317-__world reactions Shek.mod", "Admag/Berserkers");
            secondChildTown.WorldStates = worldStatesHigher;

            parentTown.OverrideTown = new[]
            {
                new ItemReference<Town>(secondChildTown, 0, 0, 0),
                new ItemReference<Town>(firstChildTown, 0, 0, 0),
            };

            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Town>("2608-gamedata.base"))
                .Returns(parentTown);
            var titleCacheMock = new Mock<IWikiTitleCache>();
            titleCacheMock
                .Setup(titleCache => titleCache.GetTitle(It.IsAny<IItem>()))
                .Returns<IItem>(result => result.Name);

            var rule = new TownOverrideSectionRule(repositoryMock.Object, titleCacheMock.Object);

            var articleData = new ArticleData()
            {
                StringIds = new[] { "2608-gamedata.base" }
            };

            var expectedContent = @"== Town overrides ==
'''Admag''' can be affected by multiple [[World States]] to produce the following [[Town Overrides]].

<tabview>
Admag/Weakened   | Weakened
Admag/Berserkers | Berserkers
</tabview>";
            var result = rule.Execute("Title", expectedContent, articleData);

            Assert.IsTrue(result.Success);
        }

        private ItemReference<WorldEventState> SetupCharacterIsWorldState(int characterCount)
        {
            var worldState = new WorldEventState("worldstateid", "name");

            var characters = new List<ItemReference<Character>>();
            for (var i = 0; i < characterCount; i++)
            {
                var character = new Character($"characterid{i}", $"character{i}");
                characters.Add(new ItemReference<Character>(character, 0, 0, 0));
            }

            worldState.NpcIs = characters;
            return new ItemReference<WorldEventState>(worldState, 0, 0, 0);
        }
    }
}
