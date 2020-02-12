using System.Linq;
using Algorithms.Stanford.Dynamic_Programming;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    //In this programming problem and the next you'll code up the knapsack algorithm from lecture.
    public class KnapsackSolver
    {
        public float SolveSmallKnapsack()
        {
            var link =
                "https://lagunita.stanford.edu/assets/courseware/v1/d55a5fe1d0942cf624532f2a2fc133f9/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/knapsack1.txt";
            return SolveKnapsack(link);
        }

        public float SolveLargeKnapsack()
        {
            var link =
                "https://lagunita.stanford.edu/assets/courseware/v1/64df53c958263a22ba04e37ce9204a74/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/knapsack_big.txt";
            return SolveKnapsack(link);
        }

        private float SolveKnapsack(string link)
        {
            var parsedLink = UtilityMethods.GetParsedStringArrayFromWeb(link, "\n");
            var weight = parsedLink[0].Split(' ').Select(int.Parse).First();
            var itemsString = parsedLink.Skip(1);
            var index = 0;
            var items = new int[parsedLink.Length - 1, 2];
            foreach (var itemString in itemsString)
            {
                var currentString = itemString.Split(' ');
                items[index, 1] = int.Parse(currentString[0]);
                items[index++, 0] = int.Parse(currentString[1]);
            }
            
            return new Knapsack(weight, items).GetOptimalSackValue();
        }
    }
}