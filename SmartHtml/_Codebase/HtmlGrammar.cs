using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    [DebuggerDisplay(@"\{Delimiter = {_delimiter}\}")]
    public class HtmlDelimiter
    {
        private readonly string _delimiter;

        public HtmlDelimiter(string delimiter)
        {
            _delimiter = delimiter;
        }

        public char this[int index] => _delimiter[index];

        public int Length => _delimiter.Length;

        internal HtmlDelimiter[] NextDelimiters { get; set; }

        public bool Contains(char c, int delimiterLength)
        {
            return delimiterLength <= _delimiter.Length && _delimiter[delimiterLength - 1] == c;
        }

        public bool IsNext(HtmlDelimiter delimiter)
        {
            return NextDelimiters.SingleOrDefault(d => d == delimiter) != null;
        }

        public static implicit operator string (HtmlDelimiter htmlDelimiter)
        {
            return htmlDelimiter._delimiter;
        }
    }

    class HtmlGrammar
    {
        public class Delimiters
        {
            public static readonly HtmlDelimiter OpenTagName = new HtmlDelimiter("<");
            public static readonly HtmlDelimiter CloseTagName = new HtmlDelimiter(">");

            public static readonly HtmlDelimiter Doctype = new HtmlDelimiter("<!DOCTYPE");
            public static readonly HtmlDelimiter OpenCloseTagName = new HtmlDelimiter("</");
            public static readonly HtmlDelimiter SelfCloseTagName = new HtmlDelimiter("/>");
            public static readonly HtmlDelimiter OpenComment = new HtmlDelimiter("<!--");
            public static readonly HtmlDelimiter CloseComment = new HtmlDelimiter("-->");

            static Delimiters()
            {
                OpenTagName.NextDelimiters = new[] { CloseTagName, SelfCloseTagName };
                CloseTagName.NextDelimiters = new[] { OpenCloseTagName, OpenTagName };
                OpenCloseTagName.NextDelimiters = new[] { CloseTagName };
            }

            public static IList<HtmlDelimiter> All = new List<HtmlDelimiter>
            {
                OpenTagName,
                CloseTagName,
                SelfCloseTagName,
                OpenCloseTagName
            };

            public static IEnumerable<HtmlDelimiter> Find(IEnumerable<HtmlDelimiter> delimiters, char c, int delimiterLength)
            {
                var result = delimiters.Where(d => d.Contains(c, delimiterLength));
                return result;
            }
        }

        public static readonly IDictionary<Tuple<HtmlDelimiter, HtmlDelimiter>, TagType> DelimiterPairs = new Dictionary<Tuple<HtmlDelimiter, HtmlDelimiter>, TagType>
        {
            // <abc>
            { new Tuple<HtmlDelimiter, HtmlDelimiter>(Delimiters.OpenTagName, Delimiters.CloseTagName), TagType.Opening },
            // <abc/>
            { new Tuple<HtmlDelimiter, HtmlDelimiter>(Delimiters.OpenTagName, Delimiters.SelfCloseTagName), TagType.Opening },
            // </abc>
            { new Tuple<HtmlDelimiter, HtmlDelimiter>(Delimiters.OpenCloseTagName, Delimiters.CloseTagName), TagType.Closing },
            // <!-- abc -->
            { new Tuple<HtmlDelimiter, HtmlDelimiter>(Delimiters.OpenComment, Delimiters.CloseComment), TagType.Void },
        };

        public static TagType IdentifyTagType(HtmlDelimiter d1, HtmlDelimiter d2)
        {
            return DelimiterPairs[new Tuple<HtmlDelimiter, HtmlDelimiter>(d1, d2)];
        }
    }
}
