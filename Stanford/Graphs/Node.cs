﻿namespace Algorithms.Stanford.Graphs
{
    public class Node
    {
        public int Id { get; set; }
        public bool IsVisited { get; set; }

        public Node(){}

        public Node(int id)
        {
            Id = id;
        }

        public void Visit()
        {
            IsVisited = true;
        }
    }
}