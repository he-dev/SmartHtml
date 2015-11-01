using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace SmartHtml.Dom
{
    public class TextNode : Node
    {
        // Returns a Text node with the parameter as its textual content.
        public TextNode() : base(NodeType.TEXT_NODE)
        {

        }

        public TextNode(string text) : this()
        {
            this.text = text;
        }

        //Returns a Boolean flag indicating whether or not the text node contains only whitespace.
        public bool isElementContentWhitespace { get; }

        //Returns a DOMString containing the text of all Text nodes logically adjacent to this Node, concatenated in document order.
        //Text.wholeText Read only

        public string text { get; set; }
    }
}
