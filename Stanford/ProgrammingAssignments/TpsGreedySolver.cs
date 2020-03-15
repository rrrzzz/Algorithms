using System;
using System.Linq;
using Algorithms.Stanford.DataStructures;
using Algorithms.Stanford.Dynamic_Programming;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    public static class TpsGreedySolver
    {
        private const string Link =
            "https://d3c33hcgiwev3.cloudfront.net/_ae5a820392a02042f87e3b437876cf19_nn.txt?Expires=1584403200&Signature=K7XXPS5Lll9XdHEYLPujtPs7U4VWA6AzNGc7F02l958jn3ZE~oQ02PSIxs5EK70pFY9CDXp~6pR9ljIZdvKTTCuYEftzLwd8CJjPFXECwWjBkhUwXT4nPij0YAmw2-KL5ydWgzJ59VEs051Viifjccs-FGMvBGAKnbrfhHHoftQ_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
      
        public static void SolveGreedyTps()
        {
            var coords = ParseCityCoords();
            Console.WriteLine( new TravellingSalesman().GetTourGreedy(coords));
        }
        
        private static VectorDouble[] ParseCityCoords()
        {
            var lines = UtilityMethods.GetParsedStringArrayFromWeb(Link, "\n");
            var size = int.Parse(lines[0]);

            lines = lines.Skip(1).ToArray();
            var cityCoords = new VectorDouble[size];
            var counter = 0;
            
            foreach (var line in lines)
            {
                if (counter >= size) break;
                var doubles = line.Split(' ').Select(double.Parse).ToArray();
                cityCoords[counter++] = new VectorDouble(doubles[1], doubles[2]);
            }

            return cityCoords;
        }
    }
}