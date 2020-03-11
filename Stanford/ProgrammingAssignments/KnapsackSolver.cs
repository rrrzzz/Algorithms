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
            "https://d3c33hcgiwev3.cloudfront.net/_6dfda29c18c77fd14511ba8964c2e265_knapsack_big.txt?Expires=1584057600&Signature=HXsLYJNrP5dTt8F44PfHbKG0pYBWv-KD5sjEKuVyvor9ftLwWZqfse9wg4yNLMM1ytA1Or5f7X2zM~9-vbtKIQ4hEn3ivMrG0ZTb7LjwUgkh5qnbAbMeaOokFLuXWpTX9Q3pClTZZ9H9YdrLyB~ZTQCzwlbBXTm7tX1POt1mPkA_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
 
        const string SmallLink =
            "https://d3c33hcgiwev3.cloudfront.net/_6dfda29c18c77fd14511ba8964c2e265_knapsack1.txt?Expires=1584057600&Signature=LLNvWUyfCsv7VZjCk-8Xb92gvsRr9It-QCDjOCiPz6FW9d91pxHcsAFr7TgZI8jrO7IzM88A8Xx7xbmsgIGrO0U4aaJ2XY8Dfb4R-x0nGsmSqfpGTpWVG4H1aOJ77s9KXpI0dK55ZjthEpRYjHvQ3fCI9ZgHRKeOLGcuMo924T8_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
 
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