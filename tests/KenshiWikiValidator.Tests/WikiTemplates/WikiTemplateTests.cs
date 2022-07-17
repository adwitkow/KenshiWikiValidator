using KenshiWikiValidator.BaseComponents;
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

        [TestMethod]
        public void ShouldBeEqualWithItselfAndOnlyName()
        {
            var template = new WikiTemplate("test", new SortedSet<string>(), new SortedList<string, string?>());

            Assert.IsTrue(template.Equals(template));
        }

        [TestMethod]
        public void ShouldNotBeEqualIfUnnamedParametersAreDifferent()
        {
            var template = new WikiTemplate("test", new SortedSet<string>()
            {
                "test"
            });
            var template2 = new WikiTemplate("test", new SortedSet<string>()
            {
                "test2"
            });

            Assert.IsFalse(template.Equals(template2));
        }

        [TestMethod]
        public void ShouldNotBeEqualIfParametersAreDifferent()
        {
            var template = new WikiTemplate("test", new SortedList<string, string?>()
            {
                { "test", "test1" },
            });
            var template2 = new WikiTemplate("test", new SortedList<string, string?>()
            {
                { "test", "test2" },
            });

            Assert.IsFalse(template.Equals(template2));
        }

        [TestMethod]
        public void ShouldNotBeEqualIfParameterCountsAreDifferent()
        {
            var template = new WikiTemplate("test", new SortedList<string, string?>()
            {
                { "test", "test1" },
            });
            var template2 = new WikiTemplate("test", new SortedList<string, string?>()
            {
                { "test", "test1" },
                { "test2", "test1" }
            });

            Assert.IsFalse(template.Equals(template2));
        }
    }
}
