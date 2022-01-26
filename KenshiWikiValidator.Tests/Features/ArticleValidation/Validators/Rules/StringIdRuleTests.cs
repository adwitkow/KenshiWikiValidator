using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var wakizashi = new Weapon()
            {
                Name = "Wakizashi",
                StringId = "1020-gamedata.base"
            };

            var itemRepository = new Mock<IItemRepository>();
            itemRepository.Setup(repo => repo.GetItems()).Returns(new[] { wakizashi });
            var rule = new StringIdRule(itemRepository.Object);

            var result = rule.Execute("Wakizashi", this.correctResourceContent!);

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
            itemRepository.Setup(repo => repo.GetItems()).Returns(new[] { wakizashi });
            var rule = new StringIdRule(itemRepository.Object);

            var result = rule.Execute("Wakizashi", this.incorrectResourceContent!);

            Assert.IsFalse(result.Success);
        }
    }
}
