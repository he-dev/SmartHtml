using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    class HtmlElementFactory
    {
        public static HtmlElement CreatElement(string name)
        {
            switch (name)
            {
            case "div": return new div() { Name = name };
            case "p": return new p() { Name = name };
            case "span": return new span() { Name = name };
            default: throw new NotImplementedException(name);
            }
        }
    }
}
