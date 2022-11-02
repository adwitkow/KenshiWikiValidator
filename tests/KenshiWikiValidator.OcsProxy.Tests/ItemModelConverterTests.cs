using System;
using System.Collections.Generic;
using System.Linq;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet.Data;
using OpenConstructionSet.Mods;

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

            var weaponDataItem = new ModItem(ItemType.Weapon, "name", "stringid");

            var convertedItem = converter.Convert(weaponDataItem);

            Assert.IsInstanceOfType(convertedItem, typeof(Weapon));
        }

        [TestMethod]
        public void ShouldConvertMultipleItems()
        {
            var repository = new Mock<ItemRepository>();
            var converter = new ItemModelConverter(repository.Object);

            var weaponDataItem = new ModItem(ItemType.Weapon, "name", "stringid");
            var containerDataItem = new ModItem(ItemType.Container, "name", "stringid");
            var armourDataItem = new ModItem(ItemType.Armour, "name", "stringid");

            var dataItems = new[] { weaponDataItem, containerDataItem, armourDataItem };

            var convertedPairs = converter.Convert(dataItems);

            foreach (var (Base, Result) in convertedPairs)
            {
                Assert.IsNotNull(Base);
                Assert.IsNotNull(Result);
            }
        }

        [TestMethod]
        public void ShouldBeAbleToConvertEveryKnownItemType()
        {
            var unknownItemTypes = new[]
            {
                ItemType.Location, ItemType.WarSavestate, ItemType.NullItem, ItemType.ZoneMap, ItemType.WorldmapCharacter,
                ItemType.CharacterAppearanceOld, ItemType.Techtree, ItemType.AiState, ItemType.InstanceCollection,
                ItemType.TemporaryInfo, ItemType.ModFilename, ItemType.Platoon, ItemType.GamestateBuilding,
                ItemType.GamestateCharacter, ItemType.GamestateFaction, ItemType.GamestateTownInstanceList, ItemType.State,
                ItemType.SavedState, ItemType.InventoryState, ItemType.InventoryItemState, ItemType.GamestateBuildingInterior,
                ItemType.LocationNode, ItemType.MedicalState, ItemType.MedicalPartState, ItemType.GamestateCrafting,
                ItemType.CharacterAppearance, ItemType.GamestateAi, ItemType.HumanCharacter, ItemType.AiSchedule, ItemType.Nest,
                ItemType.Blueprint, ItemType.ShopTraderClass, ItemType.GamestateTown, ItemType.Tutorial, ItemType.TerrainDecals,
                ItemType.Boat, ItemType.GamestateBoat, ItemType.BuildGrid, ItemType.BuildingShell, ItemType.ObjectTypeMax
            };

            var repository = new Mock<ItemRepository>();
            var converter = new ItemModelConverter(repository.Object);

            var dataItems = new List<ModItem>();
            var itemTypes = Enum.GetValues(typeof(ItemType))
                .Cast<ItemType>()
                .Except(unknownItemTypes);
            foreach (var type in itemTypes)
            {
                dataItems.Add(new ModItem(type, type.ToString(), type.ToString()));
            }

            var convertedPairs = converter.Convert(dataItems);

            foreach (var (Base, Result) in convertedPairs)
            {
                Assert.IsNotNull(Base);
                Assert.IsNotNull(Result);
            }
        }
    }
}