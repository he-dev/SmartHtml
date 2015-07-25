using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHtml.Tests
{
    [TestClass]
    public class HtmlDocumentTests
    {
        [TestMethod]
        public void Parse_EmpyElement()
        {
            var html = "<span></span>";
            var htmlDocument = HtmlDocument.Parse(html);
            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(1, htmlDocument.Elements.Count);
            Assert.AreEqual("span", htmlDocument.Elements.First().Name);
            Assert.AreEqual(0, htmlDocument.Elements.First().Elements.Count);
        }

        [TestMethod]
        public void Parse_NestedElements()
        {
            var html = "<div>Lorem <span>ipsum</span> dolor.</div>";
            var htmlDocument = HtmlDocument.Parse(html);
            Assert.IsNotNull(htmlDocument);
            Assert.AreEqual(1, htmlDocument.Elements.Count);
            Assert.AreEqual("span", htmlDocument.Elements.First().Name);
            Assert.AreEqual(0, htmlDocument.Elements.First().Elements.Count);
        }

        [TestMethod]
        public void Parse_ElementWithText()
        {
            var html = "<span>Lorem ipsum.</span>";
            var htmlDocument = HtmlDocument.Parse(html);
        }

        [TestMethod]
        public void Parse_ElementWithAttributes()
        {
            var html = "<span style  =  \"color: blue\" checked>Lorem ipsum.</span>";
            var htmlDocument = HtmlDocument.Parse(html);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var html = "   <p style  =  \"color: blue\" checked><span>Lorem ipsum</span> dolor.</p>";
            var htmlDocument = HtmlDocument.Parse(html);
        }
    }
}
