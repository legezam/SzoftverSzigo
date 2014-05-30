using System;

namespace SzoftverSzigo
{
    public class EgyszeruTetelek
    {
        public static int SorozatSzamitas(int R0, int[] tomb, int tombMeret, Func<int, int, int> muvelet)
        {
            int R = R0;

            for (int i = 0; i < tombMeret; i++)
            {
                R = muvelet(R, tomb[i]);
            }

            return R;
        }

        public static bool Eldontes(int[] tomb, int tombMeret, Func<int, bool> tulajdonsagFuggveny)
        {
            bool VAN;
            int i = 0;

            while (i < tombMeret && !tulajdonsagFuggveny(tomb[i]))
            {
                i = i + 1;
            }
            VAN = i <= tombMeret;
            return VAN;
        }

        public static int Kivalasztas(int[] tomb, int tombMeret, Func<int, bool> tulajdonsagFuggveny)
        {
            int SORSZ;

            int i = 0;
            while (i < tombMeret && !tulajdonsagFuggveny(tomb[i]))
            {
                i = i + 1;
            }
            SORSZ = i;
            return SORSZ;
        }

        public static Tuple<bool, int> LinearisKereses(int[] tomb, int tombMeret, Func<int, bool> tulajdonsagFuggveny)
        {
            bool VAN;
            int SORSZ = -1;
            int i = 0;

            while (i < tombMeret && !tulajdonsagFuggveny(tomb[i]))
            {
                i = i + 1;
            }

            VAN = i <= tombMeret;

            if (VAN)
            {
                SORSZ = i;
            }

            return new Tuple<bool, int>(VAN, SORSZ);
        }

        public static int Megszamlalas(int[] tomb, int tombMeret, Func<int, bool> tulajdonsagFuggveny)
        {
            int DB = 0;

            for (int i = 0; i < tombMeret; i++)
            {
                if (tulajdonsagFuggveny(tomb[i]))
                {
                    DB = DB + 1;
                }
            }
            return DB;
        }

        public static int MaximumKivalasztas(int[] tomb, int tombMeret) 
        {
            int MAX = 0;

            for (int i = 1; i < tombMeret; i++)
            {
                if (tomb[i] > tomb[MAX])
                {
                    MAX = i;
                }
            }

            return MAX;
        }
    } 
}
