using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace SmartHtml.Dom
{
    public enum NodeType
    {
        /// <summary>
        /// Represents an element
        /// </summary>
        ELEMENT_NODE = 1,

        /// <summary>
        /// Represents an attribute
        /// </summary>
        [Obsolete]
        ATTRIBUTE_NODE,

        /// <summary>
        /// Represents textual content in an element or attribute
        /// </summary>
        TEXT_NODE,

        /// <summary>
        /// Represents a CDATA section in a document (text that will NOT be parsed by a parser)
        /// </summary>
        [Obsolete]
        CDATA_SECTION_NODE,

        /// <summary>
        /// Represents an entity reference
        /// </summary>
        [Obsolete]
        ENTITY_REFERENCE_NODE,

        /// <summary>
        /// Represents an entity
        /// </summary>
        [Obsolete]
        ENTITY_NODE,

        /// <summary>
        /// Represents a processing instruction
        /// </summary>
        PROCESSING_INSTRUCTION_NODE,

        /// <summary>
        /// Represents a comment
        /// </summary>
        COMMENT_NODE,

        /// <summary>
        /// Represents the entire document (the root-node of the DOM tree)
        /// </summary>
        DOCUMENT_NODE,

        /// <summary>
        /// Provides an interface to the entities defined for the document
        /// </summary>
        DOCUMENT_TYPE_NODE,

        /// <summary>
        /// Represents a "lightweight" Document object, which can hold a portion of a document
        /// </summary>
        DOCUMENT_FRAGMENT_NODE,

        /// <summary>
        /// Represents a notation declared in the DTD
        /// </summary>
        [Obsolete]
        NOTATION_NODE,
    }
}
