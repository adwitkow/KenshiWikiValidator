using KenshiDataSnooper.Desktop.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiDataSnooper.Desktop.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        [TestMethod]
        public void GroupedItemsCollectionShouldInitializeOnConstruction()
        {
            var viewModel = new MainViewModel();

            Assert.IsNotNull(viewModel.GroupedItems);
        }
    }
}
