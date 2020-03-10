using System;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class LongestCommonSubstring
    {
        public float GetBestSubstring(char[] seq1, char[] seq2)
        {
            if (seq1.Length != seq2.Length)
            {
                throw new ArgumentException("Sequences have different lengths");
            }

            var seqLen = seq1.Length + 1;
            
            var cache = new int[seqLen, seqLen];
           
            for (int i = 1; i < seqLen; i++)
            {
                for (int z = 1; z < seqLen; z++)
                {
                    if (seq1[i - 1] != seq2[z - 1]) cache[i, z] = 0;
                    else
                    {
                        var ag = cache[i - 1, z] + 1;
                        var bg = cache[i, z - 1] + 1;
                        var ab = cache[i - 1, z - 1] + 1;
                        cache[i, z] = Math.Max(Math.Max(ag, bg), ab);
                    }
                }
            }
            var max = 0f;

            foreach (var item in cache)
            {
                max = Math.Max(max, item);
            }

            return max;
        }
    }
}