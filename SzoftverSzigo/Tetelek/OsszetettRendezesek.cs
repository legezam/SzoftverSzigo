using System;
using System.Collections.Generic;

namespace SzoftverSzigo
{
    public class OsszetettRendezesek
    {
        #region QuickSort

        public static int[] QuickRendezes(int[] tomb, int elso, int utolso)
        {
            int kozepso = (elso + utolso)/2;

            Szetvalogat(tomb, elso, utolso, ref kozepso);
            if (kozepso - elso > 1)
            {
                QuickRendezes(tomb, elso, kozepso - 1);
            }
            if (utolso - kozepso > 1)
            {
                QuickRendezes(tomb, kozepso + 1, utolso);
            }
            return tomb;
        }

        private static void Szetvalogat(int[] tomb, int elso, int utolso, ref int kozepso)
        {
            kozepso = elso;
            int jobb = utolso;
            int A = tomb[kozepso];

            while (kozepso < jobb)
            {
                while (kozepso < jobb && tomb[jobb] >= A)
                {
                    jobb = jobb - 1;
                }
                if (kozepso < jobb)
                {
                    tomb[kozepso] = tomb[jobb];
                    kozepso = kozepso + 1;
                    while (kozepso < jobb && tomb[kozepso] <= A)
                    {
                        kozepso = kozepso + 1;
                    }
                    if (kozepso < jobb)
                    {
                        tomb[jobb] = tomb[kozepso];
                        jobb = jobb - 1;
                    }
                }
            }
            tomb[kozepso] = A;
        }

        #endregion

        #region MergeSort

        public static int[] MergeRendezes(int[] tomb, int elso, int utolso)
        {
            if (elso < utolso)
            {
                int kozepso = (elso + utolso)/2;
                MergeRendezes(tomb, elso, kozepso);
                MergeRendezes(tomb, kozepso + 1, utolso);
                Merge(tomb, elso, ref kozepso, utolso);
            }
            return tomb;
        }

        private static void Merge(int[] tomb, int elso, ref int kozepso, int utolso)
        {
            int alsoHossz = kozepso - elso + 1;
            int felsoHossz = utolso - kozepso;

            List<int> balList = new List<int>();
            List<int> jobbList = new List<int>();
            for (int i = 1; i <= alsoHossz; i++)
            {
                balList.Add(tomb[elso + i - 1]);
            }
            for (int j = 1; j <= felsoHossz; j++)
            {
                jobbList.Add(tomb[kozepso + j]);
            }
            balList.Add(int.MaxValue);
            jobbList.Add(int.MaxValue);
            int k = 0;
            int l = 0;

            for (int m = elso; m <= utolso; m++)
            {
                if (balList[k] <= jobbList[l])
                {
                    tomb[m] = balList[k];
                    k = k + 1;
                }
                else
                {
                    tomb[m] = jobbList[l];
                    l = l + 1;
                }
            }
        }

        #endregion

        #region KupacSort
        /// <summary>
        /// Nem egyszerű ezt a dolgot összehozni. A kupacolban lévő 2*i miatt nehéz 0 indexxel kezdődő tömbökkel megvalósítani.
        /// Ezért a tömböt a KupacRendezés elején átalakítottam 1-es index-ű tömbbé és úgy megy tovább.
        /// </summary>
        public static Array KupacRendezes(int[] tomb, int tombMeret)
        {
            var oneBasedArray = CreateOneBasedArray(tomb, tombMeret);

            KupacotEpit(oneBasedArray, tombMeret);
            for (int i = tombMeret; i >= 2; i--)
            {
                Csere(oneBasedArray, 1, i);
                tombMeret = tombMeret - 1;
                KupacotEpit(oneBasedArray, tombMeret);
            }
            return oneBasedArray;
        }

        private static void KupacotEpit(Array tomb, int tombMeret)
        {
            for (int i = tombMeret / 2; i >= 1; i--)
            {
                Kupacol(tomb, i, tombMeret);
            }
        }

        private static void Kupacol(Array tomb, int i, int tombMeret)
        {
            int bal = 2*i;
            int jobb = 2*i + 1;
            int MAX = 0;
            if (bal <= tombMeret && (int)tomb.GetValue(bal) > (int)tomb.GetValue(i))
            {
                MAX = bal;
            }
            else
            {
                MAX = i;
            }

            if (jobb <= tombMeret && (int)tomb.GetValue(jobb)  > (int)tomb.GetValue(MAX))
            {
                MAX = jobb;
            }

            if (MAX != i)
            {
                Csere(tomb, i, MAX);
                Kupacol(tomb, MAX, tombMeret);
            }
        }


        #endregion

        private static void Csere(Array tomb, int i, int j)
        {
            object tmp = tomb.GetValue(i);
            tomb.SetValue(tomb.GetValue(j), i); 
            tomb.SetValue(tmp, j);
        }

        private static Array CreateOneBasedArray(int[] tomb, int tombMeret)
        {
            var oneBasedArray = Array.CreateInstance(typeof(int), new[] { tombMeret }, new[] { 1 });
            for (int i = 1; i <= oneBasedArray.Length; i++)
            {
                oneBasedArray.SetValue(tomb[i - 1], i);
            }
            return oneBasedArray;
        }
    }
}
