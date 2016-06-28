using System.Collections.Generic;
using System.Linq;

namespace MathematicalModel
{
    /// <summary>
    /// Tree node
    /// </summary>
    public class TreeNode
    {
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

        public List<Monomial> ToCanonicalForm()
        {
            var monomialList = new List<Monomial>();

            if (!Childs.Any())
            {
                monomialList.Add(Data);
                return monomialList;
            }

            foreach (var child in Childs)
            {
                var childMonimials = child.ToCanonicalForm();
                for (var i = 0; i < childMonimials.Count; i++)
                {
                    childMonimials[i] *= Data;
                }
                monomialList.AddRange(childMonimials);
            }

            return monomialList;
        }
    }
}
