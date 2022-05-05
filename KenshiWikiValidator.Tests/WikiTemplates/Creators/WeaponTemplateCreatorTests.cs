using KenshiWikiValidator.BaseComponents;
using KenshiWikiValidator.OcsProxy;
using KenshiWikiValidator.OcsProxy.Models;
using KenshiWikiValidator.WikiTemplates.Creators;
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
            var creator = new WeaponTemplateCreator(repository.Object, new ArticleData());

            var template = creator.Generate();

            Assert.IsNull(template);
        }

        [TestMethod]
        public void ShouldNotReturnNullForValidStringId()
        {
            var repository = new Mock<IItemRepository>();
            repository
                .Setup(repo => repo.GetItemByStringId<Weapon>("stringid"))
                .Returns(new Weapon("stringid", "weapon name"));
            var articleData = new ArticleData();
            articleData.StringIds.Add("stringid");

            var creator = new WeaponTemplateCreator(repository.Object, articleData);

            var template = creator.Generate();

            Assert.IsNotNull(template);
        }
    }
}
