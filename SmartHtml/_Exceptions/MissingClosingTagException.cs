using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public class MissingClosingTagException : HtmlException
    {
        public MissingClosingTagException(string html, int index, string tagName) : base(html, index)
        {
            TagName = tagName;
        }

        public string TagName { get; private set; }

        public override string Message
        {
            get
            {
                return "Element [" + TagName + "] is without closing tag.";
            }
        }
    }
}
