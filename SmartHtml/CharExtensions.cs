using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public static class CharExtensions
    {
        private static readonly HashSet<uint> UpperCaseChars = new HashSet<uint>(Enumerable.Repeat(65, 90 - 65).Select((i, x) => (uint)(x + i)));
        private static readonly HashSet<uint> LowerCaseChars = new HashSet<uint>(Enumerable.Repeat(97, 122 - 97).Select((i, x) => (uint)(x + i)));
        private static readonly HashSet<uint> Numbers = new HashSet<uint>(Enumerable.Repeat(48, 10).Select((i, x) => (uint)(x + i)));

        private static readonly HashSet<uint> TagNameChars = new HashSet<uint>(UpperCaseChars.Concat(LowerCaseChars.Concat(Numbers)));
        private static readonly HashSet<uint> AttributeNameChars = new HashSet<uint>(UpperCaseChars.Concat(LowerCaseChars.Concat(Numbers)));

        private static readonly HashSet<uint> ForbiddenUnquotedValueChars = new HashSet<uint>() { '"', '\'', '`', '=', '<', '>' };

        public static bool IsLetter(this char value)
        {
            return UpperCaseChars.Contains(value) || LowerCaseChars.Contains(value);
        }

        public static bool IsOpeningAngleBracket(this char value)
        {
            return value == '<';
        }

        public static bool IsClosingAngleBracket(this char value)
        {
            return value == '>';
        }

        public static bool IsSlash(this char value)
        {
            return value == '/';
        }

        public static bool IsSpace(this char value)
        {
            return value == ' ';
        }

        public static bool IsWhiteSpace(this char value)
        {
            return char.IsWhiteSpace(value);
        }

        public static bool IsEqualsSign(this char value)
        {
            return value == '=';
        }

        public static bool IsBackslash(this char value)
        {
            return value == '\\';
        }

        public static bool IsSingleQuotationMark(this char value)
        {
            return value == '\'';
        }

        public static bool IsDoubleQuotationMark(this char value)
        {
            return value == '\"';
        }

        public static bool IsAttributeNameChar(this char value)
        {
            return AttributeNameChars.Contains(value);
        }

        public static bool IsTagNameChar(this char value)
        {
            return AttributeNameChars.Contains(value);
        }

        public static bool IsUnquotedValueChar(this char value)
        {
            return !char.IsWhiteSpace(value) && !ForbiddenUnquotedValueChars.Contains(value);
        }

        public static bool Or(this char value, params Func<char, bool>[] predicateFuncs)
        {
            return predicateFuncs.Any(func => func(value));
        }
    }
}
