using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml.Dom
{
    public class ElementNode : Node
    {
        public ElementNode() : base(NodeType.ELEMENT_NODE)
        {

        }

        public ElementNode(string nodeName) : this()
        {
            this.nodeName = nodeName;
        }

        internal ElementNode(HtmlTagInfo tagInfo) : this(tagInfo.TagName)
        {
            if (string.IsNullOrEmpty(tagInfo.Attributes))
            {
                attributes.Add(tagInfo.Attributes);
            }
        }

        //Returns a NamedNodeMap that lists all attributes associated with the element.
        //public IList<AttributeNode> attributes { get; set; } = new List<AttributeNode>();
        public IList<string> attributes { get; set; } = new List<string>();

        //Is a Number representing the number of child nodes that are elements.
        //ParentNode.childElementCount Read only

        //Is a live HTMLCollection containing all child elements of the element, as a collection.
        //ParentNode.children Read only

        //Returns a DOMTokenList containing the list of class attributes.
        //Element.classList Read only

        //Is a DOMString representing the class of the element.
        //Element.className

        //Element.id
        //Is a DOMString representing the id of the element.

        //Is a DOMString representing the markup of the element's content.
        //Element.innerHTML

        //Returns a String with the name of the tag for the given element.
        //Element.tagName Read only
    }
}
