using System.Collections.Generic;
using System.Text;
using MathematicalModel;

namespace ParserEquation
{
    /// <summary>
    /// Parser of equation string
    /// </summary>
    public class ParserEquation
        : Error
    {
        private const string EquotionStringNullMessage = "Equotion string is null.";
        private const string IncrorrectFormatEquotionStringMessage = "Incorrect format equotion string.";
        private readonly StringContainer _stringContainer;

        public ParserEquation(string equotionString)
        {
            if (equotionString == null)
            {
                SetError(EquotionStringNullMessage);
                return;
            }
            _stringContainer = new StringContainer(equotionString);
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <returns>Root of euqotion tree</returns>
        public TreeNode Parse()
        {
            var signEqualityPosition = _stringContainer.String.IndexOf('=');
            if (signEqualityPosition < 0) 
            {
                SetError(IncrorrectFormatEquotionStringMessage);
                return null;
            }

            var beforeEqual = ParseFrom();
            var afterEqual = ParseFrom(signEqualityPosition + 1); // + 1 - next position after '='
            if (IsError)
            {
                return null;
            }
            afterEqual.Data.Сoefficient = -afterEqual.Data.Сoefficient;

            var root = new TreeNode { Data = new Monomial { Сoefficient = 1.0 } };
            root.Childs.Add(beforeEqual);
            root.Childs.Add(afterEqual);
            return root;
        }

        private TreeNode ParseFrom(int position = 0)
        {
            _stringContainer.CurrentPosition = position;
            var root = new TreeNode { Data = new Monomial { Сoefficient = 1.0 } };
            var currentNode = root;

            while (IsNeedContinueParse())
            {
                if (currentNode == null)
                {
                    SetError(IncrorrectFormatEquotionStringMessage);
                    return null;
                }
                var monomial = GetMonomial();
                var addedNode = new TreeNode { Data = monomial };
                currentNode.Childs.Add(addedNode);
                if (IsNeedDownLevel())
                {
                    _stringContainer.PopCurrentSymbol();
                    addedNode.Parent = currentNode;
                    currentNode = addedNode;
                }
                else
                {
                    while (IsNeedUpLevel())
                    {
                        _stringContainer.PopCurrentSymbol();
                        if (currentNode?.Parent == null)
                        {
                            SetError(IncrorrectFormatEquotionStringMessage);
                            return null;
                        }
                        currentNode = currentNode.Parent;
                    }
                }
            }
            if (!root.Equals(currentNode))
            {
                SetError(IncrorrectFormatEquotionStringMessage);
            }

            return !IsError ? root : null;
        }

        private bool IsNeedContinueParse()
        {
            if (_stringContainer.CurrentSymbol != '=')
            {
                return _stringContainer.CurrentSymbol != char.MinValue && !IsError;
            }
            _stringContainer.PopCurrentSymbol();
            return false;
        }

        private bool IsNeedUpLevel()
        {
            return _stringContainer.CurrentSymbol == ')';
        }

        private bool IsNeedDownLevel()
        {
            return _stringContainer.CurrentSymbol == '(';
        }

        private Monomial GetMonomial()
        {
            return new Monomial
            {
                Сoefficient = GetCoefficient(),
                Varibles = GetVaribles()
            };
        }

        private IList<Varible> GetVaribles()
        {
            var varibles = new List<Varible>();
            Varible varible;

            while ((varible = GetVarible()) != null)
            {
                varibles.Add(varible);
            }

            return varibles;
        }

        private Varible GetVarible()
        {
            if (!char.IsLetter(_stringContainer.CurrentSymbol))
            {
                return null;
            }

            var varible = new Varible { Name = _stringContainer.PopCurrentSymbol() };
            if (_stringContainer.CurrentSymbol == '^')
            {
                _stringContainer.PopCurrentSymbol();
                double degree;
                if (TryGetNumber(out degree))
                {
                    varible.Degree = (int) degree;
                }
                else
                {
                    SetError(IncrorrectFormatEquotionStringMessage);
                    return null;
                }
            }
            else
            {
                varible.Degree = 1;
            }

            return varible;
        }

        private double GetCoefficient()
        {
            var sign = 1;
            double number;

            switch (_stringContainer.CurrentSymbol)
            {
                case '+':
                    sign = 1;
                    _stringContainer.PopCurrentSymbol();
                    break;
                case '-':
                    sign = -1;
                    _stringContainer.PopCurrentSymbol();
                    break;
            }

            number = TryGetNumber(out number) ? number : 1.0;

            return sign * number;
        }

        private bool TryGetNumber(out double number)
        {
            var numberStringBuilder = new StringBuilder();
            int partNumber;

            if (TryGetNumber(out partNumber))
            {
                numberStringBuilder.Append(partNumber.ToString());
            }
            if (_stringContainer.CurrentSymbol == '.')
            {
                numberStringBuilder.Append(',');
                _stringContainer.PopCurrentSymbol();
                if (TryGetNumber(out partNumber))
                {
                    numberStringBuilder.Append(partNumber.ToString());
                }
            }

            return double.TryParse(numberStringBuilder.ToString(), out number);
        }

        private bool TryGetNumber(out int number)
        {
            var numberStringBuilder = new StringBuilder();

            while (char.IsDigit(_stringContainer.CurrentSymbol))
            {
                numberStringBuilder.Append(_stringContainer.PopCurrentSymbol());
            }

            return int.TryParse(numberStringBuilder.ToString(), out number);
        }
    }
}
