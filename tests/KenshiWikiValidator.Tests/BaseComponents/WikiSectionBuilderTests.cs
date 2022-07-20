using KenshiWikiValidator.BaseComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KenshiWikiValidator.Tests.BaseComponents
{
    [TestClass]
    public class WikiSectionBuilderTests
    {
        [TestMethod]
        public void BuildShouldReturnNonNull()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test");

            var result = builder.Build();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BuildShoulThrowIfHeaderIsEmpty()
        {
            var builder = new WikiSectionBuilder();

            var resultAction = () => builder.Build();

            Assert.ThrowsException<InvalidOperationException>(resultAction);
        }

        [TestMethod]
        public void WithNewlineShouldAddEmptyString()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithNewline();

            var result = builder.Build();

            Assert.IsTrue(result.EndsWith(Environment.NewLine));
        }

        [TestMethod]
        public void WithTemplateShouldCreateProperTemplate()
        {
            var templateProperties = new SortedList<string, string?>()
            {
                { "prop1", "val1" },
                { "prop2", "val2" },
            };
            var template = new WikiTemplate("test", templateProperties);
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithTemplate(template);

            var result = builder.Build();

            var expected = @"== test ==
{{test
| prop1 = val1
| prop2 = val2
}}";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithParagraphShouldAddTextAndNewline()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithParagraph("paragraph 1")
                .WithParagraph("paragraph 2");

            var result = builder.Build();

            var expected = @"== test ==
paragraph 1

paragraph 2
";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithUnorderedListShouldCreateLinesPrefixedWithAsterisks()
        {
            var list = new string[] { "element 1", "element 2", "element 3" };

            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithUnorderedList(list);

            var result = builder.Build();

            var expected = @"== test ==
* element 1
* element 2
* element 3";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithSubsectionShouldThrowOnLevelTooLow()
        {
            var builder = new WikiSectionBuilder();
            var action = () => builder
                .WithHeader("test")
                .WithSubsection("subsection", 0);

            Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        }

        [TestMethod]
        public void WithSubsectionShouldThrowOnLevelTooHigh()
        {
            var builder = new WikiSectionBuilder();
            var action = () => builder
                .WithHeader("test")
                .WithSubsection("subsection", 5);

            Assert.ThrowsException<ArgumentOutOfRangeException>(action);
        }

        [TestMethod]
        public void WithSubsection1ShouldCreateSubsectionSubheader()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithSubsection("subsection", 1);

            var result = builder.Build();

            var expected = @"== test ==
=== subsection ===";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithSubsection2ShouldCreateSubsectionSubheader()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithSubsection("subsection", 2);

            var result = builder.Build();

            var expected = @"== test ==
==== subsection ====";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithSubsection3ShouldCreateSubsectionSubheader()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithSubsection("subsection", 3);

            var result = builder.Build();

            var expected = @"== test ==
===== subsection =====";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithSubsection4ShouldCreateSubsectionSubheader()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithSubsection("subsection", 4);

            var result = builder.Build();

            var expected = @"== test ==
====== subsection ======";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void WithLineShouldNotAddNewLine()
        {
            var builder = new WikiSectionBuilder();
            builder.WithHeader("test")
                .WithLine("line");

            var result = builder.Build();

            Assert.IsFalse(result.EndsWith(Environment.NewLine));
        }
    }
}
