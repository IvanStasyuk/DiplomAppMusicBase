using DiplomAppMusicBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MusicBaseTests
{
    [TestClass]
    public class AutorisationTest
    {
        [TestMethod]
        public void Autorisation()
        {
            string TBLoginTest = "ivanstasyuk";
            string TBPasswordTest = "ivanstasyuk";
            Assert.AreEqual(TBLoginTest, TBPasswordTest);
        }
        [TestMethod]
        public void Autorisation1()
        {
            string TBLoginTest = "";
            string TBPasswordTest = "";
            Assert.AreEqual(TBLoginTest, TBPasswordTest);
        }
    }
}
