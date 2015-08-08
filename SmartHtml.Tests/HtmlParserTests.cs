using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHtml.Tests
{
    [TestClass]
    public class HtmlParserTests
    {
        [TestMethod]
        public void Parse_EmptyElement()
        {
            var html = ReadAllText("EmptyElement");
            var htmlDocument = HtmlParser.Parse(html);

            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithText()
        {
            var html = "<span>Lorem ipsum.</span>";
            var htmlDocument = HtmlParser.Parse(html);
        }

        [TestMethod]
        public void Parse_TwoElementsNextToEachOther()
        {
            var html = ReadAllText("TwoElementsNextToEachOther");
            var htmlDocument = HtmlParser.Parse(html);

            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        //[TestMethod]
        //public void Parse_EmptyNestedElements()
        //{
        //    var html = ReadAllText("EmptyNestedElements");
        //    var htmlDocument = HtmlParser.Parse(html);
        //    Assert.IsNotNull(htmlDocument);
        //    Assert.AreEqual(html, htmlDocument.ToString());
        //}

        [TestMethod]
        public void Parse_NestedElementsWithText()
        {
            var html = ReadAllText("NestedElementsWithText");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithAttributes()
        {
            var html = ReadAllText("ElementWithAttributes");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("<span id=\"abc\" style=\"color: blue\" class>Lorem ipsum.</span>", htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithAttributesAndSpaces()
        {
            var html = ReadAllText("ElementWithAttributesAndSpaces");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("   <p style=\"color: blue\" class><span>Lorem ipsum</span> dolor.</p>", htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_VoidElement()
        {
            var html = ReadAllText("VoidElement");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_VoidElementBeforeOtherElement()
        {
            var html = ReadAllText("VoidElementBeforeOtherElement");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_MultilineHtml()
        {
            var html = ReadAllText("MultilineHtml1");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        #region Test invalid HTML

        [TestMethod]
        [ExpectedException(typeof(MissingClosingTagException))]
        public void Parse_UnclosedElement()
        {
            var html = ReadAllText("UnclosedElement");
            var htmlDocument = HtmlParser.Parse(html);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterException))]
        public void Parse_DoubbleOpeningAngleBracket()
        {
            var html = ReadAllText("DoubbleOpeningAngleBracket");
            var htmlDocument = HtmlParser.Parse(html);
        }

        #endregion

        private static string ReadAllText(string fileName)
        {
            //var asm = Assembly.GetAssembly(typeof(HtmlParserTests));
            var fullPath = Path.Combine("_Test_Files", fileName + ".txt");
            return File.ReadAllText(fullPath);
        }
    }
}
