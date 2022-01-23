using KenshiWikiValidator.Validators.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Tests.Validators.Rules
{
    [TestClass]
    public class NewLinesRuleTests
    {
        private string resourceContent;
        private TemplateParser templateParser;

        [TestInitialize]
        public void Initialize()
        {
            this.resourceContent = File.ReadAllText(@"TestResources\WeaponArticleValidatorResource.txt");
            this.templateParser = new TemplateParser();
        }

        [TestMethod]
        public void ShouldCatchSingleLineTemplateSharingLineWithParagraph()
        {
            var rule = new NewLinesRule();
            var line = this.resourceContent.Split(Environment.NewLine).First();

            var result = rule.Execute(line);

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

            var result = rule.Execute(content);

            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ShouldSucceedForCorrectSingleLineTemplate()
        {
            var rule = new NewLinesRule();
            var line = "{{Test}}";

            var result = rule.Execute(line);

            Assert.IsTrue(result.Success);
        }
    }
}
