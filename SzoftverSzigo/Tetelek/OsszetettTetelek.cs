using System;
using System.Collections.Generic;

namespace SzoftverSzigo
{
    public class OsszetettTetelek
    {
        public static int[] Masolas(int[] tomb, int tombMeret, Func<int, int> muvelet)
        {
            int[] result = new int[tombMeret];

            for (int i = 0; i < tombMeret; i++)
            {
                result[i] = muvelet(tomb[i]);
            }

            return result;
        }

        public static Tuple<int, int[]> Kivalogatas(int[] tomb, int tombMeret, Func<int, bool> muvelet)
        {
            int DB = 0;
            List<int> result = new List<int>();

            for (int i = 0; i < tombMeret; i++)
            {
                if (muvelet(tomb[i]))
                {
                    DB = DB + 1;
                    result.Add(tomb[i]);
                }
            }

            return new Tuple<int, int[]>(DB, result.ToArray());
        }

        public static Tuple<Tuple<int, int[]>, Tuple<int, int[]>> Szetvalogatas(int[] tomb, int tombMeret,
            Func<int, bool> tulajdonsagFuggveny)
        {
            int DBY = 0;
            int DBZ = 0;
            List<int> Y = new List<int>();
            List<int> Z = new List<int>();

            for (int i = 0; i < tombMeret; i++)
            {
                if (tulajdonsagFuggveny(tomb[i]))
                {
                    DBY = DBY + 1;
                    Y.Add(tomb[i]);
                }
                else
                {
                    DBZ = DBZ + 1;
                    Z.Add(tomb[i]);
                }
            }
            var Yresult = new Tuple<int, int[]>(DBY, Y.ToArray());
            var Zresult = new Tuple<int, int[]>(DBZ, Z.ToArray());
            return new Tuple<Tuple<int, int[]>, Tuple<int, int[]>>(Yresult, Zresult);
        }

        public static Tuple<int, int[]> Metszet(int[] elsoTomb, int elsoMeret, int[] masodikTomb, int masodikMeret) 
        {
            int DB = 0;
            List<int> Z = new List<int>();

            for (int i = 0; i < elsoMeret; i++)
            {
                int j = 0;
                while (j < masodikMeret && elsoTomb[i] != (masodikTomb[j]))
                {
                    j = j + 1;
                }
                if (j < masodikMeret)
                {
                    DB = DB + 1;
                    Z.Add(elsoTomb[i]);
                }
            }

            return new Tuple<int, int[]>(DB, Z.ToArray());
        }

        public static Tuple<int, int[]> Egyesites(int[] elsoTomb, int elsoMeret, int[] masodikTomb, int masodikMeret) 
        {
            int DB = elsoMeret;
            List<int> Z = new List<int>();
            Z.AddRange(elsoTomb);

            for (int j = 0; j < masodikMeret; j++)
            {
                int i = 0;
                while (i < elsoMeret && elsoTomb[i] != masodikTomb[j])
                {
                    i = i + 1;
                }
                if (i >= elsoMeret)
                {
                    DB = DB + 1;
                    Z.Add(masodikTomb[j]);
                }
            }

            return new Tuple<int, int[]>(DB, Z.ToArray());
        }

        public static Tuple<int, int[]> Osszefuttatas(int[] elsoTomb, int elsoMeret, int[] masodikTomb, int masodikMeret) 
        {
            int i = 0;
            int j = 0;
            int DB = 0;
            List<int> Z = new List<int>();


            while (i < elsoMeret && j < masodikMeret)
            {
                DB = DB + 1;
                if (elsoTomb[i] < masodikTomb[j])
                {
                    Z.Add(elsoTomb[i]);
                    i = i + 1;
                }
                else if (elsoTomb[i] == masodikTomb[j])
                {
                    Z.Add(elsoTomb[i]);
                    i = i + 1;
                    j = j + 1;
                }
                else
                {
                    Z.Add(masodikTomb[j]);
                    j = j + 1;
                }
            }

            while (i < elsoMeret)
            {
                DB = DB + 1;
                Z.Add(elsoTomb[i]);
                i = i + 1;
            }

            while (j < masodikMeret)
            {
                DB = DB + 1;
                Z.Add(masodikTomb[j]);
                j = j + 1;
            }


            return new Tuple<int, int[]>(DB, Z.ToArray());
        } 

    }
}
