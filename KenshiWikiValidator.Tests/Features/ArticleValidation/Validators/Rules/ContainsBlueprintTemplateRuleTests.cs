using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;

namespace KenshiWikiValidator.Tests.Features.ArticleValidation.Validators.Rules
{
    [TestClass]
    public class ContainsBlueprintTemplateRuleTests
    {
        private string incorrectResourceContent;
        private string correctResourceContent;

        [TestInitialize]
        public void Initialize()
        {
            this.incorrectResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorIncorrectResource.txt");
            this.correctResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorCorrectResource.txt");
        }

        [TestMethod]
        public void ShouldSucceedIfArticleContainsBlueprintTemplate()
        {
            var wakizashi = new Weapon()
            {
                Name = "Wakizashi",
                StringId = "1020-gamedata.base"
            };

            var itemRepository = new Mock<IItemRepository>();
            itemRepository.Setup(repo => repo.GetItemByStringId(It.IsAny<string>())).Returns(wakizashi);
            var rule = new ContainsBlueprintTemplateRule(itemRepository.Object);
            var data = new ArticleData();
            data.Add("string id", "1020-gamedata.base");

            var result = rule.Execute("Wakizashi", this.correctResourceContent, data);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldFailIfArticleContainsIncorrectBlueprintTemplate()
        {
            var itemRepository = new Mock<IItemRepository>();
            var rule = new ContainsBlueprintTemplateRule(itemRepository.Object);
            var data = new ArticleData();

            var result = rule.Execute("Wakizashi", this.incorrectResourceContent, data);

            Assert.IsFalse(result.Success);
        }
    }
}
