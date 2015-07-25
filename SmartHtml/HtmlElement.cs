using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public class HtmlElement
    {
        public HtmlElement()
        {
            Attributes = new List<HtmlAttribute>();
            Elements = new List<object>();
        }

        public string Name { get; set; }

        public List<HtmlAttribute> Attributes { get; set; }

        public List<object> Elements { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
