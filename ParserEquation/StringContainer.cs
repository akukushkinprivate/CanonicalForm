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
        /// Get or set current position
        /// </summary>
        public int CurrentPosition
        {
            get { return _currentPosition; }
            set { _currentPosition = value >= 0 || value < _stringLength ? value : 0; }
        }

        /// <summary>
        /// Get contained string
        /// </summary>
        public string String => _string;

        /// <summary>
        /// Get value symbol on current position, if current position is out of range index then returns char.MinValue
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
