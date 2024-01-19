using System;
using KenshiWikiValidator.Armours;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace KenshiWikiValidator.Tests.Armours
{
    [TestClass]
    public class ArmourPriceCalculatorTests
    {
        [Test]
        #region Test Cases: Armour Price Multiplier = 1.0f, Relative Price Multiplier = 1.0f
        #region Prototype Grade
        #region Body
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 29)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 62)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 118)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 168)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 139)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 274)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 511)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 711)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 229)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 486)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 929)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 1328)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 159)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 299)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 549)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 748)]
        #endregion
        #region Legs
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 14)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 31)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 59)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 84)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 69)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 137)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 255)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 355)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 114)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 243)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 464)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 664)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 79)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 149)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 274)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 374)]
        #endregion
        #region Shirt
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 43)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 93)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 177)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 252)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 208)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 411)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 766)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 1066)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 343)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 729)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 1393)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 1992)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 238)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 448)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 823)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 1122)]
        #endregion
        #region Boots
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 5)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 12)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 23)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 33)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 27)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 54)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 102)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 142)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 45)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 97)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 185)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 265)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 31)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 59)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 109)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 149)]
        #endregion
        #region Hat
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 8)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 18)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 35)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 50)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 41)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 82)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 153)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 213)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 68)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 145)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 278)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 398)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 47)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 89)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 164)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 224)]
        #endregion
        #endregion
        #region Shoddy Grade
        #region Body
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 179)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 247)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 395)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 443)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 1036)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 1391)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 2183)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 2375)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 1273)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 1783)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 2867)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 3251)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 1356)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 1791)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 2783)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 2975)]
        #endregion
        #region Legs
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 89)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 123)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 197)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 221)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 518)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 695)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 1091)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 1187)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 636)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 891)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 1433)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 1625)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 678)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 895)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 1391)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 1487)]
        #endregion
        #region Shirt
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 268)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 370)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 592)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 664)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 1554)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 2086)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 3274)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 3562)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 1909)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 2674)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 4300)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 4876)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 2034)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 2686)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 4174)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 4462)]
        #endregion
        #region Boots
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 35)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 49)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 79)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 88)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 207)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 278)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 436)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 475)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 254)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 356)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 573)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 650)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 271)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 358)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 556)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 595)]
        #endregion
        #region Hat
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 53)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 74)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 118)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 132)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 310)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 417)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 654)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 712)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 381)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 534)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 860)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 975)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 406)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 537)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 834)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Shoddy, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 892)]
        #endregion
        #endregion
        #region Standard Grade
        #region Body
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 656)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 841)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 1283)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 1325)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 3907)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 4967)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 7535)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 7703)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 4614)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 5935)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 9071)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 9407)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 5187)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 6567)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 9935)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 10103)]
        #endregion
        #region Legs
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 328)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 420)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 641)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 662)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 1953)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 2483)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 3767)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 3851)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 2307)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 2967)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 4535)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 4703)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 2593)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 3283)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 4967)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 5051)]
        #endregion
        #region Shirt
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 984)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 1261)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 1924)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 1987)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 5860)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 7450)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 11302)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 11554)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 6921)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 8902)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 13606)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 14110)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 7780)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 9850)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 14902)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 15154)]
        #endregion
        #region Boots
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 131)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 168)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 256)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 265)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 781)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 993)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 1507)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 1540)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 922)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 1187)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 1814)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 1881)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 1037)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 1313)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 1987)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 2020)]
        #endregion
        #region Hat
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 196)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 252)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 384)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 397)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 1172)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 1490)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 2260)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 2310)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 1384)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 1780)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 2721)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 2822)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 1556)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 1970)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 2980)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Standard, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 3030)]
        #endregion
        #endregion
        #region High Grade
        #region Body
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 1452)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 1831)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 2763)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 2795)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 8691)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 10927)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 16455)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 16583)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 10182)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 12855)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 19411)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 19667)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 11571)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 14527)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 21855)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 21983)]
        #endregion
        #region Legs
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 726)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 915)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 1381)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 1397)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 4345)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 5463)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 8227)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 8291)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 5091)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 6427)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 9705)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 9833)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 5785)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 7263)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 10927)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 10991)]
        #endregion
        #region Shirt
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 2178)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 2746)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 4144)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 4192)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 13036)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 16390)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 24682)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 24874)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 15273)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 19282)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 29116)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 29500)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 17356)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 21790)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 32782)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 32974)]
        #endregion
        #region Boots
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 290)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 366)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 552)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 559)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 1738)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 2185)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 3291)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 3316)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 2036)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 2571)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 3882)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 3933)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 2314)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 2905)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 4371)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 4396)]
        #endregion
        #region Hat
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 435)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 549)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 828)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 838)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 2607)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 3278)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 4936)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 4974)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 3054)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 3856)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 5823)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 5900)]

        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 3471)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 4358)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 6556)]
        [TestCase(1.0f, 1.0f, ArmourGrade.High, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 6594)]
        #endregion
        #endregion
        #region Specialist Grade
        #region Body
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 2567)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 3217)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 4835)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 4853)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 15388)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 19271)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 28943)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 29015)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 17977)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 22543)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 33887)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 34031)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 20508)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 25671)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 38543)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 38615)]
        #endregion
        #region Legs
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 1283)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 1608)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 2417)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 2426)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 7694)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 9635)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 14471)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 14507)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 8988)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 11271)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 16943)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 17015)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 10254)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 12835)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 19271)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 19307)]
        #endregion
        #region Shirt
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 3850)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 4825)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 7252)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 7279)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 23082)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 28906)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 43414)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 43522)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 26965)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 33814)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 50830)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 51046)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 30762)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 38506)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 57814)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 57922)]
        #endregion
        #region Boots
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 513)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 643)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 967)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 970)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 3077)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 3854)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 5788)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 5803)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 3595)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 4508)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 6777)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 6806)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 4101)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 5134)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 7708)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 7723)]
        #endregion
        #region Hat
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 770)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 965)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 1450)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 1455)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 4616)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 5781)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 8682)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 8704)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 5393)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 6762)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 10166)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 10209)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 6152)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 7701)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 11562)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Specialist, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 11584)]
        #endregion
        #endregion
        #region Masterwork Grade
        #region Body
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 3611)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 4517)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 6778)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 6783)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 21667)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 27094)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 40651)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 40671)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 25285)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 31626)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 47459)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 47498)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 28887)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 36119)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 54189)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 54208)]
        #endregion
        #region Legs
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 1805)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 2258)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 3389)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 3391)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 10833)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 13547)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 20325)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 20335)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 12642)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 15813)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 23729)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 23749)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 14443)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 18059)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 27094)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 27104)]
        #endregion
        #region Shirt
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 5416)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 6775)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 10167)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 10174)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 32500)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 40641)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 60976)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 61006)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 37927)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 47439)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 71188)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 71247)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 43330)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 54178)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 81283)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 81312)]
        #endregion
        #region Boots
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 722)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 903)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 1355)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 1356)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 4333)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 5418)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 8130)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 8134)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 5057)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 6325)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 9491)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 9499)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 5777)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 7223)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 10837)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 10841)]
        #endregion
        #region Hat
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 1083)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 1355)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 2033)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 2034)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 6500)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 8128)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 12195)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 12201)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 7585)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 9487)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 14237)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 14249)]

        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 8666)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 10835)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 16256)]
        [TestCase(1.0f, 1.0f, ArmourGrade.Masterwork, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 16262)]
        #endregion
        #endregion
        #endregion
        #region Test Cases: Armour Price Multiplier = 0.6f, Relative Price Multiplier = 1.0f
        #region Prototype Grade
        #region Body
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 17)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 37)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 70)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 100)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 83)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 164)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 306)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 426)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 137)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 291)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 557)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 796)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 95)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 179)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 329)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 448)]
        #endregion
        #region Legs
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 8)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 18)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 35)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 50)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 41)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 82)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 153)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 213)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 68)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 145)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 278)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 398)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 47)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 89)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 164)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 224)]
        #endregion
        #region Shirt
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 25)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 55)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 105)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 150)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 124)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 246)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 459)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 639)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 205)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 436)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 835)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 1194)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 142)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 268)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 493)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 672)]
        #endregion
        #region Boots
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 3)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 7)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 14)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 20)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 16)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 32)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 61)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 85)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 27)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 58)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 111)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 159)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 19)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 35)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 65)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 89)]
        #endregion
        #region Hat
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 5)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 11)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 21)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 30)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 24)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 49)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 91)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 127)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 41)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 87)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 167)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 238)]

        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 28)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 53)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 98)]
        [TestCase(0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 134)]
        #endregion
        #endregion
        #endregion
        #region Test Cases: Armour Price Multiplier = 0.6f, Relative Price Multiplier = 1.4f
        #region Prototype Grade
        #region Body
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 23)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 51)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 98)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 140)]

        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 116)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 229)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 428)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 596)]

        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 191)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 407)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 779)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 1114)]

        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 133)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 250)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 460)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 627)]
        //#endregion
        //#region Legs
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 11)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 25)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 49)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 70)]

        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 58)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 114)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 214)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 298)]

        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 95)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 203)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 389)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 557)]

        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 66)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 125)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 230)]
        //[TestCase(0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 313)]
        #endregion
        #endregion
        #endregion
        #region Test Cases: Armour Price Multiplier = 1.0f, Relative Price Multiplier = 1.4f
        #region Prototype Grade
        #region Body
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 40)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 86)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 165)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 235)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 194)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 383)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 715)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 995)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 320)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 680)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 1300)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 1859)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 222)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 418)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 768)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 1047)]
        //#endregion
        //#region Legs
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 20)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 43)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 82)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 117)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 97)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 191)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 357)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 497)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 160)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 340)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 650)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 929)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 111)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 209)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 384)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 523)]
        //#endregion
        //#region Shirt
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 60)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 129)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 247)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 352)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 291)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 574)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 1072)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 1492)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 480)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 1020)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 1950)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 2788)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 333)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 627)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 1152)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 1570)]
        //#endregion
        //#region Boots
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 8)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 17)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 33)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 47)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 38)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 76)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 143)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 199)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 64)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 136)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 260)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 371)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 44)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 83)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 153)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 209)]
        //#endregion
        //#region Hat
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth, ExpectedResult = 12)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather, ExpectedResult = 25)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain, ExpectedResult = 49)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate, ExpectedResult = 70)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Cloth, ExpectedResult = 58)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Leather, ExpectedResult = 114)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Chain, ExpectedResult = 214)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate, ExpectedResult = 298)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth, ExpectedResult = 96)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Leather, ExpectedResult = 204)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Chain, ExpectedResult = 390)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate, ExpectedResult = 557)]

        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth, ExpectedResult = 66)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather, ExpectedResult = 125)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain, ExpectedResult = 230)]
        //[TestCase(1.0f, 1.4f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate, ExpectedResult = 314)]
        #endregion
        #endregion
        #endregion
        public int ShouldCalculatePrice(float armourPriceMult, float relativePriceMult, ArmourGrade grade, Slot slot, ArmourClass armourClass, MaterialType material)
        {
            var calculator = new ArmourPriceCalculator();

            var armour = new Armour("string id", "imaginary armour");
            armour.Slot = slot;
            armour.Class = armourClass;
            armour.MaterialType = material;
            armour.RelativePriceMult = relativePriceMult;

            var result = calculator.CalculatePrice(armour, grade, armourPriceMult);

            return result;
        }

        [TestMethod]
        public void ShouldCalculateMasterworkHachiganePrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armour = new Armour("string id", "Hachigane");
            armour.PartCoverage = new[] { new ItemReference<LocationalDamage>(new LocationalDamage("locational", "locational"), 100, 0, 0) };
            armour.Slot = Slot.Hat;
            armour.Class = ArmourClass.Light;
            armour.MaterialType = MaterialType.MetalPlate;
            armour.RelativePriceMult = 0.5f;

            var actual = calculator.CalculatePrice(armour, ArmourGrade.Masterwork);

            Assert.AreEqual(3660, actual);
        }

        [TestMethod]
        public void ShouldCalculatePrototypeArmouredFacePlatesPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armour = new Armour("string id", "Armoured Face Plates");
            armour.PartCoverage = new[] { new ItemReference<LocationalDamage>(new LocationalDamage("locational", "locational"), 100, 0, 0) };
            armour.Slot = Slot.Hat;
            armour.Class = ArmourClass.Medium;
            armour.MaterialType = MaterialType.MetalPlate;
            armour.RelativePriceMult = 1.0f;
            var grade = ArmourGrade.Prototype;

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(238, actual);
        }

        [TestMethod]
        public void ShouldCalculatePrototypeBandanaPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Cloth;
            var grade = ArmourGrade.Prototype;
            var armour = CreateArmourWithCoverage("Bandana", 1, Slot.Hat, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(24, actual);
        }

        [TestMethod]
        public void ShouldCalculateShoddyBlackPlateJacketPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Shoddy;
            var armour = CreateArmourWithCoverage("Black Plate Jacket", 1.2f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(1282, actual);
        }

        [TestMethod]
        public void ShouldCalculatePrototypeBlackenedChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Prototype;
            var armour = CreateArmourWithCoverage("Blackened Chain Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(1168, actual);
        }

        [TestMethod]
        public void ShouldCalculateStandardBlackenedChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Standard;
            var armour = CreateArmourWithCoverage("Blackened Chain Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(11427, actual);
        }

        [TestMethod]
        public void ShouldCalculateHighBlackenedChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.High;
            var armour = CreateArmourWithCoverage("Blackened Chain Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(24456, actual);
        }

        [TestMethod]
        public void ShouldCalculateSpecialistBlackenedChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Specialist;
            var armour = CreateArmourWithCoverage("Blackened Chain Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(42696, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkBlackenedChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Blackened Chain Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(59797, actual);
        }

        [TestMethod]
        public void ShouldCalculatePrototypeBlackenedChainmailTagelmustPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Prototype;
            var armour = CreateArmourWithCoverage("Blackened Chainmail Tagelmust", 1.4f, Slot.Hat, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(233, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkBucketZukinPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Heavy;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Bucket Zukin", 1f, Slot.Hat, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(9753, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Chain Shirt", 1f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(42712, actual);
        }

        [TestMethod]
        public void ShouldCalculatePrototypeCrabArmourPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Heavy;
            var material = MaterialType.MetalPlate;
            var grade = ArmourGrade.Prototype;
            var armour = CreateArmourWithCoverage("Crab Armour", 0.8f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(358, actual);
        }

        [TestMethod]
        public void ShouldCalculateStandardCrabArmourPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Heavy;
            var material = MaterialType.MetalPlate;
            var grade = ArmourGrade.Standard;
            var armour = CreateArmourWithCoverage("Crab Armour", 0.8f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(4848, actual);
        }

        [TestMethod]
        public void ShouldCalculateHighDarkLeatherShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.High;
            var armour = CreateArmourWithCoverage("Dark Leather Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(13767, actual);
        }

        [TestMethod]
        public void ShouldCalculateSpecialistDarkLeatherShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Specialist;
            var armour = CreateArmourWithCoverage("Dark Leather Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(24279, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkDarkLeatherShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Dark Leather Shirt", 1.4f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(34137, actual);
        }

        [TestMethod]
        public void ShouldCalculatePrototypeHackStopperJacketPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Prototype;
            var armour = CreateArmourWithCoverage("Dark Leather Shirt", 0.7f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(203, actual);
        }

        [TestMethod]
        public void ShouldCalculateSpecialistHackStopperJacketPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Specialist;
            var armour = CreateArmourWithCoverage("Dark Leather Shirt", 0.7f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(9467, actual);
        }

        [TestMethod]
        public void ShouldCalculateHighAssassinsRagsPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.High;
            var armour = CreateArmourWithCoverage("Assassin's Rags", 1f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(6556, actual);
        }

        [TestMethod]
        public void ShouldCalculateSpecialistAssassinsRagsPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Specialist;
            var armour = CreateArmourWithCoverage("Assassin's Rags", 1f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(11562, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkAssassinsRagsPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Assassin's Rags", 1f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(16256, actual);
        }

        [TestMethod]
        public void ShouldCalculatePrototypeHiverChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Prototype;
            var armour = CreateArmourWithCoverage("Hiver Chain Shirt", 1f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(835, actual);
        }

        [TestMethod]
        public void ShouldCalculateStandardHiverChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Standard;
            var armour = CreateArmourWithCoverage("Hiver Chain Shirt", 1f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(8163, actual);
        }

        [TestMethod]
        public void ShouldCalculateSpecialistHiverChainShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Chain;
            var grade = ArmourGrade.Specialist;
            var armour = CreateArmourWithCoverage("Hiver Chain Shirt", 1f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(30498, actual);
        }

        [TestMethod]
        public void ShouldCalculateShoddyLeatherHiveVestPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Shoddy;
            var armour = CreateArmourWithCoverage("Leather Hive Vest", 0.6f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(750, actual);
        }

        [TestMethod]
        public void ShouldCalculateHighLeatherHiveVestPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.High;
            var armour = CreateArmourWithCoverage("Leather Hive Vest", 0.6f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(5899, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkLeatherHiveVestPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Leather Hive Vest", 0.6f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(14629, actual);
        }

        [TestMethod]
        public void ShouldCalculateHighLeatherShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.High;
            var armour = CreateArmourWithCoverage("Leather Shirt", 1.0f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(9834, actual);
        }

        [TestMethod]
        public void ShouldCalculateSpecialistLeatherShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Specialist;
            var armour = CreateArmourWithCoverage("Leather Shirt", 1.0f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(17343, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkLeatherShirtPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Light;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Leather Shirt", 1.0f, Slot.Shirt, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(24384, actual);
        }

        [TestMethod]
        public void ShouldCalculateMasterworkPlateJacketPrice()
        {
            var calculator = new ArmourPriceCalculator();

            var armourClass = ArmourClass.Medium;
            var material = MaterialType.Leather;
            var grade = ArmourGrade.Masterwork;
            var armour = CreateArmourWithCoverage("Plate Jacket", 1.1f, Slot.Body, material, armourClass);

            var actual = calculator.CalculatePrice(armour, grade);

            Assert.AreEqual(20872, actual);
        }

        private static Armour CreateArmourWithCoverage(string name, float multiplier, Slot slot, MaterialType material, ArmourClass armourClass)
        {
            var armour = CreateArmour(name, multiplier, slot, material, armourClass);
            armour.PartCoverage = new[] { new ItemReference<LocationalDamage>(new LocationalDamage("locational", "locational"), 100, 0, 0) };
            return armour;
        }

        private static Armour CreateArmour(string name, float multiplier, Slot slot, MaterialType material, ArmourClass armourClass)
        {
            var armour = new Armour("string id", name)
            {
                Slot = slot,
                Class = armourClass,
                MaterialType = material,
                RelativePriceMult = multiplier
            };
            return armour;
        }
    }
}
