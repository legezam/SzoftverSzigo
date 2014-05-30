using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzoftverSzigo
{
    public class OsszetettRendezesek
    {
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

        private static void Merge(int [] tomb, int elso, ref int kozepso, int utolso)
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
       
    }
}
