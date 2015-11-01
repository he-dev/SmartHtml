using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    static class EnumExtensions
    {
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var attribute =
                value.GetType()
                    .GetMember(value.ToString())
                    .First()
                    .GetCustomAttribute<T>();
            return attribute;
        }
    }
}
