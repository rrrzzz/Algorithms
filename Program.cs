using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Permissions;
using System.Text;
using Algorithms.Corman;
using Algorithms.Stanford;
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
            Console.WriteLine(ClusterizationSolver.GetMaxClusterizationSpacing(4));
            ;
        }
    }
}