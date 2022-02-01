using KenshiWikiValidator.Features.ArticleValidation;
using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.DataItemConversion;
using KenshiWikiValidator.Features.DataItemConversion.Models;
using KenshiWikiValidator.Features.DataItemConversion.Models.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;

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
            var wakizashiId = "wakizashiId";
            var wakizashiResearchStringId = "wakizashiResearchStringId";
            var wakizashi = new Weapon()
            {
                Name = "Wakizashi",
                StringId = wakizashiId,
                UnlockingResearch = new ItemReference(wakizashiResearchStringId, "Wakizashis")
            };



            var itemRepository = new Mock<IItemRepository>();
            itemRepository.Setup(repo => repo.GetItemByStringId(wakizashiId)).Returns(wakizashi);
            itemRepository.Setup(repo => repo.GetItemByStringId(wakizashiResearchStringId)).Returns(wakizashi);
            var rule = new ContainsBlueprintTemplateRule(itemRepository.Object, new WikiTitleCache());
            var data = new ArticleData();
            data.Add("string id", "1020-gamedata.base");

            var result = rule.Execute("Wakizashi", this.correctResourceContent, data);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldFailIfArticleContainsIncorrectBlueprintTemplate()
        {
            var itemRepository = new Mock<IItemRepository>();
            var rule = new ContainsBlueprintTemplateRule(itemRepository.Object, new WikiTitleCache());
            var data = new ArticleData();

            var result = rule.Execute("Wakizashi", this.incorrectResourceContent, data);

            Assert.IsFalse(result.Success);
        }
    }
}
