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

            Assert.AreEqual(2980, (int)actual);
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

            Assert.AreEqual(150, (int)actual);
        }
    }
}
