using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    [DebuggerDisplay(@"\{TagName = {TagName} TagType = {TagType}\}")]
    internal class HtmlTagInfo
    {
        public string TagName { get; set; }

        public bool IsVoid => TagName.IsVoid();

        public TagType TagType { get; set; }        

        public string Attributes { get; set; }

        public string Text { get; set; }
    }
}
