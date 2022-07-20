using System.Linq;
using KenshiWikiValidator.OcsProxy.DialogueComponents;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

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

            var dataItem = new DataItem(ItemType.Weapon, 0, "weapon", "weaponid");
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

            var dataItem = new DataItem(ItemType.Weapon, 0, "weapon", "weaponid");
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

            var dataItem = new DataItem(ItemType.Town, 0, "town", "townid");
            var referenceCategory = new DataReferenceCategory("faction")
            {
                new DataReference("factionid", 0, 0, 0)
            };
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

            var dataItem = new DataItem(ItemType.Town, 0, "town", "townid");
            var referenceCategory = new DataReferenceCategory("faction")
            {
                new DataReference("factionid", 0, 0, 0)
            };
            dataItem.ReferenceCategories.Add(referenceCategory);

            mapper.Map(dataItem, builtItem);

            Assert.IsFalse(builtItem.Factions.Any());
        }

        [TestMethod]
        public void ShouldConvertFileValueObjectToString()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var mapper = new ItemMapper(repositoryMock.Object);

            var dataItem = new DataItem(ItemType.Weapon, 0, "weapon", "weaponid");
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

            var dataItem = new DataItem(ItemType.DialogueLine, 0, "line", "lineid");
            dataItem.Values["speaker"] = (int)DialogueSpeaker.Me;
            var builtItem = new DialogueLine("lineid", "line");

            mapper.Map(dataItem, builtItem);

            Assert.AreEqual(DialogueSpeaker.Me, builtItem.Speaker);
        }
    }
}
