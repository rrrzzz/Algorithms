using System;
using System.Linq;
using System.Text;

namespace Algorithms.Stanford
{
    public static class Extensions
    {
        public static string Diff(this string str1, string str2) 
        { 
            // Before proceeding further, 
            // make sure str1 is not smaller 
            // Calculate lengths of both string 
            bool IsSmaller()
            {
                int len1 = str1.Length, len2 = str2.Length; 
                if (len1 < len2) 
                    return true; 
                if (len2 < len1) 
                    return false; 
      
                for (int i = 0; i < len1; i++) 
                    if (str1[i] < str2[i]) 
                        return true; 
                    else if (str1[i] > str2[i]) 
                        return false; 
  
                return false; 
            }
            
            if (IsSmaller()) 
            { 
                string t = str1;  
                str1 = str2; 
                str2 = t; 
            } 
  
            // Take an empty string for  
            // storing result 
            string str = ""; 
  
            // Calculate lengths of both string 
            int n1 = str1.Length, n2 = str2.Length; 
            int diff = n1 - n2; 
  
            // Initially take carry zero 
            int carry = 0; 
  
            // Traverse from end of both strings 
            for (int i = n2 - 1; i >= 0; i--) 
            { 
                // Do school mathematics, compute  
                // difference of current digits and carry 
                int sub = str1[i + diff] - '0' - 
                          (str2[i] - '0') - carry; 
                if (sub < 0) 
                { 
                    sub = sub + 10; 
                    carry = 1; 
                } 
                else
                    carry = 0; 
  
                str += sub.ToString(); 
            } 
  
            // subtract remaining digits of str1[] 
            for (int i = n1 - n2 - 1; i >= 0; i--) 
            { 
                if (str1[i] == '0' && carry > 0) 
                { 
                    str += "9"; 
                    continue; 
                } 
                int sub = str1[i] - '0' - carry; 
                if (i > 0 || sub > 0) // remove preceding 0's 
                    str += sub.ToString(); 
                carry = 0; 
  
            }

            
            var res = new string(str.Reverse().ToArray()).TrimStart('0');
            return res; 
        } 
       
        public static string Mul(this string num1, string num2)  
        {  
            var len1 = num1.Length;  
            var len2 = num2.Length;  
            if (len1 == 0 || len2 == 0)  
                return "0";  
  
            // will keep the result number in vector  
            // in reverse order  
            var result = new int[len1 + len2];  
  
            // Below two indexes are used to  
            // find positions in result.  
            var iN1 = 0;
            
            int i; 
      

            for (i = len1 - 1; i >= 0; i--)  
            {  
                var iN2 = 0;
                
                int carry = 0;  
                int n1 = num1[i] - '0';

                for (int j = len2 - 1; j >= 0; j--)  
                {  
            
                    int n2 = num2[j] - '0';  
  
                    // Multiply with current digit of first number  
                    // and add result to previously stored result  
                    // charAt current position.  
                    int sum = n1 * n2 + result[iN1 + iN2] + carry;  
  

                    carry = sum / 10;  
  
                    // Store result  
                    result[iN1 + iN2] = sum % 10;  
  
                    iN2++;  
                }
                
                result[iN1 + iN2] += carry;
                iN1++;  
            }  
  
            var bldr = new StringBuilder();
            foreach (var num in result)
            {
                bldr.Insert(0, num);
            }

            var res = bldr.ToString().TrimStart('0');

            return res.Length == 0 ? "0" : res;
        }

        public static string Sum(this string x, string y)
        {
            var carry = 0;
            var res = new StringBuilder();

            var minL = Math.Min(x.Length, y.Length);
            var maxL = Math.Max(x.Length, y.Length);

            string smallerString, largerString;
            if (minL == x.Length)
            {
                smallerString = x;
                largerString = y;
            }
            else
            {
                smallerString = y;
                largerString = x;
            }

            for (int min = minL - 1, max = maxL - 1; min >= 0; min--, max--)
            {
                var temp = int.Parse(smallerString[min].ToString());
                var temp2 = int.Parse(largerString[max].ToString());

                var sum = temp + temp2 + carry;
                carry = sum > 9 ? 1 : 0;
                res.Insert(0,sum % 10);
            }

            var dif = maxL - minL;

            for (int i = dif; i > 0; i--)
            {
                var temp = int.Parse(largerString[i - 1].ToString());
                var sum = temp + carry;
                carry = sum > 9 ? 1 : 0;
                res.Insert(0,sum % 10);
            }
            res.Insert(0,carry);

            return res.ToString().TrimStart('0');
        }
        
        public static string Pow(this string num, int pow)
        {
            var res = new string(num.ToCharArray());
            for (int i = 1; i < pow; i++)
            {
                res = res.Mul(num);
            }

            return res;
        }
    }
}