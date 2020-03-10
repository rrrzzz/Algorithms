using System;
using System.Numerics;
using System.Text;

namespace Algorithms.Stanford.Misc
{
    public class KaratsubaMultiplication
    {
        // x = 10^n/2 * a + b; y = 10^n/2 * c + d
        //10^n * ac + 10^n/2*ad + 10^n/2*cb + bd
        // 1: ac, 2: bd, 3: (a + b) * (c + d) = ac + bc + ad + bd
        // (3 - 2 - 1) = bc + ad
        
        public BigInteger MultiplyBigInts(BigInteger num1, BigInteger num2)
        {
            int n = (int)Math.Ceiling(Math.Max(BigInteger.Log(num1, 2),
                BigInteger.Log(num2, 2)));

            // Base case for recursion
            // if Max of 2 numbers is less than or equal to 10 then just multiply them.
            if (n <= 3) return num1 * num2;

            n = (n + 1) / 2;
            var b = num1 >> n;          // second half of num1
            var a = num1 - (b << n);    // first half of num1
            var d = num2 >> n;          // second half of num2
            var c = num2 - (d << n);    // first half of num2

            // Recursive calls
            var ac = MultiplyBigInts(a, c);
            var bd = MultiplyBigInts(b, d);
            var abcd = MultiplyBigInts(a + b, c + d);

            return ac + ((abcd - ac - bd) << n) + (bd << (2 * n));
        }
        
        public string MultiplyStringRepresentations(string x, string y)
        {
            var lenX = x.Length;
            var lenY = y.Length;
            if (lenX == 1 || lenY == 1) return x.Mul(y);

            var len = Math.Max(x.Length, y.Length);
            len += len % 2;
            var halfLen = len / 2;

            var xRightStart = x.Length - halfLen;
            var b = "";
            if (xRightStart < 0)
            {
                b = new StringBuilder().Append('0', Math.Abs(xRightStart)).ToString();
                xRightStart = 0;
            }
            b += x.Substring(xRightStart);
            var a = xRightStart == 0 ? "0" : x.Substring(0, xRightStart);

            var yRightStart = y.Length - halfLen;
            string d = "";
            if (yRightStart < 0)
            {
                d = new StringBuilder().Append('0', Math.Abs(yRightStart)).ToString();
                yRightStart = 0;
            }
            d += y.Substring(yRightStart);
            var c = yRightStart == 0 ? "0" : y.Substring(0, yRightStart);

            var ac = MultiplyStringRepresentations(a, c);
            var bd = MultiplyStringRepresentations(b, d);
            var others = MultiplyStringRepresentations(a.Sum(b), c.Sum(d));
            var bcplusad = others.Diff(ac).Diff(bd);

            var firstPow = "10" + new string('0', halfLen * 2 - 1);
            var secondPow = "10" + new string('0', halfLen - 1);

            return firstPow.Mul(ac).Sum(bcplusad.Mul(secondPow)).Sum(bd);
        }
    }
}