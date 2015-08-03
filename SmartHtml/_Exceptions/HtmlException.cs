using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    public abstract class HtmlException : Exception
    {
        public HtmlException(string html, int index)
        {
            CalcLineAndColumn(html, index);
        }        

        public int Line { get; private set; }
        public int Column { get; private set; }

        private void CalcLineAndColumn(string html, int index)
        {
            Line = 1;
            Column = 0;
            for (int i = 0; i < index; i++)
            {
                Column++;
                var isCarriageReturn = html[i] == '\r';
                var isNewLine = html[i + 1] == '\n';
                if (isNewLine) i++;
                if (isCarriageReturn)
                {
                    Line++;
                    Column = 0;
                }
            }
        }
    }
}
