using System;
using System.Collections.Generic;
using Algorithms.Stanford.DataStructures;

namespace Algorithms.Stanford.Graphs
{
    public class NodeWeighted : Node, IHeapIndexable
    {
        public int Score { get; set; }
        public int HeapIndex { get; set; }
        public NodeWeighted Parent { get; set; }
        public List<Tuple<NodeWeighted,int>> Neighbours { get; set; } = new List<Tuple<NodeWeighted, int>>();

        public NodeWeighted(int id) : base(id){}

        public int GetKey()
        {
            return Score;
        }

        public void SetKey(int key)
        {
            Score = key;
        }        
    }
}