using System;
using KenshiWikiValidator.Armours;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace KenshiWikiValidator.Tests.Armours
{
    public class ArmourPriceCalculatorTests
    {
        [Test]
        #region Test Cases: Armour Price Multiplier = 1.0f, Relative Price Multiplier = 1.0f
        #region Prototype Grade
        #region Body
        [TestCase(29, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(62, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(118, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(168, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(139, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(274, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(511, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(711, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(229, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(486, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(929, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(1328, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(159, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(299, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(549, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(748, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Legs
        [TestCase(14, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(31, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(59, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(84, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(69, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(137, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(255, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(355, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(114, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(243, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(464, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(664, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(79, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(149, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(274, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(374, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Shirt
        [TestCase(43, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(93, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(177, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(252, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(208, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(411, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(766, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(1066, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(343, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(729, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(1393, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(1992, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(238, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(448, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(823, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(1122, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Boots
        [TestCase(5, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(12, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(23, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(33, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(27, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(54, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(102, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(142, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(45, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(97, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(185, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(265, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(31, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(59, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(109, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(149, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Hat
        [TestCase(8, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(18, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(35, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(50, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(41, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(82, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(153, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(213, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(68, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(145, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(278, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(398, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(47, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(89, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(164, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(224, 1.0f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #endregion
        #endregion
        #region Test Cases: Armour Price Multiplier = 0.6f, Relative Price Multiplier = 1.0f
        #region Prototype Grade
        #region Body
        [TestCase(17, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(37, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(70, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(100, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(83, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(164, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(306, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(426, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(137, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(291, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(557, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(796, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(95, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(179, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(329, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(448, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Legs
        [TestCase(8, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(18, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(35, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(50, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(41, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(82, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(153, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(213, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(68, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(145, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(278, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(398, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(47, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(89, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(164, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(224, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Shirt
        [TestCase(25, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(55, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(105, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(150, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(124, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(246, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(459, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(639, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(205, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(436, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(835, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(1194, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(142, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(268, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(493, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(672, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Shirt, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Boots
        [TestCase(3, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(7, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(14, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(20, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(16, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(32, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(61, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(85, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(27, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(58, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(111, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(159, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(19, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(35, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(65, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(89, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Boots, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Hat
        [TestCase(5, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(11, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(21, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(30, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(24, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(49, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(91, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(127, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(41, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(87, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(167, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(238, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(28, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(53, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(98, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(134, 0.6f, 1.0f, ArmourGrade.Prototype, Slot.Hat, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #endregion
        #endregion
        #region Test Cases: Armour Price Multiplier = 0.6f, Relative Price Multiplier = 1.4f
        #region Prototype Grade
        #region Body
        [TestCase(23, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(51, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(98, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(140, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(116, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(229, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(428, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(596, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(191, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(407, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(779, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(1114, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(133, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(250, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(460, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(627, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Body, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #region Legs
        [TestCase(11, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Cloth)]
        [TestCase(25, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Leather)]
        [TestCase(49, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.Chain)]
        [TestCase(70, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Cloth, MaterialType.MetalPlate)]

        [TestCase(58, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Cloth)]
        [TestCase(114, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Leather)]
        [TestCase(214, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.Chain)]
        [TestCase(298, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Light, MaterialType.MetalPlate)]

        [TestCase(95, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Cloth)]
        [TestCase(203, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Leather)]
        [TestCase(389, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.Chain)]
        [TestCase(557, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Medium, MaterialType.MetalPlate)]

        [TestCase(66, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Cloth)]
        [TestCase(125, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Leather)]
        [TestCase(230, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.Chain)]
        [TestCase(313, 0.6f, 1.4f, ArmourGrade.Prototype, Slot.Legs, ArmourClass.Heavy, MaterialType.MetalPlate)]
        #endregion
        #endregion
        #endregion
        public void ShouldCalculatePrice(int expectedResult, float armourPriceMult, float relativePriceMult, ArmourGrade grade, Slot slot, ArmourClass armourClass, MaterialType material)
        {
            var calculator = new ArmourPriceCalculator();

            var armour = new Armour("string id", "imaginary armour");
            armour.Slot = slot;
            armour.Class = armourClass;
            armour.MaterialType = material;
            armour.RelativePriceMult = relativePriceMult;

            var result = calculator.CalculatePrice(armour, grade, armourPriceMult);

            NUnit.Framework.Assert.That(result, Is.EqualTo(expectedResult));
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
