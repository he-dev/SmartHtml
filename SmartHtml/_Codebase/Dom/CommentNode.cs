using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml.Dom
{
    public class CommentNode : Node
    {
        /// <summary>
        /// Returns a Comment object with the parameter as its textual content.
        /// </summary>
        public CommentNode() : base(NodeType.COMMENT_NODE)
        {

        }
    }
}
