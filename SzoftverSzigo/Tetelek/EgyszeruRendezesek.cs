namespace SzoftverSzigo {
    public class EgyszeruRendezesek
    {
        public static int[] CseresRendezes(int[] tomb, int tombMeret) 
        {
            for (int i = 0; i < tombMeret - 1; i++)
            {
                for (int j = i + 1; j < tombMeret; j++)
                {
                    if (tomb[i] > tomb[j])
                    {
                        Util.Util.Csere(tomb, i, j);
                    }
                }
            }

            return tomb;
        }

        public static int[] MinimumKivalasztas(int[] tomb, int tombMeret)
        {
            for (int i = 0; i < tombMeret - 1; i++)
            {
                int MIN = i;
                for (int j = i + 1; j < tombMeret; j++)
                {
                    if (tomb[MIN] > tomb[j])
                    {
                        MIN = j;
                    } 
                }
                Util.Util.Csere(tomb, i, MIN);
            }
            return tomb;
        }

        public static int[] BuborekRendezes(int[] tomb, int tombMeret) 
        {
            for (int i = tombMeret - 1; i != 0; i--)
            {
                for (int j = 0; j < i ; j++)
                {
                    if (tomb[j] > tomb[j + 1])
                    {
                        Util.Util.Csere(tomb, j, j + 1);
                    }
                }
            }
            return tomb;
        }

        public static int[] JavitottBuborekRendezes(int[] tomb, int tombMeret) 
        {
            int i = tombMeret - 1;

            while (i >= 1)
            {
                int CS = 0;
                for (int j = 0; j < i ; j++)
                {
                    if (tomb[j] > tomb[j + 1])
                    {
                        Util.Util.Csere(tomb, j, j + 1);
                        CS = j;
                    }
                }
                i = CS;
            }
            return tomb;
        }

        public static int[] BeillesztesesRendezes(int[] tomb, int tombMeret)
        {
            for (int i = 1; i < tombMeret; i++)
            {
                int j = i - 1;
                while (j >= 0 && tomb[j] > tomb[j + 1])
                {
                    Util.Util.Csere(tomb, j, j + 1);
                    j = j - 1;
                }
            }
            
            return tomb;
        }

        public static int[] JavitottBeillesztesesRendezes(int[] tomb, int tombMeret)
        {
            for (int i = 1; i < tombMeret; i++)
            {
                int j = i - 1;
                int Y = tomb[i];
                while (j >= 0 && tomb[j] > Y)
                {
                    tomb[j + 1] = tomb[j];
                    j = j - 1;
                }
                tomb[j + 1] = Y;
            }

            return tomb;
        }

        //az ablakméret eredeti értéke és csökkenésének algoritmusa is tetszőleges, mindennel fog működni, csak nem ugyanolyan hatékonyan.
        public static int[] ShellRendezes(int[] tomb, int tombMeret)
        {
            int ablakMeret = tombMeret;

            while (ablakMeret > 0)
            {
                for (int k = ablakMeret; k <= 2 * ablakMeret; k++)
                {
                    for (int i = k; i < tombMeret; i = i + ablakMeret)
                    {
                        int j = i - ablakMeret;
                        int Y = tomb[i];
                        while (j >= 0 && tomb[j] > Y)
                        {
                            tomb[j + ablakMeret] = tomb[j];
                            j = j - ablakMeret;
                        }
                        tomb[j + ablakMeret] = Y;
                    }
                }
                ablakMeret = ablakMeret/ 2;
            }


            return tomb;
        }
    }
}
