using System;

namespace ParserEquation
{
    /// <summary>
    /// Container for string
    /// </summary>
    internal class StringContainer
    {
        private readonly string _string;
        private readonly int _stringLength;
        private int _currentPosition;

        /// <summary>
        /// Returns value symbol on current position, if current position is out of range index then returns char.MinValue
        /// </summary>
        public char CurrentSymbol
        {
            get
            {
                if (_currentPosition >= _stringLength)
                {
                    return char.MinValue;
                }
                IgnoreWhiteSpace();
                return _string[_currentPosition];
            }
        }

        public StringContainer(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            _string = str;
            _stringLength = str.Length;
        }

        /// <summary>
        /// Pop current symbol with shift current position
        /// </summary>
        /// <returns>Symbol on current position</returns>
        public char PopCurrentSymbol()
        {
            var currentSymbol = CurrentSymbol;
            _currentPosition++;
            return currentSymbol;
        }

        private void IgnoreWhiteSpace()
        {
            while (char.IsWhiteSpace(_string[_currentPosition]))
            {
                _currentPosition++;
            }
        }
    }
}
