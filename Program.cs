using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Algorithms.Corman;
using Algorithms.Stanford.Dynamic_Programming;
using Algorithms.Stanford.Graphs;
using Algorithms.Stanford.Misc;
using Algorithms.Stanford.ProgrammingAssignments;

namespace Algorithms
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            //x 1 y 2 z 3 w 4 u 5 
            var ar = new[]
            {
                new[] {-1, 2}, new[] {-2, 3}, new[] {-3, 4}, new[] {-4, 5}, new[] {-5, -1}, new[] {1, 4}, new[] {-4, 1}
            };

            Console.WriteLine(new PapadimitrousTwoSat().GetConditionsSatisfiability(ar));
            ;
        }
    }
}