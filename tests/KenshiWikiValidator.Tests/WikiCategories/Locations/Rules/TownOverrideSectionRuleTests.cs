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
            var parentTown = new Town("2608-gamedata.base", "Admag");
            var firstChildTown = new Town("1533314-__world reactions Shek.mod", "Admag (override) Seto");
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
    }
}
