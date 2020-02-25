using System;
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
    class Program
    {
       
        
        static void Main(string[] args)
        {
            var array = new[] { 16, 4, 10, 14, 7, 9, 4, 2, 8, 1 };
            var arrayTwoDigit = new[] { 16, 42, 10, 14, 71, 92, 34, 42, 58 };

            var g = new[] 
            {
                "1 3 1",
                "2 3 4",
                "3 4 2",
                "4 1 3"
            };
            
            var nodes = UtilityMethods.GetTailHeadWeightSpaceGraph(g);

            

            //var xs ="23883.33\n26133.33\n26550\n26433.33\n27096.11\n27153.61\n27026.11\n26283.33\n27233.33\n27166.67\n26150\n25149.17\n24166.67";
            //var xs ="24166.67\n26133.33\n26550\n26433.33\n27096.11\n27153.61\n27026.11\n26283.33\n27233.33\n27166.67\n26150\n25149.17\n23883.33";
            var xs ="25149.17\n24166.67\n27166.67\n26150";

            //var ys = "14533.33\n14500\n13850\n13433.33\n13415.83\n13203.33\n13051.94\n12766.67\n10450\n9833.333\n10550\n12365.83\n13250";
            //var ys = "13250\n14500\n13850\n13433.33\n13415.83\n13203.33\n13051.94\n12766.67\n10450\n9833.333\n10550\n12365.83\n14533.33";
            var ys = "12365.83\n13250\n9833.333\n10550";

            var xarr = xs.Split('\n').Select(float.Parse).ToArray();
            var yarr = ys.Split('\n').Select(float.Parse).ToArray();


            //11710.54

            //14409.19

            var s = new TravellingSalesman();
            
            var pointsCount = xarr.Length;
            Vector2[] points = new Vector2[pointsCount];

            for (int i = 0; i < pointsCount; i++)
            {
                points[i] = new Vector2(xarr[i],yarr[i]);
            }

            //Console.WriteLine(new TravellingSalesman().GetOptimalTour(points));
            Console.WriteLine(string.Join(", ",s.GetOptimalTour(points)));
            
            
            
            // var arrayTwo = new HashSet<int> { 16, 10, 14, 71, 92, 34, 42, 58 };
            // var arrayThree = new HashSet<int> { 10, 16, 14, 71, 92, 34, 42, 58 };
            //
            // var tup1 = new Tuple<HashSet<int>, int>(arrayTwo,5);
            // var tup2 = new Tuple<HashSet<int>, int>(arrayThree,5);
            // new TravellingSalesmanSolution().PrintCoords();
            //Console.WriteLine(new TravellingSalesmanSolution().SolveTravelingSalesman());

            
        }
        
        
    }
}