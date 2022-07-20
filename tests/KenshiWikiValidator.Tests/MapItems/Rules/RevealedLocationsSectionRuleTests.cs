using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.Locations;
using KenshiWikiValidator.MapItems.Rules;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.MapItems.Rules
{
    [TestClass]
    public class RevealedLocationsSectionRuleTests
    {
        [TestMethod]
        public void ShouldResultInHundredPercentChancesIfUnlockCountIsZero()
        {
            var mapItem = new MapItem("stringId", "name")
            {
                UnlockCount = 0
            };
            mapItem.Towns = new[]
            {
                new ItemReference<Town>(new Town("1","1"), 1, 100, 0),
                new ItemReference<Town>(new Town("2","2"), 2, 200, 0),
                new ItemReference<Town>(new Town("3","3"), 3, 400, 0),
            };

            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock.Setup(repo => repo.GetItemByStringId<MapItem>("stringId"))
                .Returns(mapItem);
            var titleCacheMock = new Mock<IWikiTitleCache>();
            var zoneProviderMock = new Mock<IZoneDataProvider>();

            var rule = new RevealedLocationsSectionRule(
                repositoryMock.Object,
                titleCacheMock.Object,
                zoneProviderMock.Object);

            var articleData = new ArticleData()
            {
                StringIds = new[] { "stringId" }
            };

            var result = rule.Execute("title", @"== Revealed locations ==
Upon use, this item will reveal every single location available in the table below.

{| class=""wikitable sortable"" style=""text-align: center;""
! Location !! Amount !! Chance
|-
| style=""text-align:left;"" | [[]] || 1 || 100%
|-
| style=""text-align:left;"" | [[]] || 2 || 100%
|-
| style=""text-align:left;"" | [[]] || 3 || 100%
|}", articleData);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldCalculateChancesProperlyIfUnlockCountIsNotZero()
        {
            var mapItem = new MapItem("stringId", "name")
            {
                UnlockCount = 1
            };
            mapItem.Towns = new[]
            {
                new ItemReference<Town>(new Town("1","1"), 1, 100, 0),
                new ItemReference<Town>(new Town("2","2"), 2, 200, 0),
                new ItemReference<Town>(new Town("3","3"), 3, 400, 0),
            };

            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock.Setup(repo => repo.GetItemByStringId<MapItem>("stringId"))
                .Returns(mapItem);
            var titleCacheMock = new Mock<IWikiTitleCache>();
            var zoneProviderMock = new Mock<IZoneDataProvider>();

            var rule = new RevealedLocationsSectionRule(
                repositoryMock.Object,
                titleCacheMock.Object,
                zoneProviderMock.Object);

            var articleData = new ArticleData()
            {
                StringIds = new[] { "stringId" }
            };

            var result = rule.Execute("title", @"== Revealed locations ==
Upon use, this item will reveal a single location available in the table below &mdash; chosen randomly.

{| class=""wikitable sortable"" style=""text-align: center;""
! Location !! Amount !! Chance
|-
| style=""text-align:left;"" | [[]] || 3 || 57.14%
|-
| style=""text-align:left;"" | [[]] || 2 || 28.57%
|-
| style=""text-align:left;"" | [[]] || 1 || 14.29%
|}", articleData);

            Assert.IsTrue(result.Success);
        }
    }
}
