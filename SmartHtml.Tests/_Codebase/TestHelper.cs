using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml.Tests
{
    class TestHelper
    {
        public static string ReadAllText(string fileName)
        {
            const string testHtmlDirectoryName = "_TestHtml";

            //var asm = Assembly.GetAssembly(typeof(HtmlParserTests));
            var fullPath = Path.Combine(testHtmlDirectoryName, fileName + ".html");
            return File.ReadAllText(fullPath);
        }
    }
}
