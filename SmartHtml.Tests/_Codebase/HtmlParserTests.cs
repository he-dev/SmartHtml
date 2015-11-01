using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartHtml.Dom;

namespace SmartHtml.Tests
{
    [TestClass]
    public class HtmlParserTests
    {
        [TestMethod]
        public void Parse_EmptyElement()
        {
            var html = TestHelper.ReadAllText("EmptyElement");
            var htmlDoc = HtmlParser.Parse(html);

            Assert.IsNotNull(htmlDoc);
            Assert.AreEqual(1, htmlDoc.WebPage.documentElement.childNodes.Count);

            var span = htmlDoc.WebPage.documentElement.childNodes.OfType<ElementNode>().First();
            Assert.AreEqual("span", span.nodeName);
        }

        [TestMethod]
        public void Parse_ElementWithText()
        {
            var html = TestHelper.ReadAllText("ElementWithText");
            var htmlDoc = HtmlParser.Parse(html);
            Assert.IsNotNull(htmlDoc);
            Assert.AreEqual(1, htmlDoc.WebPage.documentElement.childNodes.Count);

            var span = htmlDoc.WebPage.documentElement.childNodes.OfType<ElementNode>().First();
            Assert.AreEqual("span", span.nodeName);
            Assert.AreEqual("Lorem ipsum.", span.childNodes.OfType<TextNode>().First().text);
        }

        [TestMethod]
        public void Parse_TwoElementsNextToEachOther()
        {
            var html = TestHelper.ReadAllText("TwoElementsNextToEachOther");
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
            var html = TestHelper.ReadAllText("NestedElementsWithText");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithAttributes()
        {
            var html = TestHelper.ReadAllText("ElementWithAttributes");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("<span id=\"abc\" style=\"color: blue\" class>Lorem ipsum.</span>", htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithAttributesAndSpaces()
        {
            var html = TestHelper.ReadAllText("ElementWithAttributesAndSpaces");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("   <p style=\"color: blue\" class><span>Lorem ipsum</span> dolor.</p>", htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_VoidElement()
        {
            var html = TestHelper.ReadAllText("VoidElement");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_VoidElementBeforeOtherElement()
        {
            var html = TestHelper.ReadAllText("VoidElementBeforeOtherElement");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_MultilineHtml()
        {
            var html = TestHelper.ReadAllText("MultilineHtml1");
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        #region Test invalid HTML

        [TestMethod]
        [ExpectedException(typeof(MissingClosingTagException))]
        public void Parse_UnclosedElement()
        {
            var html = TestHelper.ReadAllText("UnclosedElement");
            var htmlDocument = HtmlParser.Parse(html);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterException))]
        public void Parse_DoubbleOpeningAngleBracket()
        {
            var html = TestHelper.ReadAllText("DoubbleOpeningAngleBracket");
            var htmlDocument = HtmlParser.Parse(html);
        }

        #endregion


    }
}
