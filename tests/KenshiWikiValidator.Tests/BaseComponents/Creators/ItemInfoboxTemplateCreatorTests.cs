using System;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.BaseComponents.Creators;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.BaseComponents.Creators
{
    [TestClass]
    public class ItemInfoboxTemplateCreatorTests
    {
        [TestMethod]
        public void ShouldReturnNullForEmptyArticleData()
        {
            var repository = new Mock<IItemRepository>();
            var creator = new ItemInfoboxTemplateCreator(repository.Object);

            var template = creator.Generate(new ArticleData());

            Assert.IsNull(template);
        }

        [TestMethod]
        public void ShouldNotReturnNullForValidStringId()
        {
            var weapon = new MapItem("stringid", "map item name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId("stringid"))
                .Returns(weapon);
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new ItemInfoboxTemplateCreator(repository.Object);

            var template = creator.Generate(articleData);

            Assert.IsNotNull(template);
        }

        [TestMethod]
        public void ShouldThrowIfDataItemOtherThanMapItemOrItemIsProvided()
        {
            var weapon = new Weapon("stringid", "weapon name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId("stringid"))
                .Returns(weapon);
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new ItemInfoboxTemplateCreator(repository.Object);

            Assert.ThrowsException<InvalidOperationException>(() => creator.Generate(articleData));
        }
    }
}
