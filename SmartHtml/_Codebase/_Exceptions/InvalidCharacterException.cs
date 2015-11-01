using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public class InvalidCharacterException : HtmlException
    {

        public InvalidCharacterException(string html, int index) : base(html, index)
        {
            InvalidCharacter = html[index];
        }

        public override string Message
        {
            get
            {
                return "Invalid character [" + InvalidCharacter + "] at line " + Line + ", column " + Column + ".";
            }
        }

        public char InvalidCharacter { get; private set; }
    }
}
