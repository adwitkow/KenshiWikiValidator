using KenshiWikiValidator.BaseComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KenshiWikiValidator.Tests.BaseComponents
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
        private const string PipedLinkTemplate = @"{{Town
| biome     = [[Bast (Zone)|Bast]]
| caption1  = The Ruin of Bast
| factions  = [[United Cities]]
| image1    = Bast.jpg
| map       = BastMap.png
| string id = 55655-rebirth.mod
| type      = Ruins
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

            Assert.AreEqual(10, result.Parameters.Count);

            Assert.AreEqual("Weapon", result.Name);
            Assert.IsTrue(result.Parameters.ContainsKey("class"));
            Assert.AreEqual("Katana", result.Parameters["class"]);
            Assert.IsTrue(result.Parameters.ContainsKey("blood loss"));
            Assert.AreEqual("1.20", result.Parameters["blood loss"]);
            Assert.IsTrue(result.Parameters.ContainsKey("indoors"));
            Assert.AreEqual("+4", result.Parameters["indoors"]);
            Assert.IsTrue(result.Parameters.ContainsKey("damage_robots"));
            Assert.AreEqual("-40", result.Parameters["damage_robots"]);
            Assert.IsTrue(result.Parameters.ContainsKey("damage_humans"));
            Assert.AreEqual("+10", result.Parameters["damage_humans"]);
            Assert.IsTrue(result.Parameters.ContainsKey("reach"));
            Assert.AreEqual("16", result.Parameters["reach"]);
            Assert.IsTrue(result.Parameters.ContainsKey("description"));
            Assert.AreEqual(description, result.Parameters["description"]);
            Assert.IsTrue(result.Parameters.ContainsKey("armour penetration"));
            Assert.AreEqual("-30", result.Parameters["armour penetration"]);
            Assert.IsTrue(result.Parameters.ContainsKey("attack"));
            Assert.AreEqual("+2", result.Parameters["attack"]);
            Assert.IsTrue(result.Parameters.ContainsKey("defence"));
            Assert.AreEqual("-2", result.Parameters["defence"]);
        }

        [TestMethod]
        public void ParserShouldHandleMultiLineTemplates()
        {
            var description = "Smaller and lighter, usually used as a sidearm for backup.  It's worth noting that although short, it has a major advantage when fighting indoors against longer weapons.";
            var parser = new TemplateParser();

            var result = parser.Parse(MultiLineTemplate);

            Assert.AreEqual(10, result.Parameters.Count);

            Assert.AreEqual("Weapon", result.Name);
            Assert.IsTrue(result.Parameters.ContainsKey("class"));
            Assert.AreEqual("Katana", result.Parameters["class"]);
            Assert.IsTrue(result.Parameters.ContainsKey("blood loss"));
            Assert.AreEqual("1.20", result.Parameters["blood loss"]);
            Assert.IsTrue(result.Parameters.ContainsKey("indoors"));
            Assert.AreEqual("+4", result.Parameters["indoors"]);
            Assert.IsTrue(result.Parameters.ContainsKey("damage_robots"));
            Assert.AreEqual("-40", result.Parameters["damage_robots"]);
            Assert.IsTrue(result.Parameters.ContainsKey("damage_humans"));
            Assert.AreEqual("+10", result.Parameters["damage_humans"]);
            Assert.IsTrue(result.Parameters.ContainsKey("reach"));
            Assert.AreEqual("16", result.Parameters["reach"]);
            Assert.IsTrue(result.Parameters.ContainsKey("description"));
            Assert.AreEqual(description, result.Parameters["description"]);
            Assert.IsTrue(result.Parameters.ContainsKey("armour penetration"));
            Assert.AreEqual("-30", result.Parameters["armour penetration"]);
            Assert.IsTrue(result.Parameters.ContainsKey("attack"));
            Assert.AreEqual("+2", result.Parameters["attack"]);
            Assert.IsTrue(result.Parameters.ContainsKey("defence"));
            Assert.AreEqual("-2", result.Parameters["defence"]);
        }

        [TestMethod]
        public void ShouldHandlePipedLinks()
        {
            var parser = new TemplateParser();

            var result = parser.Parse(PipedLinkTemplate);

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Parameters.Count);
            Assert.AreEqual("[[Bast (Zone)|Bast]]", result.Parameters["biome"]);
        }

        [TestMethod]
        public void ShouldHandleMultipleNonPipedLinks()
        {
            var input = @"{{Town|title1 = Secret Drug Farm|image1 = Secret Drug BAse.jpg|biome = [[Stobe's Gamble]]|factions = [[Blackshifters]]|map = Secret Drug Base -Map-.png|dangers = [[Security Spider II]]|resources = [[Hashish]]}}";
            var parser = new TemplateParser();

            var result = parser.Parse(input);

            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Parameters.Count);
            Assert.AreEqual("[[Stobe's Gamble]]", result.Parameters["biome"]);
            Assert.AreEqual("Secret Drug Farm", result.Parameters["title1"]);
            Assert.AreEqual("Secret Drug BAse.jpg", result.Parameters["image1"]);
            Assert.AreEqual("[[Blackshifters]]", result.Parameters["factions"]);
            Assert.AreEqual("Secret Drug Base -Map-.png", result.Parameters["map"]);
            Assert.AreEqual("[[Security Spider II]]", result.Parameters["dangers"]);
            Assert.AreEqual("[[Hashish]]", result.Parameters["resources"]);
        }
    }
}
