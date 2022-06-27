using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiCategories.SharedRules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KenshiWikiValidator.Tests.WikiCategories.SharedRules
{
    [TestClass]
    public class ContainsBlueprintsSectionRuleTests
    {
        [TestMethod]
        public void IsSuccessfulForCombinedCase()
        {
            var textToValidate = @"{{Weapon|string id = 1020-gamedata.base}}
== Blueprints ==
{{Blueprint
| color     = blue
| level     = 0
| name      = Wakizashis
| new items = [[Wakizashi]]
}}

The [[Blueprints]] for this item can be found at the following locations.

=== Shops ===
''This item's blueprint cannot be bought at any location.''

=== Loot ===
* [[Mongrel Weapon Shop]]";
            var weapon = new Weapon("1020-gamedata.base", "Wakizashi");
            var research = new Research("2107-gamedata.base", "Wakizashis")
            {
                EnableWeaponTypes = new ItemReference<Weapon>[] { new ItemReference<Weapon>(weapon, 0, 0, 0) }
            };
            var researchVendor = new VendorList("57326-rebirth.mod", "weapon vendor mongrel")
            {
                Blueprints = new ItemReference<Research>[] { new ItemReference<Research>(research, 100, 0, 0) }
            };
            var vendorSquad = new Squad("57327-rebirth.mod", "Shop weapons Mongrel")
            {
                Vendors = new ItemReference<VendorList>[] { new ItemReference<VendorList>(researchVendor, 0, 0, 0) }
            };
            var mongrelTown = new Town("18022-Newwworld.mod", "Mongrel");
            var towns = new[]
            {
                mongrelTown,
                new Town("1532936-__world reactions NE.mod", "Trader's Edge (override) prosperous"),
            };
            var mongrelFaction = new Faction("18891-rebirth.mod", "Mongrel");

            var titleCache = new WikiTitleCache();
            titleCache.AddTitle("57327-rebirth.mod", "Mongrel Weapon Shop");
            var itemRepository = new Mock<IItemRepository>();
            itemRepository
                .Setup(repo => repo.GetItemByStringId("1020-gamedata.base"))
                .Returns(weapon);
            itemRepository
                .Setup(repo => repo.GetItems<Research>())
                .Returns(new[] { research });
            itemRepository
                .Setup(repo => repo.GetItems<VendorList>())
                .Returns(new[] { researchVendor });
            itemRepository
                .Setup(repo => repo.GetItems<Squad>())
                .Returns(new[] { vendorSquad });
            itemRepository
                .Setup(repo => repo.GetItems<Faction>())
                .Returns(new[] { mongrelFaction });
            itemRepository
                .Setup(repo => repo.GetItems<Town>())
                .Returns(towns);

            var validator = new Mock<ArticleValidatorBase>(itemRepository.Object, new WikiTitleCache());
            validator
                .Setup(v => v.Rules)
                .Returns(new IValidationRule[]
                {
                    new ContainsBlueprintsSectionRule(itemRepository.Object, titleCache),
                });
            validator.Setup(v => v.CategoryName)
                .Returns("Weapons");

            validator.Object.CachePageData("Wakizashi", textToValidate);
            var result = validator.Object.Validate("Wakizashi", textToValidate);

            Assert.IsTrue(result.Success);
        }
    }
}
