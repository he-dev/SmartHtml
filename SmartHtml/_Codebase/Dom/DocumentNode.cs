using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace SmartHtml.Dom
{
    // https://developer.mozilla.org/en-US/docs/Web/API/Document

    public class DocumentNode : Node
    {
        public DocumentNode() : base(NodeType.DOCUMENT_NODE)
        {
            nodeName = "#document";
        }

        //Returns the Content-Type from the MIME Header of the current document.
        public string contentType { get; }

        //Returns the Document Type Definition(DTD) of the current document.
        public string doctype { get; }

        //Returns the Element that is a direct child of the document. For HTML documents, this is normally the HTML element.
        public ElementNode documentElement { get; } = new ElementNode(HtmlTagNames.html);

        // Returns a live HTMLCollection containing all objects of type Element that are children of this ParentNode.
        public IEnumerable<ElementNode> children { get; }

        //Returns the Element that is the first child of this ParentNode, or null if there is none.
        //ParentNode.firstElementChild Read only

        //Returns the Element that is the last child of this ParentNode, or null if there is none.
        //ParentNode.lastElementChild Read only

        //Returns an unsigned long giving the amount of children that the object has.
        //ParentNode.childElementCount Read only


        // Extension for HTML document

        //Returns the<head> element of the current document.
        public ElementNode head { get; }

        //Returns the<body> element of the current document.
        public ElementNode body { get; set; }

        //Returns a list of the images in the current document.
        //Document.images Read only

        //Returns a list of all the hyperlinks in the document.
        //Document.links Read only

        //Returns all the<script> elements on the document.
        //Document.scripts Read only

        //Returns the title of the current document.
        //Document.title Read only

        // Method
    }
}
