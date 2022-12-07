using Crimson.CSharp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Crimson.CSharp_Test.Core
{
    [TestClass]
    internal class TestCleaner
    {
        [TestMethod]
        void Test_GetFile()
        {
            Cleaner cleaner = new Cleaner("");
            FileInfo info = cleaner.GetFile("friendly_name");
            Assert.AreEqual(info.FullName, "");
        }

        [TestMethod]
        void Test_CleanFiles()
        {

        }
    }
}
