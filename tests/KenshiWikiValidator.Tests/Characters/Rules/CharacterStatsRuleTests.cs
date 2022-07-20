using System.Collections.Generic;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.Characters.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.Characters.Rules
{
    [TestClass]
    public class CharacterStatsRuleTests
    {
        [TestMethod]
        public void ShouldNotValidateLoreArticles()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var characterStatsRule = new CharacterStatsRule(repositoryMock.Object);
            var articleData = new ArticleData()
            {
                Categories = new[] { "Lore" },
                StringIds = new[] { "stringId" },
            };

            var result = characterStatsRule.Execute("", "", articleData);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldHandleAnimalArticles()
        {
            var animal = new AnimalCharacter("stringId", "animal")
            {
                Strength = 5,
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock.Setup(repo => repo.GetItemByStringId<AnimalCharacter>("stringId"))
                .Returns(animal);
            var characterStatsRule = new CharacterStatsRule(repositoryMock.Object);
            var articleData = new ArticleData()
            {
                Categories = new[] { "Animals" },
                StringIds = new[] { "stringId" },
                WikiTemplates = new[]
                {
                    new WikiTemplate("Animal Stats", new SortedList<string, string?>()
                    {
                        { "strength", "5" }
                    }),
                },
            };

            var result = characterStatsRule.Execute("", "", articleData);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldHandleBasicCharacters()
        {
            var character = new Character("stringId", "Character")
            {
                Strength = 5,
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock.Setup(repo => repo.GetItemByStringId<Character>("stringId"))
                .Returns(character);
            var characterStatsRule = new CharacterStatsRule(repositoryMock.Object);
            var articleData = new ArticleData()
            {
                StringIds = new[] { "stringId" },
                WikiTemplates = new[]
                {
                    new WikiTemplate("Character Stats", new SortedList<string, string?>()
                    {
                        { "strength", "5" }
                    }),
                },
            };

            var result = characterStatsRule.Execute("", "", articleData);

            Assert.IsTrue(result.Success);
        }


        [TestMethod]
        public void ShouldHandleComplexStats()
        {
            var stats = new Stats("statsId", "stats")
            {
                Strength = 5
            };
            var character = new Character("stringId", "Character")
            {
                Stats = new[] { new ItemReference<Stats>(stats, 0, 0, 0) },
            };

            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock.Setup(repo => repo.GetItemByStringId<Character>("stringId"))
                .Returns(character);
            var characterStatsRule = new CharacterStatsRule(repositoryMock.Object);
            var articleData = new ArticleData()
            {
                StringIds = new[] { "stringId" },
                WikiTemplates = new[]
                {
                    new WikiTemplate("Stats", new SortedList<string, string?>()
                    {
                        { "Strength", "5" }
                    }),
                },
            };

            var result = characterStatsRule.Execute("", "", articleData);

            Assert.IsTrue(result.Success);
        }
    }
}
