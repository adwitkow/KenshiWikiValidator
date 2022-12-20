using System;
using KenshiWikiValidator.Armours;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Tests.Armours
{
    [TestClass]
    public class ArmourPriceCalculatorTests
    {
        [TestMethod]
        public void ShouldCalculateForCoverageItems()
        {
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

            var armour = new Armour("string id", "name");
            armour.PartCoverage = new[] { new ItemReference<LocationalDamage>(new LocationalDamage("locational", "locational"), 100, 0, 0) };
            armour.Slot = Slot.Body;
            armour.Class = ArmourClass.Light;
            armour.MaterialType = MaterialType.Leather;
            armour.RelativePriceMult = 1;

            var actual = calculator.CalculatePrice(armour, ArmourGrade.Standard);

            Assert.AreEqual(2980, actual);
        }

        [TestMethod]
        public void ShouldCalculateForNoCoverageItems()
        {
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

            var armour = new Armour("string id", "name");
            armour.Slot = Slot.Shirt;
            armour.Class = ArmourClass.Cloth;
            armour.MaterialType = MaterialType.Cloth;
            armour.RelativePriceMult = 1;
            armour.Value = 100;

            var actual = calculator.CalculatePrice(armour, ArmourGrade.Standard);

            Assert.AreEqual(150, actual);
        }

        [TestMethod]
        public void ShouldCalculateHachiganePrice()
        {
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
        public void ShouldCalculateArmouredFacePlatesPrice()
        {
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
            var baseProvider = new BaseArmourValueProvider();
            var calculator = new ArmourPriceCalculator(baseProvider);

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
