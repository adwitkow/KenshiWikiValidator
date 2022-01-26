using KenshiWikiValidator.Features.ArticleValidation.Validators;
using KenshiWikiValidator.Features.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var validator = new WeaponArticleValidator(this.templateParser);

            var result = validator.Validate(this.incorrectResourceContent);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldNotSucceedForIncorrectResource()
        {
            var validator = new WeaponArticleValidator(this.templateParser);

            var result = validator.Validate(this.incorrectResourceContent);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedForCorrectResource()
        {
            var validator = new WeaponArticleValidator(this.templateParser);

            var result = validator.Validate(this.correctResourceContent);

            Assert.IsTrue(result.Success);
        }
    }
}
