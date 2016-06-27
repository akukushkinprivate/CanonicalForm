using System.Collections.Generic;

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
    }
}
