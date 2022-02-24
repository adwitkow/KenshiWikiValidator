using KenshiWikiValidator.Features.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Tests.Features.WikiTemplates
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
        public void ShouldReturnCorrectlyFormattedTemplate()
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
| color = green
| description = A shorter variant of the Katana, they are fast and sharp but generally used as backup weapons.
| level = 2
| name = Wakizashis
| new items = [[Wakizashi]]
| prerequisites = some prereqs
| sell value = 1250
| value = 5000
| vendors = i dunno
}}";
            Assert.AreEqual(expected, result);
        }
    }
}
