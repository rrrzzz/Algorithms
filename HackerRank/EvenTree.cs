using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.HackerRank
{
    public static class EvenTree
    {
        //You are given a tree(a simple connected graph with no cycles). The tree has nodes numbered from  to and is rooted at node.

        //Find the maximum number of edges you can remove from the tree to get a forest such that each connected component of the forest contains an even number of vertices.

        //Input Format

        //The first line of input contains two integers N and M. N is the number of vertices, and M is the number of edges. 
        //The next M  lines contain two integers  and which specifies an edge of the tree.


        //Constraints  2 <= n <= 100

        //Note: The tree in the input will be such that it can always be decomposed into components containing an even number of nodes.

        //Output Format


        //Print the number of removed edges.

        public static int Cuts;

        public static void CountCuts()
        {
            var temp1 = Console.ReadLine().Split(' ');
            var n = int.Parse(temp1[0]);
            var m = int.Parse(temp1[1]);
            var nodeArray = new Node[n];
            nodeArray[0] = new Node();

            for (var x = 0; x < m; x++)
            {
                var edge = Console.ReadLine();
                var temp = edge.Split(' ');
                var child = int.Parse(temp[0]);
                var parent = int.Parse(temp[1]);
                var tempNode = new Node();
                nodeArray[child - 1] = tempNode;
                nodeArray[parent - 1].Children.Add(tempNode);
            }

            var root = nodeArray[0];

            foreach (var child in root.Children)
            {
                child.GetSubTreeLength();
            }

            Console.WriteLine(Cuts);
            Console.ReadLine();
        }
    }

    public class Node
    {
        public int Id { get; set; }

        public List<Node> Children { get; set; } = new List<Node>();

        public int SubTreeLength { get; set; }

        public int GetSubTreeLength()
        {
            SubTreeLength = 1;
            if (Children.Count != 0)
            {
                SubTreeLength += Children.Sum(child => child.GetSubTreeLength());
                if (SubTreeLength % 2 == 0)
                {
                    EvenTree.Cuts++;
                }
            }
            return SubTreeLength;
        }
    }
}
