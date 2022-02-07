using KenshiWikiValidator.Features.ArticleValidation.Shared;
using KenshiWikiValidator.Features.ArticleValidation.Shared.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace KenshiWikiValidator.Tests.Features.ArticleValidation.Validators.Rules
{
    [TestClass]
    public class NewLinesRuleTests
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
        public void ShouldCatchSingleLineTemplateSharingLineWithParagraph()
        {
            var rule = new NewLinesRule();
            var line = this.incorrectResourceContent!.Split(Environment.NewLine).First();

            var result = rule.Execute("Wakizashi", line, new ArticleData());

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

            var result = rule.Execute("Wakizashi", content, new ArticleData());

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedForCorrectSingleLineTemplate()
        {
            var rule = new NewLinesRule();
            var line = "{{Test}}";

            var result = rule.Execute("Wakizashi", line, new ArticleData());

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedOnSingleEmptyLine()
        {
            var rule = new NewLinesRule();
            var line = @"Text

And another line of text";

            var result = rule.Execute("Wakizashi", line, new ArticleData());

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedOnDoubleEmptyLine()
        {
            var rule = new NewLinesRule();
            var line = @"Text


And another line of text";

            var result = rule.Execute("Wakizashi", line, new ArticleData());

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedOnNoEmptyLine()
        {
            var rule = new NewLinesRule();
            var line = @"Text
And another line of text";

            var result = rule.Execute("Wakizashi", line, new ArticleData());

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedOnNoNewlineBetweenListAndSection()
        {
            var rule = new NewLinesRule();
            var line = @"* item1
* item2
== section ==
* item 3
* item 4";

            var result = rule.Execute("Wakizashi", line, new ArticleData());

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedOnNewLineMissingAfterLink()
        {
            var rule = new NewLinesRule();
            var line = @"[[item2]]
== section ==";

            var result = rule.Execute("Wakizashi", line, new ArticleData());

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldNotSucceedForIncorrectResource()
        {
            var rule = new NewLinesRule();
            var content = this.incorrectResourceContent;

            var result = rule.Execute("Wakizashi", content!, new ArticleData());

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedForCorrectResource()
        {
            var rule = new NewLinesRule();
            var content = this.correctResourceContent;

            var result = rule.Execute("Wakizashi", content!, new ArticleData());

            Assert.IsTrue(result.Success);
        }
    }
}
