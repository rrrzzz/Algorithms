﻿using System;
using System.Diagnostics;
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
                new [] {4},
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

            var answer = new KasarajuSccSolver().Solve();

            Console.WriteLine("The answer is\n" + answer);
            Console.ReadLine();
        }
    }
}