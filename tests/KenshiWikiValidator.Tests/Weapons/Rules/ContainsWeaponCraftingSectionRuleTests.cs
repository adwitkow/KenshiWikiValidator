using System;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.Weapons.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.WikiCategories.Weapons.Rules
{
    [TestClass]
    public class ContainsWeaponCraftingSectionRuleTests
    {
        [TestMethod]
        public void ShouldSucceedIfStringIdIsMissing()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            var result = rule.Execute("weapon title", "content", new ArticleData());

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldReturnSectionWithMessageIfNoResearch()
        {
            var weapon = new Weapon("weaponstringid", "weaponname");
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Weapon>(weapon.StringId))
                .Returns(weapon);
            var data = new ArticleData()
            {
                StringIds = new[] { weapon.StringId }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            var result = rule.Execute("weapon title", @"== Crafting ==
This item cannot be crafted.
", data);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldThrowIfStringIdCannotBeFound()
        {
            var repositoryMock = new Mock<IItemRepository>();
            var data = new ArticleData()
            {
                StringIds = new[] { "stringid" }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            Assert.ThrowsException<InvalidOperationException>(() => rule.Execute("weapon title", string.Empty, data));
        }

        [TestMethod]
        public void ShouldThrowIfStringIdDoesntBelongToWeapon()
        {
            var town = new Town("townstringid", "townname");
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Town>(town.StringId))
                .Returns(town);
            var data = new ArticleData()
            {
                StringIds = new[] { town.StringId }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            Assert.ThrowsException<InvalidOperationException>(() => rule.Execute("weapon title", string.Empty, data));
        }

        [TestMethod]
        public void ShouldGenerateCrafting()
        {
            var weapon = new Weapon("weaponstringid", "weaponname");
            var research = new Research("researchid", "research")
            {
                EnableWeaponTypes = new[] { new ItemReference<Weapon>(weapon, 0, 0, 0) }
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Weapon>(weapon.StringId))
                .Returns(weapon);
            repositoryMock
                .Setup(repo => repo.GetItems<Research>())
                .Returns(new[] { research });
            var data = new ArticleData()
            {
                StringIds = new[] { weapon.StringId }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            var result = rule.Execute("weapon title", $@"== Crafting ==
{{{{Research info
| level     = 0
| name      = {research.Name}
| new_items = [[{weapon.Name}]]
| time      = 0
}}}}

This item can be crafted in various qualities using different levels of [[Weapon Smithing Bench]].

{{{{Crafting | collapsed
| building      = Weapon Smith I
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith II
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith III
| imagesettings = 96px
| input0        = Steel Bars
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}", data);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldGenerateEnabledBuildings()
        {
            var building = new Building("buildingid", "buildingname");
            var weapon = new Weapon("weaponstringid", "weaponname");
            var research = new Research("researchid", "research")
            {
                EnableWeaponTypes = new[] { new ItemReference<Weapon>(weapon, 0, 0, 0) },
                EnableBuildings = new[] { new ItemReference<Building>(building, 0, 0, 0) },
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Weapon>(weapon.StringId))
                .Returns(weapon);
            repositoryMock
                .Setup(repo => repo.GetItems<Research>())
                .Returns(new[] { research });
            var data = new ArticleData()
            {
                StringIds = new[] { weapon.StringId }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            var result = rule.Execute("weapon title", $@"== Crafting ==
{{{{Research info
| level     = 0
| name      = research
| new_bldgs = [[{building.Name}]]
| new_items = [[{weapon.Name}]]
| time      = 0
}}}}

This item can be crafted in various qualities using different levels of [[Weapon Smithing Bench]].

{{{{Crafting | collapsed
| building      = Weapon Smith I
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith II
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith III
| imagesettings = 96px
| input0        = Steel Bars
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}", data);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldGenerateBlueprints()
        {
            var weapon = new Weapon("weaponstringid", "weaponname");
            var research = new Research("researchid", "research")
            {
                EnableWeaponTypes = new[] { new ItemReference<Weapon>(weapon, 0, 0, 0) }
            };
            var vendorList = new VendorList("vendorlistid", "vendorlist")
            {
                Blueprints = new[] { new ItemReference<Research>(research, 0, 0, 0) }
            };
            var squad = new Squad("squadid", "squad")
            {
                Vendors = new[] { new ItemReference<VendorList>(vendorList, 0, 0, 0) }
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Weapon>(weapon.StringId))
                .Returns(weapon);
            repositoryMock
                .Setup(repo => repo.GetItems<Research>())
                .Returns(new[] { research });
            repositoryMock
                .Setup(repo => repo.GetItems<VendorList>())
                .Returns(new[] { vendorList });
            repositoryMock
                .Setup(repo => repo.GetItems<Squad>())
                .Returns(new[] { squad });
            var data = new ArticleData()
            {
                StringIds = new[] { weapon.StringId }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            var result = rule.Execute("weapon title", $@"== Crafting ==
This item can be crafted in various qualities using different levels of [[Weapon Smithing Bench]] after learning the appropriate [[Blueprints|blueprint]].

{{{{Crafting | collapsed
| building      = Weapon Smith I
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith II
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith III
| imagesettings = 96px
| input0        = Steel Bars
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}", data);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldGenerateCraftingWithMoreLevels()
        {
            var weapon = new Weapon("weaponstringid", "weaponname");
            var research = new Research("researchid", "research")
            {
                EnableWeaponTypes = new[] { new ItemReference<Weapon>(weapon, 0, 0, 0) }
            };
            var nextResearch = new Research("nextresearchid", "nextresearch")
            {
                Requirements = new[] { new ItemReference<Research>(research, 0, 0, 0) }
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Weapon>(weapon.StringId))
                .Returns(weapon);
            repositoryMock
                .Setup(repo => repo.GetItems<Research>())
                .Returns(new[] { research, nextResearch });
            var data = new ArticleData()
            {
                StringIds = new[] { weapon.StringId }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            var result = rule.Execute("weapon title", $@"== Crafting ==
{{{{Research info
| level        = 0
| name         = research
| new_items    = [[{weapon.Name}]]
| required_for = [[nextresearch (Tech)]]
| time         = 0
}}}}

This item can be crafted in various qualities using different levels of [[Weapon Smithing Bench]].

{{{{Crafting | collapsed
| building      = Weapon Smith I
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith II
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith III
| imagesettings = 96px
| input0        = Steel Bars
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = {weapon.Name}
}}}}", data);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ShouldGenerateResearchCosts()
        {
            var weapon = new Weapon("weaponstringid", "weaponname");
            var research = new Research("researchid", "research")
            {
                EnableWeaponTypes = new[] { new ItemReference<Weapon>(weapon, 0, 0, 0) },
                Costs = new[] { new ItemReference<Item>(new Item("bookid", "Book"), 2, 0, 0)}
            };
            var repositoryMock = new Mock<IItemRepository>();
            repositoryMock
                .Setup(repo => repo.GetItemByStringId<Weapon>(weapon.StringId))
                .Returns(weapon);
            repositoryMock
                .Setup(repo => repo.GetItems<Research>())
                .Returns(new[] { research });
            var data = new ArticleData()
            {
                StringIds = new[] { weapon.StringId }
            };

            var rule = new ContainsWeaponCraftingSectionRule(repositoryMock.Object);

            var result = rule.Execute("weapon title", $@"== Crafting ==
{{{{Research info
| costs     = 2 [[Book]]s
| level     = 0
| name      = research
| new_items = [[weaponname]]
| time      = 0
}}}}

This item can be crafted in various qualities using different levels of [[Weapon Smithing Bench]].

{{{{Crafting | collapsed
| building      = Weapon Smith I
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| output        = weaponname
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith II
| imagesettings = 96px
| input0        = Iron Plates
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = weaponname
}}}}
{{{{Crafting | collapsed
| building      = Weapon Smith III
| imagesettings = 96px
| input0        = Steel Bars
| input0amount  = 0
| input1        = Fabrics
| input1amount  = 0
| output        = weaponname
}}}}", data);

            Assert.IsTrue(result.Success);
        }
    }
}
