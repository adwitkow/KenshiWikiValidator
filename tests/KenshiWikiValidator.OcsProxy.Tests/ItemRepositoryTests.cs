using System.Collections.Generic;
using System.Linq;
using KenshiWikiValidator.OcsProxy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenConstructionSet;
using OpenConstructionSet.Collections;
using OpenConstructionSet.Data;
using OpenConstructionSet.Data.Models;
using OpenConstructionSet.Models;

namespace KenshiWikiValidator.OcsProxy.Tests
{
    [TestClass]
    public class ItemRepositoryTests
    {
        private Installation? installation;

        [TestInitialize]
        public void Initialize()
        {
            var modfolder = new ModFolder("", new Dictionary<string, ModFile>());
            installation = new Installation("", new List<string>(), modfolder, modfolder, modfolder, null);
        }

        [TestMethod]
        public void ShouldConstruct()
        {
            var discoveryServiceMock = CreateDiscoveryServiceMock();
            var contextBuilderMock = new Mock<IOcsDataContextBuilder>();
            var repository = new ItemRepository(discoveryServiceMock.Object, contextBuilderMock.Object);

            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void ShouldMapTownsWithOverrides()
        {
            var dataTown = new DataItem(ItemType.Town, 0, "Town", "TownId");
            var baseDataTown = new DataItem(ItemType.Town, 0, "BaseTown", "BaseTownId");
            var references = new[] { new DataReference("TownId", 0, 0, 0) };
            var referenceCategory = new DataReferenceCategory("override town", references);
            baseDataTown.ReferenceCategories.Add(referenceCategory);
            var items = new[]
            {
                dataTown,
                baseDataTown,
            };
            var discoveryServiceMock = CreateDiscoveryServiceMock();
            var contextBuilderMock = CreateDataContextBuilderMock(items);
            var repository = new ItemRepository(discoveryServiceMock.Object, contextBuilderMock.Object);
            repository.Load();

            var town = repository.GetItemByStringId<Town>(dataTown.StringId);

            Assert.AreEqual(baseDataTown.StringId, town.BaseTowns.First().StringId);
        }

        private Mock<IOcsDataContextBuilder> CreateDataContextBuilderMock(IEnumerable<DataItem> items)
        {
            var contextBuilderMock = new Mock<IOcsDataContextBuilder>();
            var ioServiceMock = new Mock<IOcsIOService>();
            var dataContext = new OcsDataContext(
                ioServiceMock.Object,
                this.installation!,
                new OcsSortedList<DataItem>(items),
                new Dictionary<string, OpenConstructionSet.Models.Item>(),
                string.Empty,
                0);
            contextBuilderMock.Setup(builder => builder.Build(It.IsAny<OcsDataContexOptions>()))
                .Returns(dataContext);

            return contextBuilderMock;
        }

        private Mock<IOcsDiscoveryService> CreateDiscoveryServiceMock()
        {
            var result = new Dictionary<string, Installation>()
            {
                { "", this.installation! },
            };
            var discoveryServiceMock = new Mock<IOcsDiscoveryService>();
            discoveryServiceMock.Setup(service => service.DiscoverAllInstallations())
                .Returns(result);

            return discoveryServiceMock;
        }
    }
}
