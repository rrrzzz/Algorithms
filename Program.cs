using System;
using System.Collections.Generic;
using System.Diagnostics;
using Algorithms.Corman;
using Algorithms.Stanford.Dynamic_Programming;
using Algorithms.Stanford.Graphs;
using Algorithms.Stanford.Misc;
using Algorithms.Stanford.ProgrammingAssignments;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
//            var array = new[] { 16, 4, 10, 14, 7, 9, 4, 2, 8, 1 };
//            var arrayTwoDigit = new[] { 16, 42, 10, 14, 71, 92, 34, 42, 58, 16 };
//
//            var graphList = new[]
//            {
//                new [] {1, 2, 5},
//                new [] {2, 3, 5, 6},
//                new [] {3, 4},
//                new [] {4},
//                new [] {5, 6},
//                new [] {6, 7},
//                new [] {7, 3, 8},
//                new [] {8, 3}
//            };
//
//            var graphList2 = new[]
//            {
//                new [] {1, 2, 3},
//                new [] {2, 3},
//                new [] {3, 4},
//                new [] {4}
//            };
//
//            var graph = new Dictionary<int, NodeWeighted>
//            {
//                {1, new NodeWeighted(1)},
//                {2, new NodeWeighted(2)},
//                {3, new NodeWeighted(3)},
//                {4, new NodeWeighted(4)}
//            };
//
//            graph[1].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[2], 1));
//            graph[1].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[3], 9));
//            graph[1].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[4], 3));
//            graph[2].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[1], 1));
//            graph[2].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[4], 2));
//            graph[3].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[1], 9));
//            graph[3].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[4], 6));
//            graph[4].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[2], 2));
//            graph[4].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[3], 6));
//            graph[4].Neighbours.Add(new Tuple<NodeWeighted, int>(graph[1], 3));

            var sack = new[,] {{3, 2}, {1, 1}, {4, 4}};

            var weight = 3; //max 6
 
            var solver = new KnapsackSolver(); 
            
            var timer = new Stopwatch();
            timer.Start();
            Console.WriteLine(solver.SolveLargeKnapsackFast());
            Console.WriteLine(timer.ElapsedMilliseconds);
        }
    }
}