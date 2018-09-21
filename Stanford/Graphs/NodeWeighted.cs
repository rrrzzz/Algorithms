﻿using System;
using System.Collections.Generic;

namespace Algorithms.Stanford.Graphs
{
    public class NodeWeighted : NodeHeapIndexable
    {
        public NodeWeighted Parent { get; set; }
        public List<Tuple<NodeWeighted,int>> Neighbours { get; set; } = new List<Tuple<NodeWeighted, int>>();

        public NodeWeighted(int id) : base(id){}
    }
}