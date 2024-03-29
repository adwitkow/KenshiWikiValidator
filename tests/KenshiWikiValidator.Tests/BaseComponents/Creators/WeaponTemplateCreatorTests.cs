﻿using System;
using System.Linq;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.BaseComponents.Creators;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.WikiTemplates.Creators
{
    [TestClass]
    public class WeaponTemplateCreatorTests
    {
        [TestMethod]
        public void ShouldReturnNullForEmptyArticleData()
        {
            var repository = new Mock<IItemRepository>();
            var creator = new WeaponTemplateCreator(repository.Object);

            var template = creator.Generate(new ArticleData());

            Assert.IsNull(template);
        }

        [TestMethod]
        public void ShouldNotReturnNullForValidStringId()
        {
            var weapon = new Weapon("stringid", "weapon name");
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Weapon>("stringid"))
                .Returns(weapon);
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new WeaponTemplateCreator(repository.Object);

            var template = creator.Generate(articleData);

            Assert.IsNotNull(template);
        }

        [TestMethod]
        public void ShouldConvertRaceDamagesCorrectly()
        {
            var races = new[]
            {
                (new Race("spiderid", "Spider"), 50),
                (new Race("smallspiderid", "Small Spider"), 150),
                (new Race("bonedogid", "Bonedog"), 0),
                (new Race("skimmerid", "Skimmer"), 75),
                (new Race("beakthingid", "Beak Thing"), 200),
                (new Race("gorilloid", "Gorillo"), 25),
                (new Race("leviathanid", "Leviathan"), 100),
            };
            var raceReferences = races.Select(race => new ItemReference<Race>(race.Item1, race.Item2, 0, 0));
            var weapon = new Weapon("stringid", "weapon name")
            {
                RaceDamage = raceReferences,
            };
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Weapon>("stringid"))
                .Returns(weapon);
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new WeaponTemplateCreator(repository.Object);

            var template = creator.Generate(articleData);

            Assert.IsNotNull(template);
            Assert.AreEqual("-50", template.Parameters["damage_spider"]);
            Assert.AreEqual("+50", template.Parameters["damage_small spider"]);
            Assert.AreEqual("-100", template.Parameters["damage_bonedog"]);
            Assert.AreEqual("-25", template.Parameters["damage_skimmer"]);
            Assert.AreEqual("+100", template.Parameters["damage_beak thing"]);
            Assert.AreEqual("-75", template.Parameters["damage_gorillo"]);
            Assert.IsNull(template.Parameters["damage_leviathan"]);
        }

        [TestMethod]
        public void ShouldConvertNewlinesInDescription()
        {
            var weapon = new Weapon("stringid", "weapon name")
            {
                Description = @"this is a line

and this is another line"
            };
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Weapon>("stringid"))
                .Returns(weapon);
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new WeaponTemplateCreator(repository.Object);

            var template = creator.Generate(articleData);

            Assert.IsNotNull(template);
            Assert.AreEqual("this is a line<br /><br />and this is another line", template.Parameters["description"]);
        }

        [TestMethod]
        public void ShouldThrowOnUnknownRace()
        {
            var races = new[]
            {
                (new Race("humanid", "Human"), 50)
            };
            var raceReferences = races.Select(race => new ItemReference<Race>(race.Item1, race.Item2, 0, 0));
            var weapon = new Weapon("stringid", "weapon name")
            {
                RaceDamage = raceReferences,
            };
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Weapon>("stringid"))
                .Returns(weapon);
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new WeaponTemplateCreator(repository.Object);

            Assert.ThrowsException<InvalidOperationException>(() => creator.Generate(articleData));
        }

        [TestMethod]
        public void BleedMultiplierShouldFormatCorrectly()
        {
            var weapon = new Weapon("stringid", "weapon name")
            {
                BleedMultiplier = 1.2f
            };
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Weapon>("stringid"))
                .Returns(weapon);
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new WeaponTemplateCreator(repository.Object);

            var template = creator.Generate(articleData);

            Assert.IsNotNull(template);
            Assert.AreEqual("1.20", template.Parameters["blood loss"]);
        }
    }
}
