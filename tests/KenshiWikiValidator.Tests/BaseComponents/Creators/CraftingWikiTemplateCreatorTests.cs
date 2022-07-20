using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.BaseComponents.Creators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Tests.WikiTemplates.Creators
{
    [TestClass]
    public class CraftingWikiTemplateCreatorTests
    {
        [TestMethod]
        public void ContainsAllKeys()
        {
            var creator = new CraftingTemplateCreator();

            var template = creator.Generate(new ArticleData());

            Assert.IsTrue(template.Parameters.ContainsKey("building"));
            Assert.IsTrue(template.Parameters.ContainsKey("input0"));
            Assert.IsTrue(template.Parameters.ContainsKey("input0amount"));
            Assert.IsTrue(template.Parameters.ContainsKey("input1"));
            Assert.IsTrue(template.Parameters.ContainsKey("input1amount"));
            Assert.IsTrue(template.Parameters.ContainsKey("output"));
        }

        [TestMethod]
        public void BuldingNameMustBeGenerated()
        {
            var creator = new CraftingTemplateCreator()
            {
                BuildingName = "building name"
            };

            var template = creator.Generate(new ArticleData());

            Assert.AreEqual("building name", template.Parameters["building"]);
        }

        [TestMethod]
        public void Input1IsNotAvailableInResultIfItsNotSpecified()
        {
            var creator = new CraftingTemplateCreator()
            {
                Input1 = ("input", 1)
            };

            var template = creator.Generate(new ArticleData());

            Assert.AreEqual(null, template.Parameters["input1"]);
            Assert.AreEqual(null, template.Parameters["input1amount"]);
        }

        [TestMethod]
        public void OutputMustBeGenerated()
        {
            var creator = new CraftingTemplateCreator()
            {
                Output = "test"
            };

            var template = creator.Generate(new ArticleData());

            Assert.AreEqual("test", template.Parameters["output"]);
        }
    }
}
