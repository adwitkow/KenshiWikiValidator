using KenshiWikiValidator.Features.ArticleValidation.Validators;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;

namespace KenshiWikiValidator.Tests.Features.ArticleValidation.Validators
{
    [TestClass]
    public class WeaponArticleValidatorTests
    {
        private TemplateParser templateParser;
        private string incorrectResourceContent;
        private string correctResourceContent;

        [TestInitialize]
        public void Initialize()
        {
            this.incorrectResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorIncorrectResource.txt");
            this.correctResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorCorrectResource.txt");
            this.templateParser = new TemplateParser();
        }

        [TestMethod]
        public void ShouldReturnArticleValidationResult()
        {
            var wakizashi = new Weapon()
            {
                Name = "Wakizashi",
                StringId = "1020-gamedata.base"
            };

            var itemRepository = new Mock<IItemRepository>();
            itemRepository.Setup(repo => repo.GetItems()).Returns(new[] { wakizashi });
            var validator = new WeaponArticleValidator(itemRepository.Object);

            var result = validator.Validate("Wakizashi", this.incorrectResourceContent);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldNotSucceedForIncorrectResource()
        {
            var wakizashi = new Weapon()
            {
                Name = "Wakizashi",
                StringId = "1020-gamedata.base"
            };

            var itemRepository = new Mock<IItemRepository>();
            itemRepository.Setup(repo => repo.GetItems()).Returns(new[] { wakizashi });
            var validator = new WeaponArticleValidator(itemRepository.Object);

            var result = validator.Validate("Wakizashi", this.incorrectResourceContent);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedForCorrectResource()
        {
            var wakizashi = new Weapon()
            {
                Name = "Wakizashi",
                StringId = "1020-gamedata.base"
            };

            var itemRepository = new Mock<IItemRepository>();
            itemRepository.Setup(repo => repo.GetItems()).Returns(new[] { wakizashi });
            var validator = new WeaponArticleValidator(itemRepository.Object);

            var result = validator.Validate("Wakizashi", this.correctResourceContent);

            Assert.IsTrue(result.Success);
        }
    }
}
