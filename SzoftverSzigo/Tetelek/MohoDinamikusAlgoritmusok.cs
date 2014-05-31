using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzoftverSzigo.Tetelek
{
    public class MohoDinamikusAlgoritmusok
    {
        #region Penzkifizetes

        private static int[] Cimletek = {20000, 10000, 5000, 2000, 1000, 500, 200, 100, 50, 20, 10, 5};

        public static int[] PenzKifizetes(int vegosszeg)
        {
            List<int> result = new List<int>();
            while (vegosszeg != 0)
            {
                int ujCimlet = GetLegnagyobbCimlet(vegosszeg);
                if (ujCimlet == 0)
                {
                    return null;
                }
                vegosszeg = vegosszeg - ujCimlet;
                result.Add(ujCimlet);
            }
            return result.ToArray();
        }

        private static int GetLegnagyobbCimlet(int vegosszeg)
        {
            int result = 0;
            int i = 0;
            while (i < Cimletek.Length && Cimletek[i] > vegosszeg)
            {
                i = i + 1;
            }
            if (i >= Cimletek.Length)
            {
                result = 0;
            }
            else
            {
                result = Cimletek[i];
            }
            return result;
        }

        #endregion

        #region KincsBegyujtes

        private static readonly int[,] KincsesTerkep =
        {
            {1, 3, 4, 8},
            {6, 10, 20, 9},
            {11, 2, 15, 1},
            {1, 5, 3, 6}
        };

        #region Moho
        /// <summary>
        /// A visszaadott Tuple eredményben az értékek:
        /// 1. lépés száma
        /// 2. i értéke
        /// 3. j értéke
        /// 4. lépés során mekkora értékű mezőre lépett
        /// </summary>
        /// <returns></returns>
        public static Tuple<int, int, int, int>[] MohoKincsetKeres()
        {
            int terkepMeret = (int)Math.Sqrt(KincsesTerkep.Length);
            var result = new List<Tuple<int, int, int, int>>();
            int i = 0;
            int j = 0;
            while (i < terkepMeret - 1 && j < terkepMeret - 1)
            {
                if (KincsesTerkep[i + 1, j] > KincsesTerkep[i, j + 1])
                {
                    AddLepes(i, j, result);
                    i = i + 1;
                }
                else
                {
                    AddLepes(i, j, result);
                    j = j + 1;
                }
            }
            while (i < terkepMeret - 1)
            {
                AddLepes(i, j, result);
                i = i + 1;
            }
            while (j < terkepMeret - 1)
            {
                AddLepes(i, j, result);
                j = j + 1;
            }

            AddLepes(i, j, result);
            return result.ToArray();
        }

        private static void AddLepes(int i, int j, List<Tuple<int, int, int, int>> statisztika)
        {
            int lepesSzam = i + j;
            int kincsErtek = KincsesTerkep[i, j];

            statisztika.Add(new Tuple<int, int, int, int>(
                lepesSzam, i, j, kincsErtek
                ));
        } 
        #endregion

        #region Dinamikus
        public static int DinamikusKincsetKeres(int i, int j)
        {
            if (i == 0 && j == 0)
            {
                return KincsesTerkep[i, j];
            }
            else if (i != 0 && j == 0)
            {
                return KincsesTerkep[i, j] + DinamikusKincsetKeres(i - 1, j);
            }
            else if (i == 0 && j != 0)
            {
                return KincsesTerkep[i, j] + DinamikusKincsetKeres(i, j - 1);
            }
            else
            {
                return KincsesTerkep[i, j] + Math.Max(DinamikusKincsetKeres(i - 1, j), DinamikusKincsetKeres(i, j - 1));
            }
        }

        //A dián lévő számok között van rossz. Pl a 18...16-nál 16 helyett 20 van!
        private static int[,] DinamikusTerkep;
        public static void DinamikusKincsetKeres()
        {
            int kincsesTerkepMeret = (int) Math.Sqrt(KincsesTerkep.Length);
            DinamikusTerkep = new int[kincsesTerkepMeret, kincsesTerkepMeret];
            for (int i = 0; i < kincsesTerkepMeret; i++)
            {
                for (int j = 0; j < kincsesTerkepMeret; j++)
                {
                    DinamikusTerkep[i, j] = DinamikusKincsetKeres(i, j);
                }
            }
        }
        #endregion

        #endregion

        #region Hátizsák

        /// <summary>
        /// Tuple értékek:
        /// 1. tárgy értéke
        /// 2. tárgy súlya
        /// </summary>
        private static readonly List<Tuple<int, int>> Targyak = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(60, 10),
            new Tuple<int, int>(100, 20),
            new Tuple<int, int>(120, 30)
        };

        private static readonly int HatizsakMeret = 50;

        public static Tuple<int, int>[] MohoHatizsak()
        {
            int fennMaradoKapacitas = HatizsakMeret;
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            int i = 0;

            while (fennMaradoKapacitas > 0 && i < Targyak.Count)
            {
                if (Targyak[i].Item2 <= fennMaradoKapacitas)
                {
                    result.Add(Targyak[i]);
                    fennMaradoKapacitas = fennMaradoKapacitas - Targyak[i].Item2;
                }
                i = i + 1;
            }
            return result.ToArray();
        } 

        #endregion
    }


}
