using System;
using System.Collections.Generic;
using System.Linq;

namespace MathematicalModel
{
    /// <summary>
    /// Tree node
    /// </summary>
    public class TreeNode
    {
        private const double Tolerance = 0.00001;

        /// <summary>
        /// Monomial data
        /// </summary>
        public Monomial Data { get; set; }

        /// <summary>
        /// Parent node
        /// </summary>
        public TreeNode Parent { get; set; }

        /// <summary>
        /// Child nodes
        /// </summary>
        public IList<TreeNode> Childs { get; set; }

        public TreeNode()
        {
            Childs = new List<TreeNode>();
        }

        /// <summary>
        /// Transform tree node to canonical form
        /// </summary>
        /// <returns>List of monomials</returns>
        public IList<Monomial> ToCanonicalForm()
        {
            var monomialList = new List<Monomial>();

            if (!Childs.Any())
            {
                AddMonomial(monomialList, Data);
                return monomialList;
            }

            foreach (var child in Childs)
            {
                var childMonimials = child.ToCanonicalForm();
                for (var i = 0; i < childMonimials.Count; i++)
                {
                    childMonimials[i] *= Data;
                }
                AddMonomials(monomialList, childMonimials);
            }

            return monomialList;
        }

        private static void AddMonomials(List<Monomial> monomialList, IEnumerable<Monomial> addedMonomialList)
        {
            foreach (var childMonimial in addedMonomialList)
            {
                AddMonomial(monomialList, childMonimial);
            }
        }

        private static void AddMonomial(List<Monomial> monomialList, Monomial monomial)
        {
            var findedSimilarMonomial = monomialList.Find(m => m.IsSimilar(monomial));
            if (findedSimilarMonomial != null)
            {
                findedSimilarMonomial.Сoefficient += monomial.Сoefficient;
                if (Math.Abs(findedSimilarMonomial.Сoefficient) < Tolerance)
                {
                    monomialList.Remove(findedSimilarMonomial);
                }
            }
            else
            {
                monomialList.Add(monomial);
            }
        }
    }
}
