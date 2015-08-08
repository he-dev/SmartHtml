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

        [TestMethod]
        public void ReadWhile_LeadingWhitespace_Ignore()
        {
            var i = 0;
            var value = "   <";
            var result = value.ReadWhile(c => c.IsWhiteSpace(), ref i, LeadingWhitespace.Ignore);
            
            Assert.AreEqual(3, i);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ReadWhile_LeadingWhitespace_Select()
        {
            var i = 0;
            var value = "   <";
            var result = value.ReadWhile(c => c.IsWhiteSpace(), ref i, LeadingWhitespace.Select);

            Assert.AreEqual(3, i);
            Assert.AreEqual("   ", result);
        }

        [TestMethod]
        public void ReadUntil_LeadingWhitespace_Ignore()
        {
            var i = 0;
            var value = "   <p";
            var result = value.ReadUntil(c => c.IsOpeningAngleBracket(), ref i, LeadingWhitespace.Ignore);

            Assert.AreEqual(3, i);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ReadUntil_LeadingWhitespace_Select()
        {
            var i = 0;
            var value = "   <p";
            var result = value.ReadUntil(c => c.IsOpeningAngleBracket(), ref i, LeadingWhitespace.Select);

            Assert.AreEqual(3, i);
            Assert.AreEqual("   ", result);
        }

        [TestMethod]
        public void ReadExact_LeadingWhitespace_Ignore()
        {
            var i = 0;
            var value = "   <p";
            var result = value.ReadExact(c => c.IsOpeningAngleBracket(), ref i, LeadingWhitespace.Ignore);

            Assert.AreEqual(4, i);
            Assert.AreEqual("<", result);
        }

        [TestMethod]
        public void ReadExact_LeadingWhitespace_Select()
        {
            var i = 0;
            var value = "   <p";
            var result = value.ReadExact(c => c.IsOpeningAngleBracket(), ref i, LeadingWhitespace.Select);

            Assert.AreEqual(0, i);
            Assert.IsNull(result);
        }
    }
}
