using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    [DebuggerDisplay(@"\{Name = {Name} Contents = {Contents.Count}\}")]
    public class HtmlElement
    {
        public HtmlElement(string name)
        {
            Name = name;
        }

        internal HtmlElement(HtmlTagInfo tagInfo)
        {
            Name = tagInfo.TagName;
            if (!string.IsNullOrEmpty(tagInfo.Text))
            {
                Contents.Add(tagInfo.Text);
            }
        }

        public string Name { get; }

        public bool IsVoid => Name.IsVoid();

        public List<HtmlAttribute> Attributes { get; set; } = new List<HtmlAttribute>();

        public List<object> Contents { get; set; } = new List<object>();

        //public override string ToString()
        //{
        //    var innerHtml = Elements.Select(e => e.ToString()).Aggregate(string.Empty, (current, next) => current + next.ToString());
        //    var html = new StringBuilder()
        //        .Append("<")
        //        .Append(Name)
        //        .Append(Attributes.Aggregate(string.Empty, (current, next) => current + " " + next.Name + (string.IsNullOrEmpty(next.Value) ? string.Empty : "=\"" + next.Value + "\"")))
        //        .Append(">")
        //        .Append(innerHtml)
        //        .Append(VoidElementNames.Contains(Name) ? string.Empty : "</" + Name + ">");
        //    return html.ToString();
        //}
    }
}
