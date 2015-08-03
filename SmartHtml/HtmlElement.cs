using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public class HtmlElement
    {
        private static readonly HashSet<string> VoidElementNames = new HashSet<string>()
        {
            "area", "base", "br", "col", "command", "embed", "hr", "img", "input",
            "keygen", "link", "meta", "param", "source", "track", "wbr"
        };

        public HtmlElement()
        {
            Attributes = new List<HtmlAttribute>();
            Elements = new HtmlElementCollection();
        }

        public HtmlElement(string tagName) : this()
        {
            Name = tagName;
        }

        public bool IsVoid
        {
            get { return VoidElementNames.Contains(Name); }
        }

        public string Name { get; set; }

        public List<HtmlAttribute> Attributes { get; set; }

        public HtmlElementCollection Elements { get; set; }

        public override string ToString()
        {
            var innerHtml = Elements.Select(e => e.ToString()).Aggregate(string.Empty, (current, next) => current + next.ToString());
            var html = new StringBuilder()
                .Append("<")
                .Append(Name)
                .Append(Attributes.Aggregate(string.Empty, (current, next) => current + " " + next.Name + (string.IsNullOrEmpty(next.Value) ? string.Empty : "=\"" + next.Value + "\"")))
                .Append(">")
                .Append(innerHtml)
                .Append(VoidElementNames.Contains(Name) ? string.Empty : "</" + Name + ">");
            return html.ToString();
        }
    }
}
