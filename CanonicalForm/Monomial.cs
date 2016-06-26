using System;
using System.Collections.Generic;
using System.Linq;

namespace CanonicalForm
{
    internal class Monomial
    {
        /// <summary>
        /// Coefficient
        /// </summary>
        public double Сoefficient { get; set; }

        /// <summary>
        /// Varibles
        /// </summary>
        public IList<Varible> Varibles { get; set; }

        public Monomial()
        {
            Varibles = new List<Varible>();
        }

        public static Monomial operator *(Monomial a, Monomial b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));

            return new Monomial
            {
                Сoefficient = a.Сoefficient * b.Сoefficient,
                Varibles = MultiplieVaribles(a.Varibles, b.Varibles)
            };
        }

        private static IList<Varible> MultiplieVaribles(IList<Varible> aVaribles, IList<Varible> bVaribles)
        {
            if (aVaribles == null) throw new ArgumentNullException(nameof(aVaribles));
            if (bVaribles == null) throw new ArgumentNullException(nameof(bVaribles));

            IList<Varible> result;
            IList<Varible> iterationVaribles = null;

            if (aVaribles.Any())
            {
                result = aVaribles;
                iterationVaribles = bVaribles;
            }
            else
            {
                result = bVaribles;
            }
            if (iterationVaribles == null || !iterationVaribles.Any())
            {
                return result;
            }
            foreach (var iterationVarible in iterationVaribles)
            {
                var varible = result.FirstOrDefault(v => v.Name == iterationVarible.Name);
                if (varible == null)
                {
                    result.Add(iterationVarible);
                }
                else
                {
                    varible.Degree += iterationVarible.Degree;
                    if (varible.Degree == 0)
                    {
                        result.Remove(varible);
                    }
                }
            }

            return result;
        }
    }
}
