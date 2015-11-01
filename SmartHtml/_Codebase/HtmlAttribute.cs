using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public class HtmlAttribute
    {
        public HtmlElement ParentElement { get; internal set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
