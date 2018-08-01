using System;
using System.Collections.Generic;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class Node : IHeapIndexable
    {
        public int Id { get; set; }
        public int DijkstraScore { get; set; }
        public int HeapIndex { get; set; }
        public List<Tuple<Node,int>> Neighbours { get; set; } = new List<Tuple<Node, int>>();
        public bool IsVisited { get; set; }

        public Node(int id)
        {
            Id = id;
        }

        public int GetKey()
        {
            return DijkstraScore;
        }

        public void SetKey(int key)
        {
            DijkstraScore = key;
        }

        public void Visit()
        {
            IsVisited = true;
        }
    }
}
