using System;
using System.Collections.Generic;
using Algorithms.Corman.DataStructures;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Misc
{
    public class HuffmanCodes
    {
        
        int[] freq = new[] {28, 27, 20, 15, 10};
        
        public Dictionary<int, string> GetCharacterEncoding(int[] charWeights)
        {
            var minHeap = InitializeMinHeap(charWeights);
            var superNode = MergeNodesByWeightIntoOne(minHeap);
            var charEncoding = UnmergeAndCreateEncoding(superNode);

            return charEncoding;
        }

        private MinHeapUniversal<BinaryTreeNode> InitializeMinHeap(int[] charWeights)
        {
            var minHeap = new MinHeapUniversal<BinaryTreeNode>();

            for (var i = 0; i < charWeights.Length; i++)
            {
                var newNode = new BinaryTreeNode(i);
                newNode.SetKey(charWeights[i]);

                minHeap.InsertElement(newNode);
            }

            return minHeap;
        }

        private BinaryTreeNode MergeNodesByWeightIntoOne(MinHeapUniversal<BinaryTreeNode> minHeap)
        {
            while (minHeap.GetHeapSize() != 1)
            {
                var firstSmallestNode = minHeap.ExtractMinElement();
                var secondSmallestNode = minHeap.ExtractMinElement();

                var superNode = new BinaryTreeNode();
                superNode.SetKey(firstSmallestNode.GetKey() + secondSmallestNode.GetKey());
                superNode.LeftChild = firstSmallestNode;
                superNode.RightChild = secondSmallestNode;
                minHeap.InsertElement(superNode);
            }

            return minHeap.ExtractMinElement();
        }

        private Dictionary<int, string> UnmergeAndCreateEncoding(BinaryTreeNode superNode)
        {
            var charEncoding = new Dictionary<int, string>();

            var leftCode = "0";
            var rightCode = "1";

            var nodeQueue = new Queue<Tuple<string, BinaryTreeNode>>();
            nodeQueue.Enqueue(new Tuple<string, BinaryTreeNode>("", superNode));

            while (nodeQueue.Count != 0)
            {
                var currentNodeTuple = nodeQueue.Dequeue();
                var (encoding, node) = currentNodeTuple;

                if (node.IsLeaf())
                {
                    charEncoding.Add(node.Id, encoding);
                    continue;
                }

                var leftNodeTuple = new Tuple<string, BinaryTreeNode>(encoding + leftCode, node.LeftChild);
                var rightNodeTuple = new Tuple<string, BinaryTreeNode>(encoding + rightCode, node.RightChild);

                nodeQueue.Enqueue(leftNodeTuple);
                nodeQueue.Enqueue(rightNodeTuple);
            }

            return charEncoding;
        }
    }
}