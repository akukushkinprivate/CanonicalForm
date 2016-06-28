namespace ParserEquation
{
    /// <summary>
    /// Class detection error while parsing
    /// </summary>
    public class Error
    {
        private bool _isError;
        private string _errorMessage;

        /// <summary>
        /// Get error state
        /// </summary>
        public bool IsError => _isError;

        /// <summary>
        /// Get error message
        /// </summary>
        public string ErrorMessage => _errorMessage;

        protected void SetError(string message)
        {
            _isError = true;
            _errorMessage = message;
        }

        protected void ResetError()
        {
            _isError = false;
            _errorMessage = null;
        }
    }
}
