using System;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class SequenceAlignment
    {
        public float GetLeastPenaltySequence(char[] seq1, char[] seq2, float pGap, float pMismatch)
        {
            if (seq1.Length != seq2.Length)
            {
                throw new ArgumentException("Sequences have different lengths");
            }

            var seqLen = seq1.Length+1;
            
            var cache = new float[seqLen, seqLen];
            for (int i = 1; i < seqLen; i++)
            {
                var p = pGap * i; 
                cache[i, 0] = p;
                cache[0, i] = p;
            }
            
            for (int i = 1; i < seqLen; i++)
            {
                for (int z = 1; z < seqLen; z++)
                {
                    var ag = cache[i - 1, z] + pGap;
                    var bg = cache[i, z - 1] + pGap;
                    var ab = cache[i - 1, z - 1] + (seq1[i - 1] == seq2[z - 1] ? 0 : pMismatch);

                    cache[i, z] = Math.Min(Math.Min(ag, bg), ab);
                }
            }
            return cache[seqLen-1, seqLen-1];
        }
    }
}