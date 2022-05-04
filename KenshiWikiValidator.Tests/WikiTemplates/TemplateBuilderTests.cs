using KenshiWikiValidator.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KenshiWikiValidator.Tests.WikiTemplates
{
    [TestClass]
    public class TemplateBuilderTests
    {
        [TestMethod]
        public void ShouldThrowExceptionForInvalidTemplate()
        {
            var builder = new TemplateBuilder();
            var template = new WikiTemplate(string.Empty, new SortedList<string, string?>());

            var action = () => builder.Build(template);

            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void TemplateWithoutParametersShouldBeSingleLine()
        {
            var builder = new TemplateBuilder();
            var template = new WikiTemplate("Navbox/Weapons");

            var result = builder.Build(template);

            var expected = "{{Navbox/Weapons}}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldReturnCorrectlyFormattedTemplateWithOnlyNamedParameters()
        {
            var builder = new TemplateBuilder();
            var templateName = "Blueprint";
            var templateValues = new SortedList<string, string?>()
            {
                { "name", "Wakizashis" },
                { "color", "green" },
                { "description", "A shorter variant of the Katana, they are fast and sharp but generally used as backup weapons." },
                { "level", "2" },
                { "value", "5000" },
                { "sell value", "1250" },
                { "prerequisites", "some prereqs" },
                { "new items", "[[Wakizashi]]" },
                { "vendors", "i dunno" },
            };
            var template = new WikiTemplate(templateName, templateValues);

            var result = builder.Build(template);

            var expected = @"{{Blueprint
| color         = green
| description   = A shorter variant of the Katana, they are fast and sharp but generally used as backup weapons.
| level         = 2
| name          = Wakizashis
| new items     = [[Wakizashi]]
| prerequisites = some prereqs
| sell value    = 1250
| value         = 5000
| vendors       = i dunno
}}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldHandleUnnamedParametersCorrectly()
        {
            var builder = new TemplateBuilder();
            var templateName = "TestName";
            var unnamedParameters = new SortedSet<string>() { "parameter 1", "parameter 2", "parameter 3" };
            var template = new WikiTemplate(templateName, unnamedParameters);

            var result = builder.Build(template);

            var expected = @"{{TestName
| parameter 1
| parameter 2
| parameter 3
}}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldHandleNamedAndUnnamedParametersCorrectly()
        {
            var builder = new TemplateBuilder();
            var templateName = "TestName";
            var unnamedParameters = new SortedSet<string>() { "parameter 1", "parameter 2", "parameter 3" };
            var namedParameters = new SortedList<string, string?>()
            {
                { "name", "Wakizashis" },
                { "color", "green" },
                { "description", "A shorter variant of the Katana, they are fast and sharp but generally used as backup weapons." },
                { "level", "2" },
                { "value", "5000" },
                { "sell value", "1250" },
                { "prerequisites", "some prereqs" },
                { "new items", "[[Wakizashi]]" },
                { "vendors", "i dunno" },
            };
            var template = new WikiTemplate(templateName, unnamedParameters, namedParameters);

            var result = builder.Build(template);

            var expected = @"{{TestName
| parameter 1
| parameter 2
| parameter 3
| color         = green
| description   = A shorter variant of the Katana, they are fast and sharp but generally used as backup weapons.
| level         = 2
| name          = Wakizashis
| new items     = [[Wakizashi]]
| prerequisites = some prereqs
| sell value    = 1250
| value         = 5000
| vendors       = i dunno
}}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ShouldPutSingleUnnamedParameterOnSameLineAsName()
        {
            var builder = new TemplateBuilder();
            var templateName = "TestName";
            var unnamedParameters = new SortedSet<string>() { "parameter 1" };
            var namedParameters = new SortedList<string, string?>()
            {
                { "name", "Wakizashis" },
                { "color", "green" },
                { "description", "A shorter variant of the Katana, they are fast and sharp but generally used as backup weapons." },
                { "level", "2" },
                { "value", "5000" },
                { "sell value", "1250" },
                { "prerequisites", "some prereqs" },
                { "new items", "[[Wakizashi]]" },
                { "vendors", "i dunno" },
            };
            var template = new WikiTemplate(templateName, unnamedParameters, namedParameters);

            var result = builder.Build(template);

            var expected = @"{{TestName | parameter 1
| color         = green
| description   = A shorter variant of the Katana, they are fast and sharp but generally used as backup weapons.
| level         = 2
| name          = Wakizashis
| new items     = [[Wakizashi]]
| prerequisites = some prereqs
| sell value    = 1250
| value         = 5000
| vendors       = i dunno
}}";
            Assert.AreEqual(expected, result);
        }
    }
}
