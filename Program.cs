using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            var array = new[] { 16, 4, 10, 14, 7, 9, 4, 2, 8, 1 };
            var arrayTwoDigit = new[] { 16, 42, 10, 14, 71, 92, 34, 42, 58, 16 };

            var g = new[] 
            {
                "1 3 1",
                "2 3 4",
                "3 4 2",
                "4 1 3"
            };
            
            var nodes = UtilityMethods.GetTailHeadWeightSpaceGraph(g);







            var stop = new Stopwatch();
            stop.Start();

            var val = new ApspSolver().SolveFourthLink();
            Console.WriteLine(stop.ElapsedMilliseconds);
            Console.WriteLine(val);
            // .max 2147483647

        }
    }
}