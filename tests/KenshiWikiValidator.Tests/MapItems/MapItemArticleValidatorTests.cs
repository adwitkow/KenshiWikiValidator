using System.Linq;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.MapItems;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.MapItems
{
    [TestClass]
    public class MapItemArticleValidatorTests
    {
        [TestMethod]
        public void ShouldConstruct()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var titleCacheMock = new Mock<IWikiTitleCache>();
            var validator = new MapItemArticleValidator(
                repositoryMock.Object,
                titleCacheMock.Object);

            Assert.IsNotNull(validator);
        }

        [TestMethod]
        public void ShouldExecuteContainsTownTemplateRuleAfterValidationsForRemainingItems()
        {
            var mapitem1 = new MapItem("MapItem1", "MapItem1Id");
            var mapItem2 = new MapItem("MapItem2", "MapItem2Id");
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock.Setup(repo => repo.GetItems())
                .Returns(new[]
                {
                    mapitem1,
                    mapItem2,
                });
            repositoryMock.Setup(repo => repo.GetItemByStringId(mapitem1.StringId))
                .Returns(mapitem1);
            repositoryMock.Setup(repo => repo.GetItemByStringId(mapItem2.StringId))
                .Returns(mapItem2);
            var titleCacheMock = new Mock<IWikiTitleCache>();
            var validator = new MapItemArticleValidator(
                repositoryMock.Object,
                titleCacheMock.Object);

            validator.PopulateStringIds();
            validator.Validate("title", "content");
            var results = validator.AfterValidations();

            Assert.AreEqual(2, results.Count());
        }
    }
}
