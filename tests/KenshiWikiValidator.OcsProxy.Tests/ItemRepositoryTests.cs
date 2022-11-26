using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet;
using OpenConstructionSet.Data;
using OpenConstructionSet.Installations;
using OpenConstructionSet.Mods;
using OpenConstructionSet.Mods.Context;

namespace KenshiWikiValidator.OcsProxy.Tests
{
    [TestClass]
    public class ItemRepositoryTests
    {
        private Installation? installation;

        [TestInitialize]
        public void Initialize()
        {
            installation = new Installation("", "", "");
        }

        [TestMethod]
        public void ShouldConstruct()
        {
            var installationServiceMock = new Mock<IInstallationService>();
            var contextBuilderMock = new Mock<IContextBuilder>();
            var repository = new ItemRepository(installationServiceMock.Object, contextBuilderMock.Object);

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public async Task ShouldMapTownsWithOverrides()
        {
            var dataTown = new ModItem(ItemType.Town, "Town", "TownId");
            var baseDataTown = new ModItem(ItemType.Town, "BaseTown", "BaseTownId");
            var references = new[] { new ModReference("TownId", 0, 0, 0) };
            var referenceCategory = new ModReferenceCategory("override town", references);
            baseDataTown.ReferenceCategories.Add(referenceCategory);
            var items = new[]
            {
                dataTown,
                baseDataTown,
            };
            var installationServiceMock = CreateInstallationServiceMock();
            var contextBuilderMock = CreateDataContextBuilderMock(items);
            var repository = new ItemRepository(installationServiceMock.Object, contextBuilderMock.Object);
            await repository.LoadAsync();

            var town = repository.GetItemByStringId<Town>(dataTown.StringId);

            Assert.AreEqual(baseDataTown.StringId, town.BaseTowns.First().StringId);
        }

        private Mock<IContextBuilder> CreateDataContextBuilderMock(IEnumerable<ModItem> items)
        {
            var contextBuilderMock = new Mock<IContextBuilder>();
            var modContextMock = new Mock<IModContext>();

            modContextMock.Setup(context => context.Items)
                .Returns(new ModItemCollection(null, items));

            contextBuilderMock.Setup(builder => builder.BuildAsync(It.IsAny<ModContextOptions>()))
                .Returns(Task.FromResult(modContextMock.Object));

            return contextBuilderMock;
        }

        private Mock<IInstallationService> CreateInstallationServiceMock()
        {
            var installationMock = new Mock<IInstallation>();
            installationMock.Setup(i => i.Identifier)
                .Returns(string.Empty);
            var result = new[] { installationMock.Object }.ToAsyncEnumerable();
            var installationServiceMock = new Mock<IInstallationService>();
            installationServiceMock.Setup(service => service.DiscoverAllInstallationsAsync())
                .Returns(result);

            return installationServiceMock;
        }
    }
}
