using KenshiWikiValidator.Features.ArticleValidation;
using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.WeaponComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;
using System.IO;

namespace KenshiWikiValidator.Tests.Features.ArticleValidation.Validators.Rules
{
    [TestClass]
    public class StringIdRuleTests
    {
        private string? incorrectResourceContent;
        private string? correctResourceContent;

        [TestInitialize]
        public void Initialize()
        {
            this.incorrectResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorIncorrectResource.txt");
            this.correctResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorCorrectResource.txt");
        }

        [TestMethod]
        public void ShouldSucceedIfArticleContainsCorrectStringId()
        {
            var wakizashi = new DataItem(ItemType.Weapon, 0, "Wakizashi", "1020-gamedata.base");

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetDataItems())
                .Returns(new[] { wakizashi });

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache()) });

            var result = validator.Object.Validate("Wakizashi", this.correctResourceContent!);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldFailIfArticleContainsIncorrectStringId()
        {
            var wakizashi = new Weapon()
            {
                Name = "Wakizashi",
                StringId = "1020-gamedata.base"
            };

            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItems())
                .Returns(new[] { wakizashi });

            var validator = new Mock<ArticleValidatorBase>();
            validator
                .Setup(v => v.Rules)
                .Returns(new[] { new StringIdRule(itemRepository.Object, new WikiTitleCache()) });

            var result = validator.Object.Validate("Wakizashi", this.incorrectResourceContent!);

            Assert.IsFalse(result.Success);
        }
    }
}
