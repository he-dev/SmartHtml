using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chExt = SmartHtml.CharExtensions;

namespace SmartHtml
{
    public class HtmlParser
    {
        private int _index = 0;
        private readonly string _html;
        private readonly HtmlElementCollection _elements = new HtmlElementCollection();
        private readonly Stack<HtmlElement> _openHtmlElements = new Stack<HtmlElement>();

        private HtmlParser(string html)
        {
            _html = html;
        }

        private char PreviousChar
        {
            get { return _html[_index - 1]; }
        }

        private char CurrentChar
        {
            get { return _html[_index]; }
        }

#if DEBUG
        private string HtmlRead => !IsEndOfHtml ? _html.Substring(0, _index + 1) : null;
#endif

        private char? NextChar
        {
            get
            {
                if (_index + 1 < _html.Length)
                {
                    return _html[_index + 1];
                }
                return null;
            }
        }

        private bool IsEndOfHtml
        {
            get { return _index >= _html.Length; }
        }

        public static HtmlDocument Parse(string html)
        {
            var htmlParser = new HtmlParser(html);
            htmlParser.Parse();

            var htmlDocument = new HtmlDocument()
            {
                Elements = htmlParser._elements
            };

            return htmlDocument;
        }

        private void Parse()
        {
            while (!IsEndOfHtml)
            {
                ParseTag();
            }
        }

        private void ParseTag()
        {
            var text = ReadUntil(c => c.IsOpeningAngleBracket(), LeadingWhitespace.Select);
            AddText(text);

            text = ReadExact(c => c.IsOpeningAngleBracket(), LeadingWhitespace.Ignore);

            var isOpeningTag = CurrentChar.IsLetter();
            if (isOpeningTag)
            {
                ParseOpeningTag();
                return;
            }

            var isClosingTag = CurrentChar.IsSlash();
            if (isClosingTag)
            {
                ParseClosingTag();         
                return;
            }

            throw new InvalidCharacterException(_html, _index);
        }

        private void ParseOpeningTag()
        {
            var openingTagName = ReadWhile(c => c.IsTagNameChar());

            if (string.IsNullOrEmpty(openingTagName))
            {
                throw new InvalidCharacterException(_html, _index);
            }

            var htmlElement = new HtmlElement(openingTagName);

            var attributes = ReadUntil(c => c.IsClosingAngleBracket(), LeadingWhitespace.Ignore);
            ParseAttributes(htmlElement, attributes);

            // Read closing angle bracket and the optional slash.
            var text = ReadWhile(c => c.IsSlash() || c.IsClosingAngleBracket());
            var isClosingBracket = text == "/>" || text == ">";

            // Advance to the next opening '<'
            text = ReadUntil(c => c.IsOpeningAngleBracket());

            // We've found more text.
            if (!string.IsNullOrEmpty(text))
            {
                htmlElement.Elements.Add(text);
            }

            // Decide what we should do with this tag.

            var isChildElement = _openHtmlElements.Count > 0;
            if (isChildElement)
            {
                // Add itself to parent.
                _openHtmlElements.Peek().Elements.Add(htmlElement);
            }

            if (htmlElement.HasClosingTag)
            {
                // We need to look for the closing tag so push to the stack.
                _openHtmlElements.Push(htmlElement);
            }

            // We're done with this tag if it's not a child and we don't have to look for the closing tag.
            if (!isChildElement && !htmlElement.HasClosingTag)
            {
                _elements.Add(htmlElement);
            }
        }

        private void ParseClosingTag()
        {
            var text = ReadExact(c => c.IsSlash(), LeadingWhitespace.Select);
            var closingTagName = ReadWhile(c => c.IsTagNameChar());

            // Get last open html element:
            var htmlElement = _openHtmlElements.Peek();

            // Does the current closing tag name match the last open html element name?
            var isValidClosingTag = htmlElement.Name == closingTagName;
            if (!isValidClosingTag)
            {
                throw new MissingClosingTagException(_html, _index, htmlElement.Name);
            }

            // Everythink is fine. Remove the last open element from the stack. It's closed now.
            _openHtmlElements.Pop();

            text = ReadExact(c => c.IsClosingAngleBracket(), LeadingWhitespace.Select);

            // Are there any others open html elements?
            var isRoot = _openHtmlElements.Count == 0;

            // No? Then this is not a nested html element.
            if (isRoot) _elements.Add(htmlElement);
        }

        private void ParseAttributes(HtmlElement htmlElement, string attributes)
        {
            // Apparently there are no attributes but just an empty space.
            if (string.IsNullOrWhiteSpace(attributes)) return;

            var index = 0;
            while (index < attributes.Length)
            {
                // abc
                var name = attributes.ReadWhile(c => c.IsAttributeNameChar(), ref index, LeadingWhitespace.Ignore);

                var attr = new HtmlAttribute() { Name = name };
                htmlElement.Attributes.Add(attr);

                var text = attributes.ReadExact(c => c.IsEqualsSign(), ref index);

                if (string.IsNullOrEmpty(text)) break;

                #region Parse value

                var hasValue = text[0].IsEqualsSign();
                if (hasValue)
                {
                    var quotationMark = attributes.ReadExact(c => c.IsDoubleQuotationMark() || c.IsSingleQuotationMark(), ref index, LeadingWhitespace.Ignore);
                    var isQuotedValue = !string.IsNullOrEmpty(quotationMark) && quotationMark.Length == 1;
                    if (isQuotedValue)
                    {
                        attr.Value = attributes.ReadUntil(c => c == quotationMark[0], ref index, LeadingWhitespace.Select);
                        text = attributes.ReadExact(c => c == quotationMark[0], ref index, LeadingWhitespace.Ignore);
                    }
                    else
                    {
                        attr.Value = attributes.ReadWhile(c => c.IsLetter(), ref index, LeadingWhitespace.Ignore);
                    }
                }

                #endregion
            }
        }

        private void AddText(string text)
        {
            if (string.IsNullOrEmpty(text)) text = string.Empty;

            var lastHtmlElement = _openHtmlElements.Count > 0 ? _openHtmlElements.Peek() : null;
            if (lastHtmlElement != null)
            {
                lastHtmlElement.Elements.Add(text);
            }
            else
            {
                _elements.Add(text);
            }
        }

        public override string ToString()
        {
            var html = _elements.Select(e => e.ToString()).Aggregate((current, next) => current + next.ToString());
            return html;
        }

        #region Helpers        

        private string ReadWhile(Func<char, bool> predicate, LeadingWhitespace leadingWhitespace = LeadingWhitespace.Ignore)
        {
            return _html.Read(predicate, ref _index, leadingWhitespace);
        }

        private string ReadUntil(Func<char, bool> predicate, LeadingWhitespace leadingWhitespace = LeadingWhitespace.Select)
        {
            return _html.Read(c => !predicate(c), ref _index, leadingWhitespace);
        }

        private string ReadExact(Func<char, bool> predicate, LeadingWhitespace leadingWhitespace = LeadingWhitespace.Ignore)
        {
            return _html.ReadExact(predicate, ref _index, leadingWhitespace);
        }        

        #endregion
    }

}
