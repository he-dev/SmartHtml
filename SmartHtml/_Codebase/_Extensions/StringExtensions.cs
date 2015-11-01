using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static bool IsVoid(this string tagName)
        {
            var attribute =
                (typeof(HtmlTagNames))
                    .GetMember(tagName)
                    .First()
                    .GetCustomAttribute<VoidAttribute>();
            return attribute != null;
        }
    }
}
