using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.SharedRules;
using KenshiWikiValidator.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace KenshiWikiValidator.Tests.BaseComponents
{
    [TestClass]
    public class StringIdFinderTests
    {
        [TestMethod]
        public void ShouldSucceedIfArticleContainsCorrectStringId()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon|string id = 1020-gamedata.base}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            var stringIdRule = new StringIdFinder(itemRepository.Object, new WikiTitleCache());
            stringIdRule.PopulateStringIds("Wakizashi", articleData, "Weapons");

            Assert.AreEqual(wakizashi.StringId, articleData.StringIds.First());
        }

        [TestMethod]
        public void ShouldFindPotentialIfArticleContainsIncorrectStringId()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon|string id = invalidStringId}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });

            var stringIdRule = new StringIdFinder(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            stringIdRule.PopulateStringIds("Wakizashi", articleData, "Weapons");

            Assert.IsFalse(articleData.StringIds.Any());
            Assert.IsFalse(string.IsNullOrEmpty(articleData.PotentialStringId));
        }

        [TestMethod]
        public void ShouldNotFindAnyStringIdsIfStringIdIsMissing()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });

            var stringIdRule = new StringIdFinder(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            stringIdRule.PopulateStringIds("Wakizashi", articleData, "Weapons");

            Assert.IsFalse(articleData.StringIds.Any());
        }

        [TestMethod]
        public void PotentialStringIdShouldMatchIfTitleMatches()
        {
            var lostArmoury = new Town("49935-rebirth.mod", "Lost Armoury");
            var textToValidate = "Content without a template";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { lostArmoury });

            var stringIdRule = new StringIdFinder(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            stringIdRule.PopulateStringIds("Lost Armoury", articleData, "Locations");

            Assert.AreEqual(lostArmoury.StringId, articleData.PotentialStringId);
        }

        [TestMethod]
        public void ShouldFindPotentialStringIdIfFcsNameIsPresent()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon|fcs_name = Wakizashi}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });

            var stringIdRule = new StringIdFinder(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            stringIdRule.PopulateStringIds("Wakizashi", articleData, "Weapons");

            Assert.IsFalse(articleData.StringIds.Any());
            Assert.IsFalse(string.IsNullOrEmpty(articleData.PotentialStringId));
        }
    }
}