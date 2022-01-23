using KenshiWikiValidator.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace KenshiWikiValidator.Tests.Validators
{
    [TestClass]
    public class WeaponArticleValidatorTests
    {
        private string resourceContent;
        private TemplateParser templateParser;

        [TestInitialize]
        public void Initialize()
        {
            this.resourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorIncorrectResource.txt");
            this.templateParser = new TemplateParser();
        }

        [TestMethod]
        public void ShouldReturnArticleValidationResult()
        {
            var validator = new WeaponArticleValidator(this.templateParser);

            var result = validator.Validate(this.resourceContent);

            Assert.IsNotNull(result);
        }
    }
}
