using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.WikiCategories.SharedRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Tests.WikiCategories.SharedRules
{
    [TestClass]
    public class NewLinesRuleTests
    {
        [TestMethod]
        public void ShouldCatchSingleLineTemplateSharingLineWithParagraph()
        {
            var rule = new NewLinesRule();
            var line = "{{Template}}'''Text''' is a description that lorem ipsum why am still writing";

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
    }
}