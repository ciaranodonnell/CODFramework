using System;
using System.Collections.Generic;
using System.Text;

namespace COD.Platform.Framework
{
    public static class StringBuilderExtensions
    {


        public static int IndexOf(this StringBuilder sb, string searchString, int startIndex = 0)
        {
            int x = startIndex;
            do
            {
                int s = 0;
                while (sb[x + s] == searchString[s++])
                {
                    if (s == searchString.Length)
                        return x;

                }
                x++;
            } while (x <= (sb.Length - searchString.Length));

            return -1;
        }


        public static int LastIndexOf(this StringBuilder sb, string searchString, int startIndex = -1)
        {
            int x = startIndex == -1 ? sb.Length - 1 : startIndex;
            var ssLastCharPos = searchString.Length-1;
            do
            {
                int s = 0;
                while (sb[x - s] == searchString[ssLastCharPos - s++])
                {
                    if (s == searchString.Length)
                        return x- ssLastCharPos;

                }
                x--;
            } while (x > -1);

            return -1;
        }



        
    }
}
