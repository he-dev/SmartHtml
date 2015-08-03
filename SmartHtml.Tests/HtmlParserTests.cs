using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHtml.Tests
{
    [TestClass]
    public class HtmlParserTests
    {
        [TestMethod]
        public void Parse_EmptyElement()
        {
            var html = "<span></span>";
            var htmlDocument = HtmlParser.Parse(html);

            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(1, htmlDocument.Elements.Count);
            Assert.IsInstanceOfType(htmlDocument.Elements[0], typeof(HtmlElement));
            Assert.AreEqual("span", (htmlDocument.Elements[0] as HtmlElement).Name);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_TwoElementsNextToEachOther()
        {
            var html = "<span>Lorem</span><span>ipsum.</span>";
            var htmlDocument = HtmlParser.Parse(html);

            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(2, htmlDocument.Elements.Count);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithText()
        {
            var html = "<span>Lorem ipsum.</span>";
            var htmlDocument = HtmlParser.Parse(html);
        }

        [TestMethod]
        public void Parse_EmptyNestedElements()
        {
            var html = "<div><span></span></div>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(1, htmlDocument.Elements.Count);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_NestedElementsWithText()
        {
            var html = "<div>Lorem <span>ipsum</span> dolor.</div>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(1, htmlDocument.Elements.Count);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithAttributes()
        {
            var html = "<span style=\"color: blue\" checked>Lorem ipsum.</span>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("<span style=\"color: blue\" checked>Lorem ipsum.</span>", htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithAttributesAndSpaces()
        {
            var html = "   <p style  =  \"color: blue\" checked><span>Lorem ipsum</span> dolor.</p>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("<p style=\"color: blue\" checked><span>Lorem ipsum</span> dolor.</p>", htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_ElementWithBr()
        {
            var html = "<p>Lorem<br>ipsum dolor.</p>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("<p>Lorem<br>ipsum dolor.</p>", htmlDocument.ToString());
        }

        [TestMethod]
        public void Parse_BrBeforeSpan()
        {
            var html = "<br><span>Lorem ipsum dolor.</span>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual(html, htmlDocument.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(MissingClosingTagException))]
        public void Parse_NotClosedSpan()
        {
            var html = "<p>Lorem<br>ipsum <span>dolor.</p>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("<p>Lorem<br>ipsum dolor.</p>", htmlDocument.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCharacterException))]
        public void Parse_DoubbleOpeningAngleBracket()
        {
            var html = "<p>Lorem<<br>ipsum <span>dolor.</p>";
            var htmlDocument = HtmlParser.Parse(html);
            Assert.AreEqual("<p>Lorem<br>ipsum dolor.</p>", htmlDocument.ToString());
        }
    }
}
