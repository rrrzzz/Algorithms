using System;

namespace Algorithms.Stanford.Dynamic_Programming
{
    public class OptimalBst
    {
        public float GetOptimalExpectedValueBstSearch(float[] f)
        {
            var freqs = new float[f.Length + 1];
            for (int i = 1; i <= f.Length; i++)
            {
                freqs[i] = f[i - 1];
            }

            var n = freqs.Length;
            
            var A = new float[n,n];
            n--;

            for (var S = 0; S < n; S++)
            {
                for (var i = 1; i <= n - S; i++)
                {
                    var j = i + S;
                    
                    A[i, j] = float.MaxValue;
                    
                    if (i == j)
                    {
                        A[i, j] = freqs[i]; 
                        continue;
                    }

                    var sumFreqs = 0f;
                    for (int k = i; k <= j; k++)
                    {
                        sumFreqs += freqs[k];
                    }

                    for (int r = i; r <= j; r++)
                    {
                        var x = i > r - 1 ? 0 : A[i, r - 1];
                        var y = r + 1 > j ? 0 : A[r + 1, j];
                        A[i,j] = Math.Min(A[i,j], sumFreqs + x + y);
                    }
                }
            }

            return A[1, n];
        }
    }
}