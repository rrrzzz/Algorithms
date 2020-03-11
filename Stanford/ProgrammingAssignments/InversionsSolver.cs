using System;
using System.Linq;
using System.Net;
using Algorithms.Stanford.Sorting;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    
    // This file contains all of the 100,000 integers between 1 and 100,000 (inclusive) in some order, with no integer repeated.
    // Your task is to compute the number of inversions in the file given
    // Answer: 2407905288
    
    public class InversionsSolver
    {
        private const string Link =
            "https://d3c33hcgiwev3.cloudfront.net/_bcb5c6658381416d19b01bfc1d3993b5_IntegerArray.txt?Expires=1584057600&Signature=D-syCWgArOwGKRTlDygG2rEsOoDo7F7H~C6ye2RkhMJbS1yTNA972~gIbEtcYAIOo-SlEBb1HpaAk4W2FTl0hwWN4UqCvWDbq~HIZUl4MTHRIc5NHYzIZnd0gsK85hfUiLuxuxS1LANUe4w-8wGBhRL2ozATLSZ1CMKMvMqr0dQ_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";
        
        public static void Solve()
        {
            var parsedArray = GetArrayFromWeb(Link);
            var counter = new InversionCounter();
            Console.WriteLine(counter.CountInversions(parsedArray));
        }

        private static int[] GetArrayFromWeb(string link)
        {
            string content;

            using (var client = new WebClient())
            {
                content = client.DownloadString(link).TrimEnd();
            }

            var stringArray = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return stringArray.Select(int.Parse).ToArray();
        }
    }
}