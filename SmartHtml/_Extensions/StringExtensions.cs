using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public static class StringExtensions
    {
        public static string Extract(this string value, int startIndex, int endIndex)
        {
            return value.Substring(startIndex, endIndex - startIndex);
        }       

        internal static string Read(this string value, Func<char, bool> predicate, ref int index, LeadingWhitespace leadingWhitespace = LeadingWhitespace.Ignore)
        {
            var checkWhitespace = true;
            var result = new StringBuilder();
            for (; index < value.Length; index++)
            {
                var currentChar = value[index];

                if (checkWhitespace && currentChar.IsWhiteSpace())
                {
                    switch (leadingWhitespace)
                    {
                    case LeadingWhitespace.Ignore: continue;
                    case LeadingWhitespace.Select: result.Append(currentChar); continue;
                    }
                }
                else checkWhitespace = false;

                var isMatch = predicate(currentChar);
                if (isMatch)
                {
                    result.Append(currentChar);
                }
                else
                {
                    break;
                }
            }
            return result.ToString();
        }

        internal static string ReadWhile(this string value, Func<char, bool> predicate, ref int index, LeadingWhitespace leadingWhitespace = LeadingWhitespace.Ignore)
        {
            return value.Read(predicate, ref index, leadingWhitespace);
        }

        internal static string ReadUntil(this string value, Func<char, bool> predicate, ref int index, LeadingWhitespace leadingWhitespace = LeadingWhitespace.Select)
        {
            return value.Read(c => !predicate(c), ref index, leadingWhitespace);
        }

        internal static string ReadExact(this string value, Func<char, bool> predicate, ref int index, LeadingWhitespace leadingWhitespace = LeadingWhitespace.Ignore)
        {
            var result = new StringBuilder();
            for (; index < value.Length; index++)
            {
                var currentChar = value[index];

                var checkWhitespace = result.Length == 0;
                if (checkWhitespace)
                {
                    var ignoreLeadingWhitespace = leadingWhitespace == LeadingWhitespace.Ignore && currentChar.IsWhiteSpace();
                    if (ignoreLeadingWhitespace)
                    {
                        continue;
                    }
                }

                if (result.Length == 1)
                {
                    return result.ToString();
                }

                var isMatch = predicate(currentChar);
                if (isMatch)
                {
                    result.Append(currentChar);
                }
                else
                {
                    break;
                }
            }
            return null;
        }

    }
}
