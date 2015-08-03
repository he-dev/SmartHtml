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

        public static string AdvanceWhile(this string value, Func<char, bool> predicate, ref int i)
        {
            return value.Advance(predicate, ref i);
        }

        public static string AdvanceUntil(this string value, Func<char, bool> predicate, ref int i)
        {
            return value.Advance(c => !predicate(c), ref i);
        }

        public static string AdvanceOne(this string value, Func<char, bool> predicate, ref int i)
        {           
            var one = 0;
            return value.Advance(c => one++ < 1, ref i);
        }

        internal static string Advance(this string value, Func<char, bool> predicate, ref int i)
        {
            var startIndex = i;
            var endIndex = i;
            for (; i < value.Length && predicate(value[i]); i++, endIndex++) ;

            return value.Extract(startIndex, endIndex);
        }

    }
}
