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
        }

        [TestMethod]
        public void AdvanceWhile_Whitespace()
        {
            var i = 0;
            var value = "   <";
            var result = value.AdvanceWhile(c => c.IsWhiteSpace(), ref i);
            
            Assert.AreEqual(3, i);
            Assert.AreEqual("   ", result);
        }

        [TestMethod]
        public void AdvanceUntil_OpeningAngleBracketTest()
        {
            var i = 0;
            var value = "abc<";
            var result = value.AdvanceUntil(c => c.IsOpeningAngleBracket(), ref i);

            Assert.AreEqual(3, i);
            Assert.AreEqual("abc", result);
        }

        [TestMethod]
        public void AdvanceOne()
        {
            var i = 0;
            var value = "<span>";
            var result = value.AdvanceOne(c => c.IsOpeningAngleBracket(), ref i);

            Assert.AreEqual(1, i);
            Assert.AreEqual("<", result);
        }
    }
}
