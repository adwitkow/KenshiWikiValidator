using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.WeaponComponents;
using KenshiWikiValidator.WikiCategories.SharedRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;
using System.Collections.Generic;
using System.IO;

namespace KenshiWikiValidator.Tests.Features.ArticleValidation.Validators.Rules
{
    [TestClass]
    public class StringIdRuleTests
    {
        [TestMethod]
        public void ShouldSucceedIfArticleContainsCorrectStringId()
        {
            var wakizashi = new DataItem(ItemType.Weapon, 0, "Wakizashi", "1020-gamedata.base");
            var textToValidate = "{{Weapon|string id = 1020-gamedata.base}}";

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetDataItems())
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
            itemRepository
                .Setup(repo => repo.GetDataItemByStringId("invalidStringId"))
                .Throws<KeyNotFoundException>();

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache()) });

            Assert.ThrowsException<KeyNotFoundException>(() => validator.Object.Validate("Wakizashi", textToValidate));
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
    }
}