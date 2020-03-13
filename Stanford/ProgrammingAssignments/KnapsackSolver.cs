using System.Linq;
using Algorithms.Stanford.Dynamic_Programming;
 
namespace Algorithms.Stanford.ProgrammingAssignments
{
    /*In this programming problem and the next you'll code up the knapsack algorithm from lecture.
     
      Small set answer: 2493893    
     _____________________________________________________________________________________________
    
      This instance is so big that the straightforward iterative implemetation uses 
      an infeasible amount of time and space. So you will have to be creative 
      to compute an optimal solution. One idea is to go back to a recursive implementation, 
      solving subproblems --- and, of course, caching the results to avoid redundant work --- 
      only on an "as needed" basis. Also, be sure to think about appropriate data structures 
      for storing and looking up solutions to subproblems.    
            
      Large set answer: 4243395
    */

    public class KnapsackSolver
    {
        const string LargeLink =
            "https://lagunita.stanford.edu/assets/courseware/v1/64df53c958263a22ba04e37ce9204a74/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/knapsack_big.txt";
 
        const string SmallLink =
            "https://lagunita.stanford.edu/assets/courseware/v1/d55a5fe1d0942cf624532f2a2fc133f9/asset-v1:Engineering+Algorithms2+SelfPaced+type@asset+block/knapsack1.txt";
 
        public float SolveSmallKnapsack()
        {
            return SolveKnapsack(SmallLink);
        }
 
        public float SolveSmallKnapsackFast()
        {
            return SolveKnapsackFast(SmallLink);
        }
       
        public float SolveLargeKnapsack()
        {
            return SolveKnapsack(LargeLink);
        }
        
        public float SolveLargeKnapsackFast()
        {
            return SolveKnapsackFast(LargeLink);
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
       
        private float SolveKnapsackFast(string link)
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
           
            return new Knapsack(weight, items).GetOptimalSackValueBottomUp();
        }
    }
}