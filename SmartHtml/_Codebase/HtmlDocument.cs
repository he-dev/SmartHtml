using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHtml.Dom;

namespace SmartHtml
{
    public class HtmlDocument
    {
        public HtmlElement DoctypeDeclaration { get; set; } = new HtmlElement(HtmlTagNames.doctype);

        //public HtmlElement RootElement { get; } = new HtmlElement(HtmlTagNames.html);

        public DocumentNode WebPage { get; } = new DocumentNode();

        //public override string ToString()
        //{
        //    var html = Elements.Select(e => e.ToString()).Aggregate((current, next) => current + next.ToString());
        //    return html;
        //}
    }
}
