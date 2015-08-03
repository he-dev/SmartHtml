using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chExt = SmartHtml.CharExtensions;

namespace SmartHtml
{
    public class HtmlParser
    {
        private int _line = 1;
        private int _column = 1;
        private int _i = 0;
        private readonly string _html;
        private readonly HtmlElementCollection _elements = new HtmlElementCollection();
        private readonly Stack<HtmlElement> _openHtmlElementStack = new Stack<HtmlElement>();

        private HtmlParser(string html)
        {
            _html = html;
        }

        private char PreviousChar
        {
            get { return _html[_i - 1]; }
        }

        private char CurrentChar
        {
            get { return _html[_i]; }
        }

        private char? NextChar
        {
            get
            {
                if (_i + 1 < _html.Length)
                {
                    return _html[_i + 1];
                }
                return null;
            }
        }

        private bool IsEndOfHtml
        {
            get { return _i >= _html.Length; }
        }

        public static HtmlDocument Parse(string html)
        {
            var htmlParser = new HtmlParser(html.Trim());
            htmlParser.Parse();

            var htmlDocument = new HtmlDocument()
            {
                Elements = htmlParser._elements
            };

            return htmlDocument;
        }

        private void Parse()
        {
            AdvanceWhile(c => c.IsWhiteSpace());

            while (!IsEndOfHtml)
            {
                var htmlElement = ParseTag();
                if (htmlElement != null)
                {
                    _elements.Add(htmlElement);
                }
            }
        }

        private HtmlElement ParseTag()
        {
            AdvanceOne(c => c.IsOpeningAngleBracket());

            var isClosingTag = CurrentChar.IsSlash();
            return
                isClosingTag
                ? ParseClosingTag()
                : ParseOpeningTag();
        }

        private HtmlElement ParseOpeningTag()
        {
            var tagName = AdvanceWhile(c => c.IsTagNameChar());
            if (string.IsNullOrEmpty(tagName))
            {
                throw new Exception("Invalid charachter at " + (_i + 1) + ".");
            }

            var htmlElement = new HtmlElement(tagName);
            var parentHtmlElement = _openHtmlElementStack.Count > 0 ? _openHtmlElementStack.Peek() : null;
            if (parentHtmlElement != null)
            {
                parentHtmlElement.Elements.Add(htmlElement);
            }
            if (!htmlElement.IsVoid)
            {
                _openHtmlElementStack.Push(htmlElement);
            }

            htmlElement.Attributes = ParseAttributes();

            var text = AdvanceUntil(c => c.IsClosingAngleBracket());
            if (!string.IsNullOrWhiteSpace(text))
            {
                // Check for invalid self closed void elements.
                text = text.Trim();
                var isSelfClosed = htmlElement.IsVoid && text.Length == 1 && text.Single().IsSlash();
                if (!isSelfClosed)
                {
                    throw new Exception("Malformed html. Invalid chars between tagName/attributes and closing angle bracket.");
                }
            }
            AdvanceOne(c => c.IsClosingAngleBracket());

            text = AdvanceUntil(c => c.IsOpeningAngleBracket());
            if (!string.IsNullOrEmpty(text))
            {
                htmlElement.Elements.Add(text);
            }

            if (htmlElement.IsVoid)
            {
                return null;
            }

            return null;
        }

        private HtmlElement ParseClosingTag()
        {
            AdvanceOne(c => c.IsSlash());
            var closingTagName = AdvanceWhile(c => c.IsTagNameChar());

            var htmlElement = _openHtmlElementStack.Peek();
            var isValidClosingTag = htmlElement.Name == closingTagName;
            if (!isValidClosingTag)
            {
                throw new MissingClosingTagException(_html, _i, htmlElement.Name);
            }
            _openHtmlElementStack.Pop();
            AdvanceOne(c => c.IsClosingAngleBracket());

            var text = AdvanceUntil(c => c.IsOpeningAngleBracket());
            if (!string.IsNullOrEmpty(text))
            {
                htmlElement = _openHtmlElementStack.Peek();
                htmlElement.Elements.Add(text);
            }

            return
                _openHtmlElementStack.Count == 0
                ? htmlElement
                : null;
        }

        private List<HtmlAttribute> ParseAttributes()
        {
            var attributes = new List<HtmlAttribute>();

            AdvanceWhile(c => c.IsWhiteSpace());

            if (CurrentChar.IsClosingAngleBracket())
            {
                return attributes;
            }

            while (true)
            {
                var name = AdvanceWhile(c => c.IsAttributeNameChar());
                var value = string.Empty;

                AdvanceWhile(c => c.IsWhiteSpace());

                var hasValue = CurrentChar.IsEqualsSign();
                if (hasValue)
                {
                    AdvanceOne(c => c.IsEqualsSign());

                    var isUnquotedValue = CurrentChar.IsLetter();
                    if (isUnquotedValue)
                    {
                        value = AdvanceWhile(c => c.IsUnquotedValueChar());
                    }
                    else // Quoted value.
                    {
                        AdvanceWhile(c => c.IsWhiteSpace());
                        AdvanceOne(c => c.IsDoubleQuotationMark() || c.IsSingleQuotationMark());
                        value = AdvanceUntil(c => c.IsDoubleQuotationMark() || c.IsSingleQuotationMark());
                        AdvanceOne(c => c.IsDoubleQuotationMark() || c.IsSingleQuotationMark());
                    }
                }

                attributes.Add(new HtmlAttribute()
                {
                    Name = name,
                    Value = value
                });

                AdvanceWhile(c => c.IsWhiteSpace());

                if (CurrentChar.IsClosingAngleBracket() || IsEndOfHtml)
                {
                    //AdvanceOne(c => c.IsClosingAngleBracket());
                    break;
                }
            }

            return attributes;
        }

        public override string ToString()
        {
            var html = _elements.Select(e => e.ToString()).Aggregate((current, next) => current + next.ToString());
            return html;
        }

        #region Helpers

        private string AdvanceWhile(Func<char, bool> predicate)
        {
            return _html.AdvanceWhile(predicate, ref _i);
        }

        private string AdvanceUntil(Func<char, bool> predicate)
        {
            return _html.AdvanceUntil(predicate, ref _i);
        }

        private string AdvanceOne(Func<char, bool> predicate)
        {
            return _html.AdvanceOne(predicate, ref _i);
        }

        private bool CheckNext(Func<char, bool> predicate)
        {
            return
                NextChar.HasValue
                ? predicate(NextChar.Value)
                : false;
        }

        #endregion
    }

}
