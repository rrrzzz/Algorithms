using System.Linq;
using System.Numerics;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    
    // You should implement the nearest neighbor heuristic:
    //
    // Start the tour at the first city.
    // Repeatedly visit the closest city that the tour hasn't visited yet. In case of a tie, go to the closest city with the lowest index.
    // For example, if both the third and fifth cities have the same distance from the first city (and are closer than any other city),
    // then the tour should begin by going from the first city to the third city.
    // Once every city has been visited exactly once, return to the first city to complete the tour.
    // Answer: 
    
    public class TspGreedySolver
    {
        private const string Link =
            "https://d3c33hcgiwev3.cloudfront.net/_ae5a820392a02042f87e3b437876cf19_nn.txt?Expires=1584057600&Signature=ZPTalWbo737F856C891gaV1Un-8aR5NuqzGO7OYW7kRIWD722h7ZzIVBAepDbembcgNcxRZmyrGpLNDbl1omFuvu2czoRRytOXmyZY1ji7IbpTUmu3AVOQgrNAXshhwYTf~92~4Qt8veVB6hf7Gs6sRFPNoYaIh4XB9NzDDQiV8_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        
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