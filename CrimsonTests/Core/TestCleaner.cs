using Crimson.CSharp.Core;
using System.Diagnostics;

namespace CrimsonTest.Core
{
    [TestClass]
    public class TestCleaner
    {
        [TestMethod]
        public void Test_GetFileInfo()
        {
            string cleaner_root = "cleaner_root";
            string friendly_name = "friendly_name";
            string extension = ".crm_clnr";
            Cleaner cleaner = new Cleaner(cleaner_root);

            FileInfo actual = cleaner.GetUnfriendlyFileInfo(friendly_name);

            // check is equal to "cleaner_root/friendly_name.crm_clnr"
            FileInfo ideal = new FileInfo(cleaner_root + "/" + friendly_name + extension);
            Assert.AreEqual(ideal.FullName, actual.FullName);
        }


    [TestMethod]
        public void Test_GetFile()
        {

        }

        [TestMethod]
        public void Test_CleanFiles()
        {

        }
    }
}
