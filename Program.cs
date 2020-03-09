using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

            
            var freqs = new[] {.2f, .05f, .17f, .1f, .2f, .03f, .25f};

            var f = new OptimalBst();
            var res = f.GetOptimalExpectedValueBstSearch(freqs);

            Console.WriteLine(res);
        }
    }
}