using System.Collections.Generic;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.Characters.Templates;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.Characters.Templates
{
    [TestClass]
    public class CharacterTemplateCreatorTests
    {
        private static readonly Race TestRace = new Race("race id", "test race");

        [TestMethod]
        public void GenerateShouldReturnNullIfCharacterIsNull()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);

            var result = sut.Generate(new ArticleData());

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GenerateShouldReturnNotReturnNullIfCharacterIsNotNull()
        {
            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = new Character("string id", "name");

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TemplateShouldIncludeSquadRacesIfCharacterRacesIsEmpty()
        {
            var character = new Character("string id", "name");
            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItems<Squad>())
                .Returns(new List<Squad>()
                {
                    new Squad("squad string id", "squad name")
                    {
                        Characters = new[] { new ItemReference<Character>(character) },
                        RaceOverrides = new[] { new ItemReference<Race>(TestRace) }
                    }
                });
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual($"[[{TestRace.Name}]]", result.Parameters["race"]);
        }

        [TestMethod]
        public void TemplateShouldIncludeFactionRacesIfCharacterAndSquadRacesIsEmpty()
        {
            var character = new Character("string id", "name");
            var faction = new Faction("faction id", "faction")
            {
                Races = new[] { new ItemReference<Race>(TestRace) }
            };
            var squad = new Squad("squad string id", "squad name")
            {
                Characters = new[] { new ItemReference<Character>(character) },
                Faction = new[] { new ItemReference<Faction>(faction) }
            };
            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItems<Squad>())
                .Returns(new List<Squad>() { squad });
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual($"[[{TestRace.Name}]]", result.Parameters["race"]);
        }

        [TestMethod]
        public void RaceShouldBeSubraceIfRaceHasParent()
        {
            var character = new Character("string id", "name");
            var faction = new Faction("faction id", "faction")
            {
                Races = new[] { new ItemReference<Race>(new Race("race id", "Soldierbot")) }
            };
            var squad = new Squad("squad string id", "squad name")
            {
                Characters = new[] { new ItemReference<Character>(character) },
                Faction = new[] { new ItemReference<Faction>(faction) }
            };
            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItems<Squad>())
                .Returns(new List<Squad>() { squad });
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual($"[[Skeleton]]", result.Parameters["race"]);
            Assert.AreEqual($"[[Soldierbot]]", result.Parameters["subrace"]);
        }

        [TestMethod]
        public void ShouldConvertFemaleChanceCorrectly()
        {
            var character = new Character("string id", "name")
            {
                FemaleChance = 50
            };

            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual("50% female", result.Parameters["gender"]);
        }

        [TestMethod]
        public void ShouldConvertZeroPercentFemaleChanceCorrectly()
        {
            var character = new Character("string id", "name")
            {
                FemaleChance = 0
            };

            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual("Male", result.Parameters["gender"]);
        }

        [TestMethod]
        public void ShouldConvertHundredPercentFemaleChanceCorrectly()
        {
            var character = new Character("string id", "name")
            {
                FemaleChance = 100
            };

            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual("Female", result.Parameters["gender"]);
        }

        [TestMethod]
        public void ShouldPullImageFromExistingTemplate()
        {
            var character = new Character("string id", "name");
            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var articleData = new ArticleData()
            {
                WikiTemplates = new[]
                {
                    new WikiTemplate("Character", new IndexedDictionary<string, string?>()
                    {
                        { "image1", "image.png" }
                    })
                }
            };
            var result = sut.Generate(articleData);

            Assert.IsNotNull(result);
            Assert.AreEqual("image.png", result.Parameters["image1"]);
        }

        [TestMethod]
        public void ShouldAssignSingleClothingItems()
        {
            var clothingItems = new List<ItemReference<Armour>>()
            {
                new ItemReference<Armour>(new Armour("legs id", "legs")
                {
                    Slot = AttachSlot.Legs
                }),
                new ItemReference<Armour>(new Armour("body id", "body")
                {
                    Slot = AttachSlot.Body
                }),
                new ItemReference<Armour>(new Armour("hat id", "hat")
                {
                    Slot = AttachSlot.Hat
                }),
                new ItemReference<Armour>(new Armour("boots id", "boots")
                {
                    Slot = AttachSlot.Boots
                }),
                new ItemReference<Armour>(new Armour("shirt id", "shirt")
                {
                    Slot = AttachSlot.Shirt
                }),
            };
            var character = new Character("string id", "name")
            {
                Clothing = clothingItems
            };

            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual("[[hat]]", result.Parameters["headgear"]);
            Assert.AreEqual("[[legs]]", result.Parameters["legwear"]);
            Assert.AreEqual("[[body]]", result.Parameters["armour"]);
            Assert.AreEqual("[[shirt]]", result.Parameters["shirt"]);
            Assert.AreEqual("[[boots]]", result.Parameters["footwear"]);
        }

        [TestMethod]
        public void ShouldHandleMultipleClothingItems()
        {
            var clothingItems = new List<ItemReference<Armour>>()
            {
                new ItemReference<Armour>(new Armour("legs id1", "legs1")
                {
                    Slot = AttachSlot.Legs
                }),
                new ItemReference<Armour>(new Armour("legs id2", "legs2")
                {
                    Slot = AttachSlot.Legs
                }),
            };
            var character = new Character("string id", "name")
            {
                Clothing = clothingItems
            };

            var itemRepositoryMock = new Mock<IItemRepository>();
            var sut = new CharacterTemplateCreator(itemRepositoryMock.Object);
            sut.Character = character;

            var result = sut.Generate(new ArticleData());

            Assert.IsNotNull(result);
            Assert.AreEqual("[[legs1]], [[legs2]]", result.Parameters["legwear"]);
        }
    }
}
