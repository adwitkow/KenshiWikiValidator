using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.SharedRules;
using KenshiWikiValidator.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace KenshiWikiValidator.Tests.WikiCategories.SharedRules
{
    [TestClass]
    public class StringIdRuleTests
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

            var stringIdRule = new StringIdRule(itemRepository.Object, new WikiTitleCache());
            var articleData = new ArticleData();
            var result = stringIdRule.Execute("Wakizashi", textToValidate, articleData);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldFailIfArticleContainsIncorrectStringId()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon|string id = invalidStringId}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });

            var stringIdRule = new StringIdRule(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            var result = stringIdRule.Execute("Wakizashi", textToValidate, articleData);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldFailIfStringIdIsMissing()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });

            var stringIdRule = new StringIdRule(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            var result = stringIdRule.Execute("Wakizashi", textToValidate, articleData);

            Assert.IsFalse(result.Success);
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

            var stringIdRule = new StringIdRule(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            stringIdRule.Execute("Lost Armoury", textToValidate, articleData);

            Assert.AreEqual("49935-rebirth.mod", articleData.PotentialStringId);
        }

        [TestMethod]
        public void ShouldFailWithCorrectStringIdIfFcsNameIsPresent()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon|fcs_name = Wakizashi}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });

            var stringIdRule = new StringIdRule(itemRepository.Object, new WikiTitleCache());
            var templateParser = new TemplateParser();
            var articleData = new ArticleData()
            {
                WikiTemplates = templateParser.ParseAllTemplates(textToValidate)
            };
            var result = stringIdRule.Execute("Wakizashi - title different from the FCS name", textToValidate, articleData);

            Assert.AreEqual("No string id! Most likely string id: [string id = 1020-gamedata.base|]", result.Issues.First());
        }
    }
}