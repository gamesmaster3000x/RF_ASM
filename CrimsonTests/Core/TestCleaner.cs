using Crimson.CSharp.Core;
using System.Diagnostics;

namespace CrimsonTest.Core
{
    [TestClass]
    public class TestCleaner
    {
        private class DummyCleaner : Cleaner
        {
            public DummyCleaner(string rootPath) : base(rootPath)
            {
            }

            public Dictionary<string, FileInfo> D_GetFiles()
            {
                return _files;
            }

        }

        [TestMethod]
        public void Test_GetFileInfo()
        {
            string cleaner_root = "cleaner_root";
            string friendly_name = "friendly_name";
            string extension = ".crm_clnr";
            DummyCleaner cleaner = new DummyCleaner(cleaner_root);

            FileInfo ideal = new FileInfo(cleaner_root + "/" + friendly_name + extension);
            FileInfo actual = cleaner.GetUnfriendlyFileInfo(friendly_name);

            // check is equal to "cleaner_root/friendly_name.crm_clnr"
            Assert.AreEqual(ideal.FullName, actual.FullName);
        }

        [TestMethod]
        public void Test_GetFile_WhenIsNotTrackingFile()
        {
            string cleaner_root = "cleaner_root";
            string friendly_name = "friendly_name";
            string extension = ".crm_clnr";
            DummyCleaner cleaner = new DummyCleaner(cleaner_root);

            FileInfo ideal = new FileInfo(cleaner_root + "/" + friendly_name + extension);
            FileInfo actual = cleaner.GetFile(friendly_name);

            Assert.AreEqual(ideal.FullName, actual.FullName);
        }

        [TestMethod]
        public void Test_GetFile_WhenIsTrackingFile()
        {
            string cleaner_root = "cleaner_root";
            string friendly_name = "friendly_name";
            string extension = ".crm_clnr";
            DummyCleaner cleaner = new DummyCleaner(cleaner_root);

            FileInfo ideal = new FileInfo(cleaner_root + "/" + friendly_name + extension);
            cleaner.D_GetFiles().Add(friendly_name, ideal);
            FileInfo actual = cleaner.GetFile(friendly_name);

            Assert.AreEqual(ideal.FullName, actual.FullName);
        }

        [TestMethod]
        public void Test_CleanFiles()
        {

        }
    }
}
