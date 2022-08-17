using System.Linq;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.Locations;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.WikiCategories.Locations
{
    [TestClass]
    public class LocationsArticleValidatorTests
    {
        [TestMethod]
        public void ShouldConstruct()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var zoneProviderMock = new Mock<IZoneDataProvider>();
            var titleCacheMock = new Mock<IWikiTitleCache>();
            var validator = new LocationsArticleValidator(
                repositoryMock.Object,
                zoneProviderMock.Object,
                titleCacheMock.Object);

            Assert.IsNotNull(validator);
        }

        [TestMethod]
        public void ShouldExecuteContainsTownTemplateRuleAfterValidationsForRemainingItems()
        {
            var town1 = new Town("Town1", "Town1Id")
            {
                TownType = TownType.Town
            };
            var town2 = new Town("Town2", "Town2Id")
            {
                TownType = TownType.Town
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock.Setup(repo => repo.GetItems())
                .Returns(new[]
                {
                    town1,
                    town2,
                });
            repositoryMock.Setup(repo => repo.GetItemByStringId(town1.StringId))
                .Returns(town1);
            repositoryMock.Setup(repo => repo.GetItemByStringId(town2.StringId))
                .Returns(town2);
            repositoryMock.Setup(repo => repo.GetItemByStringId<Town>(town1.StringId))
                .Returns(town1);
            repositoryMock.Setup(repo => repo.GetItemByStringId<Town>(town2.StringId))
                .Returns(town2);
            var zoneProviderMock = new Mock<IZoneDataProvider>();
            var titleCacheMock = new Mock<IWikiTitleCache>();
            titleCacheMock.Setup(cache => cache.GetTitle(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((input1, input2) => input2);
            var validator = new LocationsArticleValidator(
                repositoryMock.Object,
                zoneProviderMock.Object,
                titleCacheMock.Object);

            validator.PopulateStringIds();
            validator.Validate("title", "content");
            var results = validator.AfterValidations();

            Assert.AreEqual(2, results.Count());
        }
    }
}
