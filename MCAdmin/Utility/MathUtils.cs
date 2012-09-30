using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCAdmin.Utility
{
    public class MathUtils
    {
        /// <summary>
        /// Calculates the average in the collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static int Avg(int[] collection)
        {
            return collection.Sum() / collection.Length;
        }
    }
}
