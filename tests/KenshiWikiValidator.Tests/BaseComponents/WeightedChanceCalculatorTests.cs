using KenshiWikiValidator.BaseComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Tests.BaseComponents
{
    [TestClass]
    public class WeightedChanceCalculatorTests
    {
        [TestMethod]
        public void ShouldReturnOneIfTheResultIsCertain()
        {
            var weightedChanceCalculator = new WeightedChanceCalculator();
            var tries = 1;
            var successes = 1;
            var chance = 1;

            var result = weightedChanceCalculator.Calculate(tries, successes, chance);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ShouldReturn0Dot375ForTwoTriesOf0Dot25Chance()
        {
            var weightedChanceCalculator = new WeightedChanceCalculator();
            var tries = 2;
            var successes = 1;
            var chance = 0.25;

            var result = weightedChanceCalculator.Calculate(tries, successes, chance);

            Assert.AreEqual(0.375, result);
        }

        [TestMethod]
        public void ShouldReturn0Dot421875ForTwoSuccessesOfThreeTriesWithChance0Dot75()
        {
            var weightedChanceCalculator = new WeightedChanceCalculator();
            var tries = 3;
            var successes = 2;
            var chance = 0.75;

            var result = weightedChanceCalculator.Calculate(tries, successes, chance);

            Assert.AreEqual(0.421875, result);
        }
    }
}
