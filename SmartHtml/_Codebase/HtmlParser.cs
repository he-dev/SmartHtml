using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHtml.Dom;
using chExt = SmartHtml.CharExtensions;

namespace SmartHtml
{
    public class HtmlParser : IDisposable
    {
        private readonly Stack<ElementNode> _elementStack = new Stack<ElementNode>();

        private readonly Stack<HtmlTagInfo> _tagInfoStack = new Stack<HtmlTagInfo>();

        private readonly HtmlDocument _htmlDocument = new HtmlDocument();

        internal HtmlParser(string html)
        {
            Html = new HtmlEnumerator(html);
            _elementStack.Push(_htmlDocument.WebPage.documentElement);
        }

        private HtmlEnumerator Html { get; }

        private ElementNode CurrentElement => _elementStack.Peek();

        public static HtmlDocument Parse(string html)
        {
            using (var htmlParser = new HtmlParser(html))
            {
                htmlParser.Parse();
                return htmlParser._htmlDocument;
            }
        }

        private void Parse()
        {
            // advance to first <
            while (Html.MoveNext() && Html.Current.IsWhiteSpace()) { }

            do
            {
                var tagInfo = ParseTag();
                if (tagInfo == null)
                {
                    break;
                }

                switch (tagInfo.TagType)
                {

                case TagType.Opening:
                    {
                        if (tagInfo.IsVoid)
                        {
                            CurrentElement.childNodes.Add(new ElementNode(tagInfo));
                        }

                        // push tag info on stack for later use
                        _tagInfoStack.Push(tagInfo);

                        var element = new ElementNode(tagInfo);
                        CurrentElement.childNodes.Add(element);

                        // push html elemen on stack for later use
                        _elementStack.Push(element);

                        // consume text
                        var text = new StringBuilder().Append(Html.Current);
                        while (Html.MoveNext() && !Html.Current.IsOpeningAngleBracket())
                        {
                            text.Append(Html.Current);
                        }

                        if (text.Length > 0)
                        {
                            element.childNodes.Add(new TextNode(text.ToString()));
                        }
                    }
                    break;

                case TagType.Closing:
                    {
                        // pop tag info from stack
                        var prevTagInfo = _tagInfoStack.Pop();

                        // check if it matches the opening tag info
                        if (prevTagInfo.TagName != tagInfo.TagName)
                        {
                            throw new Exception("Malformed html.");
                        }

                        // pop last html element from stack
                        _elementStack.Pop();
                    }
                    break;
                }

            } while (!Html.IsEndOfHtml);
        }

        private HtmlTagInfo ParseTag()
        {
            var getDelimiterByLength = new Func<IEnumerable<HtmlDelimiter>, int, HtmlDelimiter>((delims, length) =>
            {
                return delims.Single(d => d.Length == length);
            });

            // read left tag delimiter

            var delimiterLength = 0;
            var currentDelimiters = HtmlGrammar.Delimiters.All;

            do
            {
                var delimiters = HtmlGrammar.Delimiters.Find(currentDelimiters, Html.Current, ++delimiterLength).ToList();
                if (delimiters.Count == 0)
                {
                    --delimiterLength;
                    break;
                }

                currentDelimiters = delimiters;
            } while (Html.MoveNext());

            var leftDelimiter = getDelimiterByLength(currentDelimiters, delimiterLength);

            if (!Html.Current.IsTagNameChar())
            {
                throw new Exception("Invalid html!");
            }

            // read tag name
            var tagName = new StringBuilder();
            do
            {
                tagName.Append(Html.Current);
            } while (Html.MoveNext() && Html.Current.IsTagNameChar());

            // read attributes
            var attributes = new StringBuilder();
            if (Html.Current.IsSpace())
            {
                while (Html.MoveNext() && !Html.Current.IsClosingAngleBracket())
                {
                    attributes.Append(Html.Current);
                }
            }

            // read right tag delimiter
            delimiterLength = 0;
            currentDelimiters = leftDelimiter.NextDelimiters.ToList();

            do
            {
                var delimiters = HtmlGrammar.Delimiters.Find(currentDelimiters, Html.Current, ++delimiterLength).ToList();
                if (delimiters.Count == 0)
                {
                    --delimiterLength;
                    break;
                }
                currentDelimiters = delimiters;
            } while (Html.MoveNext());

            var rightDelimiter = getDelimiterByLength(currentDelimiters, delimiterLength);
            if (!leftDelimiter.IsNext(rightDelimiter))
            {
                throw new Exception("Invalid html");
            }

            var tagInfo = new HtmlTagInfo
            {
                TagName = tagName.ToString(),
                TagType = HtmlGrammar.IdentifyTagType(leftDelimiter, rightDelimiter),
                // remove / on invalid self closing tags
                Attributes = attributes.ToString().TrimEnd('/')
            };

            return tagInfo;
        }

        public void Dispose()
        {
            Html?.Dispose();
        }
    }
}
