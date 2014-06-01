using System;
using System.Collections.Generic;

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
        public static int[,] DinamikusKincsetKeres()
        {
            int kincsesTerkepMeret = (int) Math.Sqrt(KincsesTerkep.Length);
            int[,] DinamikusTerkep = new int[kincsesTerkepMeret, kincsesTerkepMeret];
            for (int i = 0; i < kincsesTerkepMeret; i++)
            {
                for (int j = 0; j < kincsesTerkepMeret; j++)
                {
                    DinamikusTerkep[i, j] = DinamikusKincsetKeres(i, j);
                }
            }
            return DinamikusTerkep;
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
            new Tuple<int, int>(int.MinValue , int.MaxValue),//dummy, indexelés miatt kell. Ezt soha nem nézi az algo
            new Tuple<int, int>(3, 2),
            new Tuple<int, int>(4, 3),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(6, 5)
        };

        private const int HatizsakMeret = 6;

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

        public static Tuple<int, int>[] DinamikusHatizsak()
        {
            int[,] eredmenyTomb = GenerateTablazat(Targyak, Targyak.Count , HatizsakMeret);
            Tuple<int, int>[] eredmenyIndexek = EredmenytKiolvas(eredmenyTomb, Targyak);
            return eredmenyIndexek;
        }

        private static Tuple<int,int>[] EredmenytKiolvas(int[,] eredmenyTomb, List<Tuple<int, int>> targyak)
        {
            int targy = eredmenyTomb.GetLength(0) - 1;
            int suly = eredmenyTomb.GetLength(1) - 1;
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            while (targy > 0 && suly > 0)
            {
                if (eredmenyTomb[targy, suly] != eredmenyTomb[targy-1, suly])
                {
                    result.Add(targyak[targy]);
                    suly = suly - targyak[targy].Item2;
                }
                targy = targy - 1;
            }

            return result.ToArray();
        }

        private static int[,] GenerateTablazat(List<Tuple<int, int>> targyak, int targyakSzama, int hatizsakMeret)
        {
            int[,] eredmeny = new int[targyakSzama,hatizsakMeret + 1];
            for (int maxTargy = 1; maxTargy < targyakSzama; maxTargy++)
            {
                for (int maxSuly = 1; maxSuly <= hatizsakMeret; maxSuly++)
                {
                    var item = targyak[maxTargy];
                    int ertek = item.Item1;
                    int suly = item.Item2;
                    if (suly <= maxSuly)
                    {
                        eredmeny[maxTargy, maxSuly] = Math.Max(eredmeny[maxTargy - 1, maxSuly],
                            eredmeny[maxTargy - 1, maxSuly - suly] + ertek);
                    }
                    else
                    {
                        eredmeny[maxTargy, maxSuly] = eredmeny[maxTargy - 1, maxSuly];
                    }
                }
            }
            return eredmeny;
        }
        #endregion

        #region LCS

        private static readonly string[] EgyikTomb = {"XXX", "A", "B", "C", "B", "D", "A", "B"};
        private static readonly string[] MasikTomb = {"YYY", "B", "D", "C", "A", "B", "A"};
        public static int RekurzivLCS()
        {
            return RekurzivLCS(EgyikTomb, EgyikTomb.Length, MasikTomb, MasikTomb.Length);
        }

        private static int RekurzivLCS(string[] tombX, int tombXMeret, string[] tombY, int tombYMeret)
        {
            if (tombXMeret == 0 || tombYMeret == 0)
            {
                return 0;
            }
            if (tombX[tombXMeret - 1] == tombY[tombYMeret - 1])
            {
                return RekurzivLCS(tombX, tombXMeret - 1, tombY, tombYMeret - 1) + 1;
            }
            else
            {
                return Math.Max(RekurzivLCS(tombX, tombXMeret - 1, tombY, tombYMeret),
                    RekurzivLCS(tombX, tombXMeret, tombY, tombYMeret - 1));
            }
        }

        public static int DinamikusLCS()
        {
            var result = DinamikusLCS(EgyikTomb, MasikTomb);
            return result[result.GetLength(0)-1,result.GetLength(1)-1];
        }

        private static int[,] DinamikusLCS(string[] tombX, string[] tombY)
        {
            int[,] eredmeny = new int[tombX.Length, tombY.Length];
            for (int i = 1; i < tombX.Length; i++)
            {
                for (int j = 1; j < tombY.Length; j++)
                {
                    if (tombX[i] == tombY[j])
                    {
                        eredmeny[i, j] = eredmeny[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        eredmeny[i, j] = Math.Max(eredmeny[i - 1, j], eredmeny[i, j - 1]);
                    }
                }
            }
            string res = Util.Util.GenMulArrayStringResult(eredmeny);
            return eredmeny;
        }
        #endregion
    }


}
