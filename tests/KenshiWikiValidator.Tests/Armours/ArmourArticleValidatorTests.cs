using System.Linq;
using KenshiWikiValidator.Armours;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.Armours
{
    [TestClass]
    public class ArmourArticleValidatorTests
    {
        [TestMethod]
        public void ShouldHaveRulesUponConstruction()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var wikiCacheMock = new Mock<IWikiTitleCache>();

            var validator = new ArmourArticleValidator(repositoryMock.Object, wikiCacheMock.Object);
            Assert.IsTrue(validator.Rules.Any());
        }
    }
}
