using KenshiWikiValidator.Features.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KenshiWikiValidator.Tests.Features.WikiTemplates
{
    [TestClass]
    public class WikiTemplateTests
    {
        [TestMethod]
        public void ConstructorMustThrowIfNameParameterIsNull()
        {
            string name = null!;
            var properties = new SortedList<string, string?>();

            var action = () => new WikiTemplate(name, properties);

            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void ConstructorMustThrowIfPropertiesParameterIsNull()
        {
            var name = string.Empty;
            SortedList<string, string?> properties = null!;

            var action = () => new WikiTemplate(name, properties);

            Assert.ThrowsException<ArgumentNullException>(action);
        }
    }
}
