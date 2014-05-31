using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzoftverSzigo.Util
{
    public class Util
    {
        public static string GenArrayStringResult<T>(T[] array)
        {
            var masolasOut = new StringBuilder();
            Array.ForEach(array, item => masolasOut.Append(String.Format("{0, 3} ", item)));
            return masolasOut.ToString();
        }

        public static string GenMulArrayStringResult<T>(T[,] array)
        {
            var result = new StringBuilder();
            int rowLength = array.GetLength(0);
            int colLength = array.GetLength(1);
            result.Append("    ");

            for (int i = 0; i < colLength; i++)
            {
                result.Append(string.Format("{0, 3} ", i));

            }
            result.Append(Environment.NewLine + Environment.NewLine);

            for (int i = 0; i < rowLength; i++)
            {
                result.Append(string.Format("{0, 3}|", i));
                for (int j = 0; j < colLength; j++)
                {
                    result.Append(string.Format("{0, 3} ", array[i, j]));
                }
                result.Append(Environment.NewLine + Environment.NewLine);
            }

            return result.ToString();
        }
    }
}
