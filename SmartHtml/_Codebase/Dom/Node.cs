using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace SmartHtml.Dom
{
    public abstract class Node
    {
        protected IList<Node> _nodes = new List<Node>();

        protected Node(NodeType nodeType)
        {
            this.nodeType = nodeType;
        }

        //Returns a DOMString representing the base URL.The concept of base URL changes from one language to another; in HTML, it corresponds to the protocol, the domain name and the directory structure, that is all until the last '/'.
        //Node.baseURI Read only

        //(Not available to web content.) The read-only nsIURI object representing the base URI for the element.
        //Node.baseURIObject 

        //Returns a live NodeList containing all the children of this node.NodeList being live means that if the children of the Node change, the NodeList object is automatically updated.
        public IList<Node> childNodes { get; set; } = new List<Node>();

        //Returns a Node representing the first direct child node of the node, or null if the node has no child.
        public Node firstChild { get; }

        //Returns a Node representing the last direct child node of the node, or null if the node has no child.
        public Node lastChild { get; }

        //Returns a DOMString representing the local part of the qualified name of an element.In Firefox 3.5 and earlier, the property upper-cases the local name for HTML elements (but not XHTML elements). In later versions, this does not happen, so the property is in lower case for both HTML and XHTML.
        // Though recent specifications require localName to be defined on the Element interface, Gecko-based browsers still implement it on the Node interface.
        //Node.localName Read only

        //The namespace URI of this node, or null if it is no namespace.In Firefox 3.5 and earlier, HTML elements are in no namespace.In later versions, HTML elements are in the http://www.w3.org/1999/xhtml namespace in both HTML and XML trees. 
        //Though recent specifications require namespaceURI to be defined on the Element interface, Gecko-based browsers still implement it on the Node interface.
        //Node.namespaceURI Read only

        //Returns a Node representing the next node in the tree, or null if there isn't such node.
        //Node.nextSibling Read only

        /// <summary>
        /// Returns a DOMString containing the name of the Node.The structure of the name will differ with the name type. 
        /// E.g.An HTMLElement will contain the name of the corresponding tag, like 'audio' for an HTMLAudioElement, 
        /// a Text node will have the '#text' string, or a Document node will have the '#document' string.
        /// </summary>
        public string nodeName { get; internal set; }

        //A nsIPrincipal representing the node principal.
        //Node.nodePrincipal

        /// <summary>
        /// Returns an unsigned short representing the type of the node.
        /// </summary>
        public NodeType nodeType { get; internal set; }


        //Is a DOMString representing the value of an object. For most Node types, this returns null and any set operation is ignored.For nodes of type TEXT_NODE (Text objects), COMMENT_NODE(Comment objects), and PROCESSING_INSTRUCTION_NODE(ProcessingInstruction objects), the value corresponds to the text data contained in the object.
        //Node.nodeValue

        //Returns the Document that this node belongs to.If no document is associated with it, returns null.
        public HtmlDocument ownerDocument { get; }

        /// <summary>
        /// Returns a Node that is the parent of this node. If there is no such node, 
        /// like if this node is the top of the tree or if doesn't participate in a tree, 
        /// this property returns null.
        /// </summary>
        /// <remarks>Documents don't have a parent node</remarks>
        public Node parentNode { get; internal set; }

        //Is a DOMString representing the namespace prefix of the node, or null if no prefix is specified.
        //Though recent specifications require prefix to be defined on the Element interface, Gecko-based browsers still implement it on the Node interface.
        //Node.prefix Read only

        //Returns a Node representing the previous node in the tree, or null if there isn't such node.
        //Node.previousSibling Read only

        //Is a DOMString representing the textual content of an element and all its descendants.
        //Node.textContent

        //Methods

        //Insert a Node as the last child node of this element.
        //Node.appendChild()

        //Clone a Node, and optionally, all of its contents. By default, it clones the content of the node.
        //Node.cloneNode()

        //Node.compareDocumentPosition()

        //Node.contains()

        //Node.getFeature() 


        //Allows a user to get some DOMUserData from the node.
        //Node.getUserData() 

        //Returns a Boolean indicating if the element has any attributes, or not.
        //Node.hasAttributes()

        //Returns a Boolean indicating if the element has any child nodes, or not.
        //Node.hasChildNodes()

        //Inserts the first Node given in a parameter immediately before the second, child of this element, Node.
        //Node.insertBefore()

        //Node.isDefaultNamespace()

        //Node.isEqualNode()

        //Node.isSameNode()

        //Returns a Boolean flag containing the result of a test whether the DOM implementation implements a specific feature and this feature is supported by the specific node.
        //Node.isSupported()

        //Node.lookupPrefix()

        //Node.lookupNamespaceURI()

        //Clean up all the text nodes under this element(merge adjacent, remove empty).
        //Node.normalize()

        //Removes a child node from the current element, which must be a child of the current node.
        //Node.removeChild()

        //Replaces one child Node of the current one with the second one given in parameter.
        //Node.replaceChild()

        //Allows a user to attach, or remove, DOMUserData to the node.
        //Node.setUserData()

    }
}
