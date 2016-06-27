using System;
using System.Collections.Generic;
using System.Text;
using MathematicalModel;

namespace ParserEquation
{
    /// <summary>
    /// Parser of equation string
    /// </summary>
    public class ParserEquation
    {
        private readonly StringContainer _stringContainer;
        private int _currentSign;

        public ParserEquation(string equotionString)
        {
            if (equotionString == null)
            {
                throw new ArgumentNullException(nameof(equotionString));
            }
            _stringContainer = new StringContainer(equotionString);
            _currentSign = 1;
        }

        public TreeNode Parse()
        {
            var root = new TreeNode { Data = new Monomial { Сoefficient = 1.0 } };
            var currentNode = root;

            while (_stringContainer.CurrentSymbol != char.MinValue)
            {
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
                        if (currentNode.Parent == null)
                        {
                            return null;
                        }
                        currentNode = currentNode.Parent;
                    }
                }
                if (!IsNeedChangeSign())
                {
                    continue;
                }
                _stringContainer.PopCurrentSymbol();
                _currentSign = -_currentSign;
            }

            return root.Equals(currentNode) ? root : null;
        }

        private bool IsNeedChangeSign()
        {
            return _stringContainer.CurrentSymbol == '=';
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
                    throw new Exception();
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

            return _currentSign * sign * number;
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
