using System;
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
                new [] {1, 2, 5, 6},
                new [] {2, 1, 3, 5, 6},
                new [] {3, 2, 4, 7, 8},
                new [] {4, 3, 7, 8},
                new [] {5, 1, 2, 6},
                new [] {6, 1, 2, 5, 7},
                new [] {7, 3, 4, 6, 8},
                new [] {8, 3, 4, 7}
            };

            //var solver = new MinCutSolver("https://lagunita.stanford.edu/assets/courseware/v1/d81819849ab16ea07f97e4814a7f76d0/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/kargerMinCut.txt");

            var solver = new MinCutSolver(graphList);

            Console.WriteLine(solver.FindMinCutByTrialsUnion());
            Console.ReadLine();

            //graph.ParseGraphListFromWebSource("https://lagunita.stanford.edu/assets/courseware/v1/d81819849ab16ea07f97e4814a7f76d0/asset-v1:Engineering+Algorithms1+SelfPaced+type@asset+block/kargerMinCut.txt");
        }
    }
}