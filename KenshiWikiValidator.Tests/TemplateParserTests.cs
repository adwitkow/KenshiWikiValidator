using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Tests
{
    [TestClass]
    public class TemplateParserTests
    {
        private const string SingleLineTemplate = "{{Weapon|class = Katana|blood loss = 1.20|indoors = +4|damage_robots = -40|damage_humans = +10|reach = 16|description = Smaller and lighter, usually used as a sidearm for backup.  It's worth noting that although short, it has a major advantage when fighting indoors against longer weapons.|armour penetration = -30|attack = +2|defence = -2}}";
        private const string MultiLineTemplate = @"{{Weapon
|class = Katana
 | blood loss=1.20
|indoors = +4
|damage_robots = -40
|damage_humans= +10
|reach = 16
|description = Smaller and lighter, usually used as a sidearm for backup.  It's worth noting that although short, it has a major advantage when fighting indoors against longer weapons.
| armour penetration = -30
|attack = +2|defence = -2
}}";

        [TestMethod]
        public void MustBeNotNullIfHasAllCorrectFormat()
        {
            var parser = new TemplateParser();

            var result = parser.Parse(SingleLineTemplate);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShouldThrowIfDoesNotStartWithBraces()
        {
            var parser = new TemplateParser();

            var action = () => parser.Parse("TEST}}");

            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void ShouldThrowIfDoesNotEndWithBraces()
        {
            var parser = new TemplateParser();

            var action = () => parser.Parse("{{TEST");

            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void ShouldThrowIfHasOnlyBraces()
        {
            var parser = new TemplateParser();

            var action = () => parser.Parse("{{}}");

            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void ParserShouldHandleSingleLineTemplates()
        {
            var description = "Smaller and lighter, usually used as a sidearm for backup.  It's worth noting that although short, it has a major advantage when fighting indoors against longer weapons.";
            var parser = new TemplateParser();

            var result = parser.Parse(SingleLineTemplate);

            Assert.AreEqual(10, result.Properties.Count);

            Assert.AreEqual("Weapon", result.Name);
            Assert.IsTrue(result.Properties.ContainsKey("class"));
            Assert.AreEqual("Katana", result.Properties["class"]);
            Assert.IsTrue(result.Properties.ContainsKey("blood loss"));
            Assert.AreEqual("1.20", result.Properties["blood loss"]);
            Assert.IsTrue(result.Properties.ContainsKey("indoors"));
            Assert.AreEqual("+4", result.Properties["indoors"]);
            Assert.IsTrue(result.Properties.ContainsKey("damage_robots"));
            Assert.AreEqual("-40", result.Properties["damage_robots"]);
            Assert.IsTrue(result.Properties.ContainsKey("damage_humans"));
            Assert.AreEqual("+10", result.Properties["damage_humans"]);
            Assert.IsTrue(result.Properties.ContainsKey("reach"));
            Assert.AreEqual("16", result.Properties["reach"]);
            Assert.IsTrue(result.Properties.ContainsKey("description"));
            Assert.AreEqual(description, result.Properties["description"]);
            Assert.IsTrue(result.Properties.ContainsKey("armour penetration"));
            Assert.AreEqual("-30", result.Properties["armour penetration"]);
            Assert.IsTrue(result.Properties.ContainsKey("attack"));
            Assert.AreEqual("+2", result.Properties["attack"]);
            Assert.IsTrue(result.Properties.ContainsKey("defence"));
            Assert.AreEqual("-2", result.Properties["defence"]);
        }

        [TestMethod]
        public void ParserShouldHandleMultiLineTemplates()
        {
            var description = "Smaller and lighter, usually used as a sidearm for backup.  It's worth noting that although short, it has a major advantage when fighting indoors against longer weapons.";
            var parser = new TemplateParser();

            var result = parser.Parse(MultiLineTemplate);

            Assert.AreEqual(10, result.Properties.Count);

            Assert.AreEqual("Weapon", result.Name);
            Assert.IsTrue(result.Properties.ContainsKey("class"));
            Assert.AreEqual("Katana", result.Properties["class"]);
            Assert.IsTrue(result.Properties.ContainsKey("blood loss"));
            Assert.AreEqual("1.20", result.Properties["blood loss"]);
            Assert.IsTrue(result.Properties.ContainsKey("indoors"));
            Assert.AreEqual("+4", result.Properties["indoors"]);
            Assert.IsTrue(result.Properties.ContainsKey("damage_robots"));
            Assert.AreEqual("-40", result.Properties["damage_robots"]);
            Assert.IsTrue(result.Properties.ContainsKey("damage_humans"));
            Assert.AreEqual("+10", result.Properties["damage_humans"]);
            Assert.IsTrue(result.Properties.ContainsKey("reach"));
            Assert.AreEqual("16", result.Properties["reach"]);
            Assert.IsTrue(result.Properties.ContainsKey("description"));
            Assert.AreEqual(description, result.Properties["description"]);
            Assert.IsTrue(result.Properties.ContainsKey("armour penetration"));
            Assert.AreEqual("-30", result.Properties["armour penetration"]);
            Assert.IsTrue(result.Properties.ContainsKey("attack"));
            Assert.AreEqual("+2", result.Properties["attack"]);
            Assert.IsTrue(result.Properties.ContainsKey("defence"));
            Assert.AreEqual("-2", result.Properties["defence"]);
        }
    }
}
