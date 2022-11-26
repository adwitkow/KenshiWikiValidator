using KenshiWikiValidator.Armours;
using KenshiWikiValidator.OcsProxy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Tests.Armours
{
    [TestClass]
    public class BaseArmourValueProviderTests
    {
        [TestMethod]
        public void PrototypeGradeAndClothMaterialAndHeavyClassShouldReturn159()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Prototype);

            Assert.AreEqual(159, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndLeatherMaterialAndHeavyClassShouldReturn299()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Prototype);

            Assert.AreEqual(299, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndChainMaterialAndHeavyClassShouldReturn549()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Prototype);

            Assert.AreEqual(549, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndMetalPlateMaterialAndHeavyClassShouldReturn748()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Prototype);

            Assert.AreEqual(748, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndClothMaterialAndMediumClassShouldReturn229()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Prototype);

            Assert.AreEqual(229, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndLeatherMaterialAndMediumClassShouldReturn486()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Prototype);

            Assert.AreEqual(486, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndChainMaterialAndMediumClassShouldReturn929()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Prototype);

            Assert.AreEqual(929, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndMetalPlateMaterialAndMediumClassShouldReturn1328()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Prototype);

            Assert.AreEqual(1328, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndClothMaterialAndLightClassShouldReturn139()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Prototype);

            Assert.AreEqual(139, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndLeatherMaterialAndLightClassShouldReturn274()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Leather, ArmourGrade.Prototype);

            Assert.AreEqual(274, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndChainMaterialAndLightClassShouldReturn511()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Chain, ArmourGrade.Prototype);

            Assert.AreEqual(511, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndMetalPlateMaterialAndLightClassShouldReturn711()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Prototype);

            Assert.AreEqual(711, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndClothMaterialAndClothClassShouldReturn29()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Prototype);

            Assert.AreEqual(29, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndLeatherMaterialAndClothClassShouldReturn62()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Prototype);

            Assert.AreEqual(62, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndChainMaterialAndClothClassShouldReturn118()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Prototype);

            Assert.AreEqual(118, actual);
        }

        [TestMethod]
        public void PrototypeGradeAndMetalPlateMaterialAndClothClassShouldReturn168()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Prototype);

            Assert.AreEqual(168, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndClothMaterialAndHeavyClassShouldReturn1356()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Shoddy);

            Assert.AreEqual(1356, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndLeatherMaterialAndHeavyClassShouldReturn1791()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Shoddy);

            Assert.AreEqual(1791, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndChainMaterialAndHeavyClassShouldReturn2783()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Shoddy);

            Assert.AreEqual(2783, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndMetalPlateMaterialAndHeavyClassShouldReturn2975()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Shoddy);

            Assert.AreEqual(2975, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndClothMaterialAndMediumClassShouldReturn1273()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Shoddy);

            Assert.AreEqual(1273, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndLeatherMaterialAndMediumClassShouldReturn1783()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Shoddy);

            Assert.AreEqual(1783, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndChainMaterialAndMediumClassShouldReturn2867()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Shoddy);

            Assert.AreEqual(2867, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndMetalPlateMaterialAndMediumClassShouldReturn3251()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Shoddy);

            Assert.AreEqual(3251, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndClothMaterialAndLightClassShouldReturn1036()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Shoddy);

            Assert.AreEqual(1036, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndLeatherMaterialAndLightClassShouldReturn1391()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Leather, ArmourGrade.Shoddy);

            Assert.AreEqual(1391, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndChainMaterialAndLightClassShouldReturn2183()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Chain, ArmourGrade.Shoddy);

            Assert.AreEqual(2183, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndMetalPlateMaterialAndLightClassShouldReturn2375()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Shoddy);

            Assert.AreEqual(2375, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndClothMaterialAndClothClassShouldReturn179()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Shoddy);

            Assert.AreEqual(179, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndLeatherMaterialAndClothClassShouldReturn247()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Shoddy);

            Assert.AreEqual(247, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndChainMaterialAndClothClassShouldReturn395()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Shoddy);

            Assert.AreEqual(395, actual);
        }

        [TestMethod]
        public void ShoddyGradeAndMetalPlateMaterialAndClothClassShouldReturn443()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Shoddy);

            Assert.AreEqual(443, actual);
        }

        [TestMethod]
        public void StandardGradeAndClothMaterialAndHeavyClassShouldReturn5187()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Standard);

            Assert.AreEqual(5187, actual);
        }

        [TestMethod]
        public void StandardGradeAndLeatherMaterialAndHeavyClassShouldReturn6567()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Standard);

            Assert.AreEqual(6567, actual);
        }

        [TestMethod]
        public void StandardGradeAndChainMaterialAndHeavyClassShouldReturn9935()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Standard);

            Assert.AreEqual(9935, actual);
        }

        [TestMethod]
        public void StandardGradeAndMetalPlateMaterialAndHeavyClassShouldReturn10103()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Standard);

            Assert.AreEqual(10103, actual);
        }

        [TestMethod]
        public void StandardGradeAndClothMaterialAndMediumClassShouldReturn4614()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Standard);

            Assert.AreEqual(4614, actual);
        }

        [TestMethod]
        public void StandardGradeAndLeatherMaterialAndMediumClassShouldReturn5935()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Standard);

            Assert.AreEqual(5935, actual);
        }

        [TestMethod]
        public void StandardGradeAndChainMaterialAndMediumClassShouldReturn9071()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Standard);

            Assert.AreEqual(9071, actual);
        }

        [TestMethod]
        public void StandardGradeAndMetalPlateMaterialAndMediumClassShouldReturn9407()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Standard);

            Assert.AreEqual(9407, actual);
        }

        [TestMethod]
        public void StandardGradeAndClothMaterialAndLightClassShouldReturn3907()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Standard);

            Assert.AreEqual(3907, actual);
        }

        [TestMethod]
        public void StandardGradeAndLeatherMaterialAndLightClassShouldReturn4967()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Leather, ArmourGrade.Standard);

            Assert.AreEqual(4967, actual);
        }

        [TestMethod]
        public void StandardGradeAndChainMaterialAndLightClassShouldReturn7535()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Chain, ArmourGrade.Standard);

            Assert.AreEqual(7535, actual);
        }

        [TestMethod]
        public void StandardGradeAndMetalPlateMaterialAndLightClassShouldReturn7703()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Standard);

            Assert.AreEqual(7703, actual);
        }

        [TestMethod]
        public void StandardGradeAndClothMaterialAndClothClassShouldReturn656()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Standard);

            Assert.AreEqual(656, actual);
        }

        [TestMethod]
        public void StandardGradeAndLeatherMaterialAndClothClassShouldReturn841()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Standard);

            Assert.AreEqual(841, actual);
        }

        [TestMethod]
        public void StandardGradeAndChainMaterialAndClothClassShouldReturn1283()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Standard);

            Assert.AreEqual(1283, actual);
        }

        [TestMethod]
        public void StandardGradeAndMetalPlateMaterialAndClothClassShouldReturn1325()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Standard);

            Assert.AreEqual(1325, actual);
        }

        [TestMethod]
        public void HighGradeAndClothMaterialAndHeavyClassShouldReturn11571()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.High);

            Assert.AreEqual(11571, actual);
        }

        [TestMethod]
        public void HighGradeAndLeatherMaterialAndHeavyClassShouldReturn14527()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.High);

            Assert.AreEqual(14527, actual);
        }

        [TestMethod]
        public void HighGradeAndChainMaterialAndHeavyClassShouldReturn21855()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.High);

            Assert.AreEqual(21855, actual);
        }

        [TestMethod]
        public void HighGradeAndMetalPlateMaterialAndHeavyClassShouldReturn21983()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.High);

            Assert.AreEqual(21983, actual);
        }

        [TestMethod]
        public void HighGradeAndClothMaterialAndMediumClassShouldReturn10182()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.High);

            Assert.AreEqual(10182, actual);
        }

        [TestMethod]
        public void HighGradeAndLeatherMaterialAndMediumClassShouldReturn12855()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Leather, ArmourGrade.High);

            Assert.AreEqual(12855, actual);
        }

        [TestMethod]
        public void HighGradeAndChainMaterialAndMediumClassShouldReturn19411()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Chain, ArmourGrade.High);

            Assert.AreEqual(19411, actual);
        }

        [TestMethod]
        public void HighGradeAndMetalPlateMaterialAndMediumClassShouldReturn19667()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.High);

            Assert.AreEqual(19667, actual);
        }

        [TestMethod]
        public void HighGradeAndClothMaterialAndLightClassShouldReturn8691()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Cloth, ArmourGrade.High);

            Assert.AreEqual(8691, actual);
        }

        [TestMethod]
        public void HighGradeAndLeatherMaterialAndLightClassShouldReturn10927()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Leather, ArmourGrade.High);

            Assert.AreEqual(10927, actual);
        }

        [TestMethod]
        public void HighGradeAndChainMaterialAndLightClassShouldReturn16455()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Chain, ArmourGrade.High);

            Assert.AreEqual(16455, actual);
        }

        [TestMethod]
        public void HighGradeAndMetalPlateMaterialAndLightClassShouldReturn16583()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.High);

            Assert.AreEqual(16583, actual);
        }

        [TestMethod]
        public void HighGradeAndClothMaterialAndClothClassShouldReturn1452()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.High);

            Assert.AreEqual(1452, actual);
        }

        [TestMethod]
        public void HighGradeAndLeatherMaterialAndClothClassShouldReturn1831()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.High);

            Assert.AreEqual(1831, actual);
        }

        [TestMethod]
        public void HighGradeAndChainMaterialAndClothClassShouldReturn2763()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.High);

            Assert.AreEqual(2763, actual);
        }

        [TestMethod]
        public void HighGradeAndMetalPlateMaterialAndClothClassShouldReturn2795()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.High);

            Assert.AreEqual(2795, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndClothMaterialAndHeavyClassShouldReturn20508()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Specialist);

            Assert.AreEqual(20508, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndLeatherMaterialAndHeavyClassShouldReturn25671()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Specialist);

            Assert.AreEqual(25671, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndChainMaterialAndHeavyClassShouldReturn38543()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Specialist);

            Assert.AreEqual(38543, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndMetalPlateMaterialAndHeavyClassShouldReturn38615()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Specialist);

            Assert.AreEqual(38615, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndClothMaterialAndMediumClassShouldReturn17977()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Specialist);

            Assert.AreEqual(17977, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndLeatherMaterialAndMediumClassShouldReturn22543()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Specialist);

            Assert.AreEqual(22543, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndChainMaterialAndMediumClassShouldReturn33887()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Specialist);

            Assert.AreEqual(33887, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndMetalPlateMaterialAndMediumClassShouldReturn34031()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Specialist);

            Assert.AreEqual(34031, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndClothMaterialAndLightClassShouldReturn15388()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Specialist);

            Assert.AreEqual(15388, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndLeatherMaterialAndLightClassShouldReturn19271()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Leather, ArmourGrade.Specialist);

            Assert.AreEqual(19271, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndChainMaterialAndLightClassShouldReturn28943()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Chain, ArmourGrade.Specialist);

            Assert.AreEqual(28943, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndMetalPlateMaterialAndLightClassShouldReturn29015()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Specialist);

            Assert.AreEqual(29015, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndClothMaterialAndClothClassShouldReturn2567()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Specialist);

            Assert.AreEqual(2567, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndLeatherMaterialAndClothClassShouldReturn3217()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Specialist);

            Assert.AreEqual(3217, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndChainMaterialAndClothClassShouldReturn4835()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Specialist);

            Assert.AreEqual(4835, actual);
        }

        [TestMethod]
        public void SpecialistGradeAndMetalPlateMaterialAndClothClassShouldReturn4853()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Specialist);

            Assert.AreEqual(4853, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndClothMaterialAndHeavyClassShouldReturn28887()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Cloth, ArmourGrade.Masterwork);

            Assert.AreEqual(28887, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndLeatherMaterialAndHeavyClassShouldReturn36119()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Leather, ArmourGrade.Masterwork);

            Assert.AreEqual(36119, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndChainMaterialAndHeavyClassShouldReturn54190()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.Chain, ArmourGrade.Masterwork);

            Assert.AreEqual(54190, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndMetalPlateMaterialAndHeavyClassShouldReturn54208()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Heavy, MaterialType.MetalPlate, ArmourGrade.Masterwork);

            Assert.AreEqual(54208, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndClothMaterialAndMediumClassShouldReturn25285()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Cloth, ArmourGrade.Masterwork);

            Assert.AreEqual(25285, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndLeatherMaterialAndMediumClassShouldReturn31626()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Leather, ArmourGrade.Masterwork);

            Assert.AreEqual(31626, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndChainMaterialAndMediumClassShouldReturn47459()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.Chain, ArmourGrade.Masterwork);

            Assert.AreEqual(47459, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndMetalPlateMaterialAndMediumClassShouldReturn47498()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Medium, MaterialType.MetalPlate, ArmourGrade.Masterwork);

            Assert.AreEqual(47498, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndClothMaterialAndLightClassShouldReturn21667()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Cloth, ArmourGrade.Masterwork);

            Assert.AreEqual(21667, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndLeatherMaterialAndLightClassShouldReturn27094()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Leather, ArmourGrade.Masterwork);

            Assert.AreEqual(27094, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndChainMaterialAndLightClassShouldReturn40651()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.Chain, ArmourGrade.Masterwork);

            Assert.AreEqual(40651, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndMetalPlateMaterialAndLightClassShouldReturn40971()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Light, MaterialType.MetalPlate, ArmourGrade.Masterwork);

            Assert.AreEqual(40971, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndClothMaterialAndClothClassShouldReturn3611()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Cloth, ArmourGrade.Masterwork);

            Assert.AreEqual(3611, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndLeatherMaterialAndClothClassShouldReturn4517()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Leather, ArmourGrade.Masterwork);

            Assert.AreEqual(4517, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndChainMaterialAndClothClassShouldReturn6778()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.Chain, ArmourGrade.Masterwork);

            Assert.AreEqual(6778, actual);
        }

        [TestMethod]
        public void MasterworkGradeAndMetalPlateMaterialAndClothClassShouldReturn6783()
        {
            var provider = new BaseArmourValueProvider();
            var actual = provider.GetBaseValue(ArmourClass.Cloth, MaterialType.MetalPlate, ArmourGrade.Masterwork);

            Assert.AreEqual(6783, actual);
        }
    }
}
