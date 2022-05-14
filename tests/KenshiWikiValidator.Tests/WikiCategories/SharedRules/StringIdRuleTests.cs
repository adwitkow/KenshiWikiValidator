using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.SharedRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache()) });

            var result = validator.Object.Validate("Wakizashi", textToValidate);

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

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache()) });

            Assert.IsFalse(validator.Object.Validate("Wakizashi", textToValidate).Success);
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

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache()) });

            var result = validator.Object.Validate("Wakizashi", textToValidate);

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

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache()) });

            validator.Object.Validate("Lost Armoury", textToValidate);
            
            Assert.AreEqual("49935-rebirth.mod", validator.Object.Data.PotentialStringId);
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

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache(), true) });

            var result = validator.Object.Validate("Wakizashi - title different from the FCS name", textToValidate);

            Assert.AreEqual("No string id! Most likely string id: [string id = 1020-gamedata.base|]", result.Issues.First());
        }

        [TestMethod]
        public void ShouldFilterByTypeCorrectly()
        {
            var wakizashi = new Weapon("1020-gamedata.base", "Wakizashi");
            var textToValidate = "{{Weapon|string id = 1020-gamedata.base}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems<Weapon>())
                .Returns(new[] { wakizashi });

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule<Weapon>(itemRepository.Object, new WikiTitleCache(), true) });

            var result = validator.Object.Validate("Wakizashi", textToValidate);

            Assert.IsTrue(result.Success);
        }
    }
}