using KenshiWikiValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace KenshiWikiValidator.Tests
{
    [TestClass]
    public class WeaponArticleValidatorTests
    {
        private string resourceContent;

        [TestInitialize]
        public void Initialize()
        {
            this.resourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorResource.txt");
        }

        [TestMethod]
        public void ShouldReturnArticleValidationResult()
        {
            var validator = new WeaponArticleValidator();

            var result = validator.Validate(this.resourceContent);

            Assert.IsNotNull(result);
        }
    }
}
