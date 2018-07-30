using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Stanford.GraphStructures;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] {16, 4, 10, 14, 7, 9, 3, 2, 8, 1 };

            var graphList = new []
            {
                new [] {1, 2, 5},
                new [] {2, 3, 5, 6},
                new [] {3, 4},
                new [] {4, 7},
                new [] {5, 6},
                new [] {6, 7},
                new [] {7, 3, 8},
                new [] {8, 3}
            };

            var graphList2 = new[]
            {
                new [] {1, 2, 3},
                new [] {2, 3},
                new [] {3, 4},
                new [] {4}
            };

            //var solver = new MinCutSolver("https://lagunita.stanford.edu/assets/courseware/v1/d81819849ab16ea07f97e4814a7f76d0/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/kargerMinCut.txt");

            //var solver = new MinCutSolver(graphList);

            var graph = GraphGenerator.CreateAdjacencyList(graphList2);
            

            
            var searcher = new GraphSearch(graph);

            var order = searcher.GetTopologicalOrdering();



            Console.WriteLine(string.Join(", ", order.Select(x => x.ToString())));
            Console.ReadLine();
        }
    }
}