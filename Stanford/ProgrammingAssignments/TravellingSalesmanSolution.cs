using System;
using System.Linq;
using System.Numerics;
using System.Text;
using Algorithms.Stanford.Dynamic_Programming;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public class TravellingSalesmanSolution
    {
        //In this assignment you will implement one or more algorithms for the traveling salesman problem,
        //such as the dynamic programming algorithm covered in the video lectures. Here is a data file describing a TSP instance.
        //Calculate the minimum cost of a traveling salesman tour for this instance, rounded down to the nearest integer.
        
        private const string Link =
            "https://lagunita.stanford.edu/assets/courseware/v1/0557f47943b80364030343bfd38d0c72/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/tsp.txt";


        // public float SolveTravelingSalesman()
        // {
        //     var coords = ParseCityCoords();
        //     
        //     var tsp = new TravellingSalesman();
        //
        //     return tsp.GetOptimalTour(coords);
        // }
        //
        // public float SolveTravelingSalesmanTest()
        // {
        //     var tsp = new TravellingSalesman();
        //
        //     return tsp.GetOptimalTourTest(Enumerable.Range(0,4).ToArray());
        // }

        public void PrintCoords3()
        {
            var coords = ParseCityCoords();
            
            char c = 'A';
            foreach (var vec2 in coords)
            {
                Console.WriteLine($"{c} = ({vec2.X},{vec2.Y})");
                c++;
            }
        }
        
        public void PrintCoords()
        {
            var coords = ParseCityCoords();
            
            char c = 'A';
            foreach (var vec2 in coords)
            {
                Console.WriteLine($"{vec2.X}");
            }
            
            Console.WriteLine($"\n\n\n");
            foreach (var vec2 in coords)
            {
                Console.WriteLine($"{vec2.Y}");
            }
            
        }
        
        public void PrintCoords2()
        {
            var coords = ParseCityCoords();
            var stringB = new StringBuilder();
            char c = 'A';
            foreach (var vec2 in coords)
            {
                stringB.Append($"{c} = ({vec2.X},{vec2.Y}), ");
                c++;
            }

            Console.WriteLine(stringB.ToString());
            
        }

        private Vector2[] ParseCityCoords()
        {
            var lines = UtilityMethods.GetParsedStringArrayFromWeb(Link, "\n");
            var size = int.Parse(lines[0]);
            lines = lines.Skip(1).ToArray();
            var cityCoords = new Vector2[size];
            var counter = 0;
            
            foreach (var line in lines)
            {
                var floats = line.Split(' ').Select(float.Parse).ToArray();
                cityCoords[counter++] = new Vector2(floats[0], floats[1]);
            }

            return cityCoords;
        }
    }
}