using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KanjiRecognizer.Source
{
    public class CustomMath
    {        
        public static int RoundUp(double i)
        {
            return i % 1 != 0 ? (int) i + 1 : (int) i;
        }

        public static int RoundedUpSqrt(double i)
        {
            return RoundUp(Math.Sqrt(i));
        }
    }
}
