using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.Locations;
using KenshiWikiValidator.WikiCategories.Locations.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.WikiCategories.Locations.Templates
{
    [TestClass]
    public class TownTemplateCreatorTests
    {
        [TestMethod]
        public void ShouldReturnNullForEmptyArticleData()
        {
            var repository = new Mock<IItemRepository>();
            var zoneDataProvider = new Mock<IZoneDataProvider>();
            var creator = new TownTemplateCreator(repository.Object, zoneDataProvider.Object, new WikiTitleCache(), new ArticleData());

            var template = creator.Generate();

            Assert.IsNull(template);
        }

        [TestMethod]
        public void ShouldNotReturnNullForValidStringId()
        {
            var town = new Town("stringid", "town name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Town>("stringid"))
                .Returns(town);
            var zoneDataProvider = new Mock<IZoneDataProvider>();
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new TownTemplateCreator(repository.Object, zoneDataProvider.Object, new WikiTitleCache(), articleData);

            var template = creator.Generate();

            Assert.IsNotNull(template);
        }
    }
}
