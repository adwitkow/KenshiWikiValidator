using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Tests
{
    [TestClass]
    public class WikiTemplateTests
    {
        [TestMethod]
        public void ConstructorMustThrowIfNameParameterIsNull()
        {
            string name = null!;
            var properties = new SortedList<string, string>();

            var action = () => new WikiTemplate(name, properties);

            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void ConstructorMustThrowIfPropertiesParameterIsNull()
        {
            var name = string.Empty;
            SortedList<string, string> properties = null!;

            var action = () => new WikiTemplate(name, properties);

            Assert.ThrowsException<ArgumentNullException>(action);
        }
    }
}
