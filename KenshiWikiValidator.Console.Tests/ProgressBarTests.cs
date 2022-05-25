using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Console.Tests
{
    [TestClass]
    public class ProgressBarTests
    {
        [TestMethod]
        public async Task ShouldHandleNegativeValues()
        {
            var progress = new ProgressBar();
            using (progress)
            {
                for (int i = 0; i < 10; i++)
                {
                    progress.Report(-i);
                    await Task.Delay(200);
                }
            }

            Assert.ThrowsException<ObjectDisposedException>(() => progress.Report(-1));
        }

        [TestMethod]
        public async Task ShouldHandleBigValues()
        {
            var progress = new ProgressBar();
            using (progress)
            {
                for (int i = 0; i < 10; i++)
                {
                    progress.Report(double.MaxValue);
                    await Task.Delay(200);
                }
            }

            Assert.ThrowsException<ObjectDisposedException>(() => progress.Report(double.MaxValue));
        }
    }
}