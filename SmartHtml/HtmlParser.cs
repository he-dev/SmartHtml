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
        private int _i = 0;
        private readonly string _html;
        private readonly HtmlElementCollection _elements = new HtmlElementCollection();
        private readonly Stack<string> _tagNameStack = new Stack<string>();

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
            var htmlParser = new HtmlParser(html);

            var htmlDocument = new HtmlDocument()
            {
                Elements = htmlParser._elements
            };

            return htmlDocument;
        }

        private void Parse()
        {
            AdvanceWhile(c => c.IsWhiteSpace());

            for (; _i < _html.Length; _i++)
            {
                var element = ParseElement();
                _elements.Add(element);
            }
        }

        private HtmlElement ParseElement()
        {
            if (!CurrentChar.IsOpeningAngleBracket())
            {
                throw new Exception("Malformed html.");
            }

            #region Opening tag.

            AdvanceOne(c => c.IsOpeningAngleBracket());

            var name = AdvanceWhile(c => c.IsTagNameChar());

            _tagNameStack.Push(name);
            var element = new HtmlElement() { Name = name };

            AdvanceWhile(c => c.IsWhiteSpace());

            element.Attributes = ParseAttributes();

            AdvanceWhile(c => c.IsWhiteSpace());
            AdvanceOne(c => c.IsClosingAngleBracket());

            #endregion

            while (!IsEndOfHtml)
            {
                var text = AdvanceUntil(c => c.IsOpeningAngleBracket());
                if (!string.IsNullOrEmpty(text))
                {
                    element.Elements.Add(text);
                }

                var isClosingTag = NextChar.HasValue && NextChar.Value.IsSlash();
                if (isClosingTag)
                {
                    AdvanceOne(c => c.IsOpeningAngleBracket());
                    AdvanceOne(c => c.IsSlash());
                    var closingTagName = AdvanceWhile(c => c.IsTagNameChar());
                    var isValidClosingTag = _tagNameStack.Peek() == closingTagName;
                    if (!isValidClosingTag)
                    {
                        throw new Exception("Invalid tag nesting/closing.");
                    }
                    _tagNameStack.Pop();
                    AdvanceOne(c => c.IsClosingAngleBracket());
                    break;
                }

                // Nested element.
                var nestedElement = ParseElement();
                element.Elements.Add(nestedElement);
            }

            return element;
        }

        private List<HtmlAttribute> ParseAttributes()
        {
            var attributes = new List<HtmlAttribute>();

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
                    AdvanceOne(c => c.IsClosingAngleBracket());
                    break;
                }
            }

            return attributes;
        }

        public override string ToString()
        {
            var html = Elements.Select(e => e.ToString()).Aggregate((current, next) => current + next.ToString());
            return html;
        }

        #region Utilities

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

        #endregion
    }

}
