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
using KenshiWikiValidator.WikiTemplates;
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

        [TestMethod]
        public void ShouldNotReturnNullForPotentialStringId()
        {
            var town = new Town("stringid", "town name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Town>("stringid"))
                .Returns(town);
            var zoneDataProvider = new Mock<IZoneDataProvider>();
            var articleData = new ArticleData
            {
                PotentialStringId = "stringid"
            };

            var creator = new TownTemplateCreator(repository.Object, zoneDataProvider.Object, new WikiTitleCache(), articleData);

            var template = creator.Generate();

            Assert.IsNotNull(template);
        }

        [TestMethod]
        public void ShouldConvertFaction()
        {
            var faction = new Faction("factionid", "faction name");
            var town = new Town("stringid", "town name")
            {
                Faction = new[] { new ItemReference<Faction>(faction, 0, 0, 0) }
            };
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
            Assert.AreEqual("[[faction name]]", template.Parameters["factions"]);
        }

        [TestMethod]
        public void ShouldConvertRegion()
        {
            var town = new Town("stringid", "town name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Town>("stringid"))
                .Returns(town);
            var zoneDataProvider = new Mock<IZoneDataProvider>();
            zoneDataProvider
                .Setup(provider => provider.GetZones("town name"))
                .Returns(new[] {"zone name"});
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new TownTemplateCreator(repository.Object, zoneDataProvider.Object, new WikiTitleCache(), articleData);

            var template = creator.Generate();

            Assert.IsNotNull(template);
            Assert.AreEqual("[[zone name]]", template.Parameters["biome"]);
        }


        [TestMethod]
        public void ShouldCopyExistingPropertiesCorrectly()
        {
            var town = new Town("stringid", "town name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Town>("stringid"))
                .Returns(town);
            var zoneDataProvider = new Mock<IZoneDataProvider>();
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");
            articleData.WikiTemplates = new[]
            {
                new WikiTemplate("Town", new SortedList<string, string?>()
                {
                    { "image1", "old image" }
                })
            };

            var creator = new TownTemplateCreator(repository.Object, zoneDataProvider.Object, new WikiTitleCache(), articleData);

            var template = creator.Generate();

            Assert.IsNotNull(template);
            Assert.AreEqual("old image", template.Parameters["image1"]);
        }

        [TestMethod]
        public void ShouldFindBaseTownZoneEvenIfNameDoesNotMatch()
        {
            var town = new Town("subpagetownstringid", "completely different town name");
            var baseTown = new Town("basetownstringid", "base town")
            {
                OverrideTown = new[] { new ItemReference<Town>(town, 0, 0, 0) }
            };
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Town>(town.StringId))
                .Returns(town);
            repository
                .Setup(repo => repo.GetItemByStringId<Town>(baseTown.StringId))
                .Returns(baseTown);
            repository
                .Setup(repo => repo.GetItems<Town>())
                .Returns(new[] { baseTown, town });
            var zoneDataProvider = new Mock<IZoneDataProvider>();
            zoneDataProvider
                .Setup(provider => provider.GetZones(baseTown.Name))
                .Returns(new[] { "zone name" });
            var articleData = new ArticleData();
            articleData.StringIds.Add(town.StringId);

            var creator = new TownTemplateCreator(repository.Object, zoneDataProvider.Object, new WikiTitleCache(), articleData);

            var template = creator.Generate();

            Assert.IsNotNull(template);
            Assert.AreEqual("[[zone name]]", template.Parameters["biome"]);
        }

        [TestMethod]
        public void ShouldAddFcsNameIfArticleTitleDoesNotMatch()
        {
            var town = new Town("stringid", "town name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Town>("stringid"))
                .Returns(town);
            var zoneDataProvider = new Mock<IZoneDataProvider>();
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");
            var wikiTitleCache = new Mock<IWikiTitleCache>();
            wikiTitleCache
                .Setup(cache => cache.GetTitle("stringid", "town name"))
                .Returns("article title");

            var creator = new TownTemplateCreator(repository.Object, zoneDataProvider.Object, wikiTitleCache.Object, articleData);

            var template = creator.Generate();

            Assert.IsNotNull(template);
            Assert.AreEqual("article title", template.Parameters["title1"]);
        }
    }
}
