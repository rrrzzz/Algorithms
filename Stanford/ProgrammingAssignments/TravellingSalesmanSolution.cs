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
        //answer is 26442
 
        private const string Link =
            "https://d3c33hcgiwev3.cloudfront.net/_f702b2a7b43c0d64707f7ab1b4394754_tsp.txt?Expires=1584057600&Signature=Neba2-uXMeeo~nGtTMV9YITlGpfH9XBbb-KALljSirI04TmZHSuTrWmE2o8g6fXEvHvnk0kO6vl337cTzZUCMYRxhbKN2kB9emyer3iw66EhhVUdKyEJ-l~pu9f61-GViqPrmcluQUfExiFD86z0IWZYkcKlma2j2Zet81SpGpc_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

        private readonly double[]  _xs =
        {
            20833.33,
            20900,
            21600,
            23616.67,
            23700,
            23883.33,
            24166.67,
            26133.33,
            27990.69,
            21600,
            22583.33,
            21600,
            21300,
            22183.33,
            22683.33
        };

        private readonly double[] _ys =
        {
            17100,
            17066.67,
            16500,
            15866.67,
            15933.33,
            14533.33,
            13250,
            14500,
            9405.644,
            14966.67,
            14300,
            14150,
            13016.67,
            13133.33,
            12716.67
        };
        
        public float SolveTravelingSalesman()
        {
            var tsp = new TravellingSalesman();
            
            var pointsCount = _ys.Length;
            Vector2[] points = new Vector2[pointsCount];

            for (int i = 0; i < pointsCount; i++)
            {
                points[i] = new Vector2((float)_xs[i],(float)_ys[i]);
            }
            
            var (pathlength, optimalPath) = tsp.GetTourPathLengthToLastNode(points);
            return pathlength;
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