using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHtml.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Extract()
        {
            Assert.AreEqual("cd", "abcd".Extract(2, 4));
        }

       
    }
}
