using KenshiWikiValidator.Features.WikiTemplates.Creators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Tests.Features.WikiTemplates.Creators
{
    [TestClass]
    public class CraftingWikiTemplateCreatorTests
    {
        [TestMethod]
        public void ContainsAllKeys()
        {
            var creator = new CraftingTemplateCreator();

            var template = creator.Generate();

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

            var template = creator.Generate();

            Assert.AreEqual("building name", template.Parameters["building"]);
        }

        [TestMethod]
        public void Input1IsNotAvailableInResultIfItsNotSpecified()
        {
            var creator = new CraftingTemplateCreator()
            {
                Input1 = ("input", 1)
            };

            var template = creator.Generate();

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

            var template = creator.Generate();

            Assert.AreEqual("test", template.Parameters["output"]);
        }
    }
}
