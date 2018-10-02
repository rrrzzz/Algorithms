using System;
using System.Diagnostics;
using System.Linq;
using Algorithms.Stanford.Misc;

namespace Algorithms.Stanford.ProgrammingAssignments
{
    //In this programming problem and the next you'll code up the greedy algorithm from the lectures on Huffman coding.
    //For example, the third line of the file is "6852892," indicating that the weight of the second symbol of the alphabet is 6852892. (We're using weights instead of frequencies, like in the "A More Complex Example" video.)

    //Your task in this problem is to run the Huffman coding algorithm from lecture on this data set. What is the maximum length of a codeword in the resulting Huffman code?
    //What is the minimum length?
    //Maximum length of encoding is 19
    //Minimum length of encoding is 9
    
    public static class HuffmanCodesSolver
    {
        public static void GetMinMaxLengthHuffmanCode()
        {
            var huffman = new HuffmanCodes();
            var encoding = huffman.GetCharacterEncoding(ParseCharWeightsFromWeb());

            var max = 0;
            var min = int.MaxValue;

            foreach (var code in encoding.Values)
            {
                if (code.Length > max) max = code.Length;
                if (code.Length < min) min = code.Length;
            }

            Console.WriteLine($"Maximum length of encoding is {max}\nMinimum length of encoding is {min}");
        }

        private static int[] ParseCharWeightsFromWeb()
        {
            const string link =
                "https://d3c33hcgiwev3.cloudfront.net/_eed1bd08e2fa58bbe94b24c06a20dcdb_huffman.txt?Expires=1537574400&Signature=JEcvRQJ2aMIkxtr~FQG9r1Jm-CNx5ACHRZ1AX794ocfIPEcA4TBnJkSXAmeFktwrGrgDICatqFzU67XdV3BLfCaq5gwDJ9S2M610Bgl6xXgT24p3OWyRLBG5b5Lx66wY1rAgCr~uGk9j5BX6Y2zim~b4lM3qRVPKY3c77ra1yqk_&Key-Pair-Id=APKAJLTNE6QMUY6HBC5A";

            return UtilityMethods.GetParsedStringArrayFromWeb(link, '\n').Skip(1).Select(int.Parse).ToArray();
        }
    }
}