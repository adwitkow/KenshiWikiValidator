using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.BaseComponents
{
    [TestClass]
    public class ArticleValidatorBaseTests
    {
        [TestMethod]
        public void ShouldParseCategories()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var validatorMock = new Mock<ArticleValidatorBase>(repositoryMock.Object, new WikiTitleCache());

            validatorMock.Object.CachePageData("title", "[[Category:Test]]");

            var articleCategories = validatorMock.Object.ArticleDataMap["title"].Categories;
            Assert.AreEqual(1, articleCategories.Count);
            Assert.AreEqual("Test", articleCategories.First());
        }

        [TestMethod]
        public void ShouldSucceedIfAllRulesSucceed()
        {
            var successResult = new RuleResult();
            var rule = new Mock<IValidationRule>();
            rule.Setup(r => r.Execute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ArticleData>()))
                .Returns(successResult);

            var repositoryMock = new Mock<IItemRepository>();
            var validatorMock = new Mock<ArticleValidatorBase>(repositoryMock.Object, new WikiTitleCache());
            validatorMock
                .Setup(v => v.Rules)
                .Returns(new List<IValidationRule>() { rule.Object, rule.Object, rule.Object });

            var result = validatorMock.Object.Validate("title", "content");

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldFailIfOneRuleFails()
        {
            var successResult = new RuleResult();
            var failResult = new RuleResult();
            failResult.AddIssue("Fail");

            var successRule = new Mock<IValidationRule>();
            successRule.Setup(r => r.Execute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ArticleData>()))
                .Returns(successResult);
            var failRule = new Mock<IValidationRule>();
            failRule.Setup(r => r.Execute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ArticleData>()))
                .Returns(failResult);

            var repositoryMock = new Mock<IItemRepository>();
            var validatorMock = new Mock<ArticleValidatorBase>(repositoryMock.Object, new WikiTitleCache());
            validatorMock
                .Setup(v => v.Rules)
                .Returns(new List<IValidationRule>() { successRule.Object, successRule.Object, failRule.Object });

            var result = validatorMock.Object.Validate("title", "content");

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldFailIfAllRulesFail()
        {
            var failResult = new RuleResult();
            failResult.AddIssue("Fail");

            var failRule = new Mock<IValidationRule>();
            failRule.Setup(r => r.Execute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ArticleData>()))
                .Returns(failResult);

            var repositoryMock = new Mock<IItemRepository>();
            var validatorMock = new Mock<ArticleValidatorBase>(repositoryMock.Object, new WikiTitleCache());
            validatorMock
                .Setup(v => v.Rules)
                .Returns(new List<IValidationRule>() { failRule.Object, failRule.Object, failRule.Object });

            var result = validatorMock.Object.Validate("title", "content");

            Assert.IsFalse(result.Success);
        }
    }
}
