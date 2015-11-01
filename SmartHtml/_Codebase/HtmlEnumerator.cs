using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHtml
{
    internal class HtmlEnumerator : IEnumerator<char>
    {
        private readonly CharEnumerator _charEnumerator;

        private char _lastChar;

        private bool _isEndOfHtml = true;

        public HtmlEnumerator(string html)
        {
            _charEnumerator = html.GetEnumerator();
        }

        public bool IsEndOfHtml => _isEndOfHtml;

        public char Current => _isEndOfHtml ? _lastChar : _charEnumerator.Current;

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            _isEndOfHtml = !_charEnumerator.MoveNext();
            if (!_isEndOfHtml)
            {
                _lastChar = Current;
            }
            return !_isEndOfHtml;
        }       

        public void Reset()
        {
            _charEnumerator.Reset();
        }

        public void Dispose()
        {
            _charEnumerator.Dispose();
        }
    }
}
