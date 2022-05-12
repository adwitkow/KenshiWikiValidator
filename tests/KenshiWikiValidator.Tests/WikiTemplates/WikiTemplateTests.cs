using KenshiWikiValidator.WikiTemplates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KenshiWikiValidator.Tests.WikiTemplates
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
        public void ConstructorMustThrowIfParametersParameterIsNull()
        {
            var name = string.Empty;
            SortedList<string, string?> properties = null!;

            var action = () => new WikiTemplate(name, properties);

            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void ConstructorMustThrowIfUnnamedParametersParameterIsNull()
        {
            var name = string.Empty;
            SortedSet<string> unnamedParameters = null!;

            var action = () => new WikiTemplate(name, unnamedParameters);

            Assert.ThrowsException<ArgumentNullException>(action);
        }
    }
}
