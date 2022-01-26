using KenshiWikiValidator.Features.ArticleValidation.Validators.Rules;
using KenshiWikiValidator.Features.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace KenshiWikiValidator.Tests.Features.ArticleValidation.Validators.Rules
{
    [TestClass]
    public class NewLinesRuleTests
    {
        private string incorrectResourceContent;
        private string correctResourceContent;
        private TemplateParser templateParser;

        [TestInitialize]
        public void Initialize()
        {
            this.incorrectResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorIncorrectResource.txt");
            this.correctResourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorCorrectResource.txt");
            this.templateParser = new TemplateParser();
        }

        [TestMethod]
        public void ShouldCatchSingleLineTemplateSharingLineWithParagraph()
        {
            var rule = new NewLinesRule();
            var line = this.incorrectResourceContent.Split(Environment.NewLine).First();

            var result = rule.Execute("Wakizashi", line);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldCatchMultiLineTemplateSharingLineWithParagraph()
        {
            var rule = new NewLinesRule();
            var content = @"{{Weapon
|test = 1
|test2 = 3
}} And this is a test";

            var result = rule.Execute("Wakizashi", content);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedForCorrectSingleLineTemplate()
        {
            var rule = new NewLinesRule();
            var line = "{{Test}}";

            var result = rule.Execute("Wakizashi", line);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedOnSingleEmptyLine()
        {
            var rule = new NewLinesRule();
            var line = @"Text

And another line of text";

            var result = rule.Execute("Wakizashi", line);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedOnDoubleEmptyLine()
        {
            var rule = new NewLinesRule();
            var line = @"Text


And another line of text";

            var result = rule.Execute("Wakizashi", line);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedOnNoEmptyLine()
        {
            var rule = new NewLinesRule();
            var line = @"Text
And another line of text";

            var result = rule.Execute("Wakizashi", line);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedForIncorrectResource()
        {
            var rule = new NewLinesRule();
            var content = this.incorrectResourceContent;

            var result = rule.Execute("Wakizashi", content);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedForCorrectResource()
        {
            var rule = new NewLinesRule();
            var content = this.correctResourceContent;

            var result = rule.Execute("Wakizashi", content);

            Assert.IsTrue(result.Success);
        }
    }
}
