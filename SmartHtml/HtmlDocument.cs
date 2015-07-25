using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using chExt = SmartHtml.CharExtensions;

namespace SmartHtml
{
    // <span>Lorem ipsum.<span>

    public class HtmlDocument
    {
        private int _i = 0;

        private string _html;

        private readonly Stack<string> _tagNameStack = new Stack<string>();

        private HtmlDocument(string html)
        {
            _html = html;

            Elements = new List<HtmlElement>();

            AdvanceWhile(c => c.IsWhiteSpace());

            for (; _i < html.Length; _i++)
            {
                var element = ParseElement();
                Elements.Add(element);
                //tagStack.Push(tagName);
            }
        }

        private char PreviousChar
        {
            get { return _html[_i - 1]; }
        }

        private char CurrentChar
        {
            get { return _html[_i]; }
        }

        private bool IsEndOfHtml
        {
            get { return _i >= _html.Length; }
        }

        public List<HtmlElement> Elements { get; set; }

        public static HtmlDocument Parse(string html)
        {
            return new HtmlDocument(html);
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
            var element = new HtmlElement() { Name = name };// HtmlElementFactory.CreatElement(name);

            AdvanceWhile(c => c.IsWhiteSpace());

            element.Attributes = ParseAttributes();

            AdvanceWhile(c => c.IsWhiteSpace());
            AdvanceOne(c => c.IsClosingAngleBracket());

            #endregion

            var text = AdvanceUntil(c => c.IsOpeningAngleBracket());
            //AdvanceOne(c => c.IsOpeningAngleBracket());

            var isClosingTag = CurrentChar.IsSlash();
            if (isClosingTag)
            {
                AdvanceOne(c => c.IsSlash());
                var closingTagName = AdvanceWhile(c => c.IsTagNameChar());
                var isValidClosingTag = _tagNameStack.Peek() == closingTagName;
                if (!isValidClosingTag)
                {
                    throw new Exception("Invalid tag nesting/closing.");
                }
                _tagNameStack.Pop();
                AdvanceOne(c => c.IsClosingAngleBracket());
            }
            else // Nested element.
            {

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

    }
    
}
