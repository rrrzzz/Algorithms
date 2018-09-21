using Algorithms.Stanford.Graphs;

namespace Algorithms.Stanford.DataStructures
{
    public class BinaryTreeNode : NodeHeapIndexable
    {
        public BinaryTreeNode Parent { get; set; }
        public BinaryTreeNode LeftChild { get; set; }
        public BinaryTreeNode RightChild { get; set; }
        
        public BinaryTreeNode(){}
        public BinaryTreeNode(int id) : base(id){}

        public bool IsLeaf()
        {
            return LeftChild == null && RightChild == null;
        }
    }
}
