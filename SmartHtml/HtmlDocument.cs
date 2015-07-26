using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chExt = SmartHtml.CharExtensions;

namespace SmartHtml
{
    public class HtmlDocument
    {
        public HtmlElementCollection Elements { get; set; }

        public override string ToString()
        {
            var html = Elements.Select(e => e.ToString()).Aggregate((current, next) => current + next.ToString());
            return html;
        }

    }

}
