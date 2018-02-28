using System;
using System.IO.IsolatedStorage;
using IFiles_CFindFile_CWriteFile;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingClasses
{
    [TestClass]
    public class CFindFileTest
    {
   
        [TestMethod]
        public void ReversedMod1Test()
        {
            IFiles_CFindFile_CWriteFile.CFindFiles func = new CFindFiles();
            string[] str={ @"\1\1\2\test_dat.dat"};
            string[] exp={ @"test_dat.dat\2\1\1\"};
            string[] act;

            act = func.Reversed(str, 1);

            Assert.AreEqual(exp[0], act[0],"on this string {0} expected {1} have {2}",str[0],exp[0],act[0]);
        }

        [TestMethod]
        public void ReversedMod2Test()
        {
            IFiles_CFindFile_CWriteFile.CFindFiles func = new CFindFiles();
            string[] str = { @"\qwer\tyui\test_dat.dat" };
            string[] exp = { @"tad.tad_tset\iuyt\rewq\" };
            string[] act;

            act = func.Reversed(str, 2);

            Assert.AreEqual(exp[0], act[0], "on this string {0} expected {1} have {2}", str[0], exp[0], act[0]);
        }

        [TestMethod]
        public void ReadyNamesTest()
        {
            string[] str = {@"D:\test\1\1\2\test_dat.dat"};
            string dir = @"D:\test";
            IFiles_CFindFile_CWriteFile.CFindFiles func = new CFindFiles();
            string exp = @"\1\1\2\test_dat.dat";
            string []act;

            act = func.ReadyNames(str, dir);

            Assert.AreEqual(exp,act[0],"On string {0} expected {1} have {2}",str[0],exp,act[0]);
        }       
    }

}
