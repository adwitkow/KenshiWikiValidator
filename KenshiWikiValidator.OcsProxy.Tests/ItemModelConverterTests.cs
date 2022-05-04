using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Tests
{
    [TestClass]
    public class ItemModelConverterTests
    {
        [TestMethod]
        public void ShouldConvertWeaponDataItemToWeapon()
        {
            var repository = new Mock<ItemRepository>();
            var converter = new ItemModelConverter(repository.Object);

            var weaponDataItem = new DataItem(ItemType.Weapon, 0, "name", "stringid");

            var convertedItem = converter.Convert(weaponDataItem);

            Assert.IsInstanceOfType(convertedItem, typeof(Weapon));
        }

        [TestMethod]
        public void ShouldConvertMultipleItems()
        {
            var repository = new Mock<ItemRepository>();
            var converter = new ItemModelConverter(repository.Object);

            var weaponDataItem = new DataItem(ItemType.Weapon, 0, "name", "stringid");
            var containerDataItem = new DataItem(ItemType.Container, 0, "name", "stringid");
            var armourDataItem = new DataItem(ItemType.Armour, 0, "name", "stringid");

            var dataItems = new[] { weaponDataItem, containerDataItem, armourDataItem };

            var convertedPairs = converter.Convert(dataItems);

            foreach (var pair in convertedPairs)
            {
                Assert.IsNotNull(pair.Base);
                Assert.IsNotNull(pair.Result);
            }
        }
    }
}