using System.Linq;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

namespace KenshiWikiValidator.OcsProxy.Tests
{
    [TestClass]
    public class ItemMapperTests
    {
        [TestMethod]
        public void ShouldConvertExistingValue()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var mapper = new ItemMapper(repositoryMock.Object);

            var dataItem = new ModItem(ItemType.Weapon, "weapon", "weaponid");
            dataItem.Values.Add("attack mod", 5);
            var builtItem = new Weapon("weaponid", "weapon");

            mapper.Map(dataItem, builtItem);

            Assert.AreEqual(5, builtItem.AttackModifier);
        }

        [TestMethod]
        public void ShouldNotConvertNullValue()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var mapper = new ItemMapper(repositoryMock.Object);

            var dataItem = new ModItem(ItemType.Weapon, "weapon", "weaponid");
            var builtItem = new Weapon("weaponid", "weapon");

            mapper.Map(dataItem, builtItem);

            Assert.AreEqual(null, builtItem.AttackModifier);
        }

        [TestMethod]
        public void ShouldConvertExistingReference()
        {
            var builtItem = new Town("townid", "town");
            var builtFaction = new Faction("factionid", "faction");
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId(builtFaction.StringId))
                .Returns(builtFaction);

            var mapper = new ItemMapper(repositoryMock.Object);

            var dataItem = new ModItem(ItemType.Town, "town", "townid");
            var referenceCategory = new ModReferenceCategory("faction");
            referenceCategory.References.Add(new ModReference("factionid", 0, 0, 0));
            dataItem.ReferenceCategories.Add(referenceCategory);

            mapper.Map(dataItem, builtItem);

            Assert.AreSame(builtFaction, builtItem.Factions.First().Item);
        }

        [TestMethod]
        public void ShouldNotAddInvalidReferences()
        {
            var builtItem = new Town("townid", "town");
            var invalidFaction = new Weapon("factionid", "faction");
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId(invalidFaction.StringId))
                .Returns(invalidFaction);

            var mapper = new ItemMapper(repositoryMock.Object);

            var dataItem = new ModItem(ItemType.Town, "town", "townid");
            var referenceCategory = new ModReferenceCategory("faction");
            referenceCategory.References.Add(new ModReference("factionid", 0, 0, 0));
            dataItem.ReferenceCategories.Add(referenceCategory);

            mapper.Map(dataItem, builtItem);

            Assert.IsFalse(builtItem.Factions.Any());
        }

        [TestMethod]
        public void ShouldConvertFileValueObjectToString()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var mapper = new ItemMapper(repositoryMock.Object);

            var dataItem = new ModItem(ItemType.Weapon, "weapon", "weaponid");
            var fileValue = new FileValue("path");
            dataItem.Values["icon"] = fileValue;
            var builtItem = new Weapon("weaponid", "weapon");

            mapper.Map(dataItem, builtItem);

            Assert.AreEqual("path", builtItem.Icon);
        }

        [TestMethod]
        public void ShouldConvertEnumTypes()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var mapper = new ItemMapper(repositoryMock.Object);

            var dataItem = new ModItem(ItemType.DialogueLine, "line", "lineid");
            dataItem.Values["speaker"] = (int)DialogueSpeaker.Me;
            var builtItem = new DialogueLine("lineid", "line");

            mapper.Map(dataItem, builtItem);

            Assert.AreEqual(DialogueSpeaker.Me, builtItem.Speaker);
        }
    }
}
