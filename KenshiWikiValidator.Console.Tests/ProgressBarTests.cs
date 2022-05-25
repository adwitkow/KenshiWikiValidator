using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KenshiWikiValidator.Console.Tests
{
    [TestClass]
    public class ProgressBarTests
    {
        [TestMethod]
        public void ShouldHandleNegativeValues()
        {
            var progress = new ProgressBar();
            using (progress)
            {
                progress.Report(-1);
            }

            Assert.ThrowsException<ObjectDisposedException>(() => progress.Report(-1));
        }

        [TestMethod]
        public void ShouldHandleBigValues()
        {
            var progress = new ProgressBar();
            using (progress)
            {
                progress.Report(double.MaxValue);
            }

            Assert.ThrowsException<ObjectDisposedException>(() => progress.Report(double.MaxValue));
        }
    }
}