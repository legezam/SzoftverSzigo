using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzoftverSzigo.BTree;
using SzoftverSzigo.Tetelek;


namespace SzoftverSzigo
{
    /// <summary>
    /// Apróbetűs rész:
    /// - semmilyen felelősséget nem vállalok a tartalom helyességét illetően
    /// - a cél az volt, hogy lehetőleg minél inkább hasonlítson a diákon lévő algoritmusokra, szóval nincsen benne egy "faszább quicksort a netről"
    /// - van mindenhez teszt, lehet nyomkodni
    /// </summary>
    class Program
    {
        static readonly Random rand = new Random();

        static void Main(string[] args)
        {
            TesztEgyszeruTetelek();

            TesztOsszetettTetelek();

            TesztEgyszeruRendezesek();

            TesztOsszetettRendezesek();

            TesztKeresoFak();

            TesztMohoDinamikus();

            Console.ReadLine();
        }

        private static void TesztMohoDinamikus()
        {
            int[] visszaadasResult = MohoDinamikusAlgoritmusok.PenzKifizetes(79845);
            Console.WriteLine("Mohó pénzvisszaadás eredmény: 79845 = [{0}]\n", Util.Util.GenArrayStringResult(visszaadasResult));

            var mohoKincsResult = MohoDinamikusAlgoritmusok.MohoKincsetKeres();
            Console.WriteLine("Mohó kincseresés eredmény: [{0}]\n", Util.Util.GenArrayStringResult(mohoKincsResult));
            Console.WriteLine("A visszaadott zárójeles kifejezések értéke:\n" +
                              "1. lépés száma\n" +
                              "2. i értéke\n" +
                              "3. j értéke\n" +
                              "5. lépés során mekkora értékű mezőre lépett\n");

            int dinamikusKincsresult = MohoDinamikusAlgoritmusok.DinamikusKincsetKeres(3, 3);
            Console.WriteLine("Dinamikus kincskeresés eredmény: legtávolabbi mező értéke: {0}\n", dinamikusKincsresult);

            Console.WriteLine("Dinamikus programozás kincskeresés eredmény:");
            Console.WriteLine(Util.Util.GenMulArrayStringResult(MohoDinamikusAlgoritmusok.DinamikusKincsetKeres()));

            var mohoHatizsakResult = MohoDinamikusAlgoritmusok.MohoHatizsak();
            Console.WriteLine("Mohó hátizsák eredmény: [{0}]\n", Util.Util.GenArrayStringResult(mohoHatizsakResult));
            Console.WriteLine("A visszaadott zárójeles kifejezések értéke:\n" +
                              "1. tárgy értéke\n" +
                              "2. tárgy súlya\n");

            var dinamikusHatizsakResult = MohoDinamikusAlgoritmusok.DinamikusHatizsak();
            Console.WriteLine("Dinamikus hátizsák eredmény: [{0}]\n", Util.Util.GenArrayStringResult(dinamikusHatizsakResult));
            Console.WriteLine("A visszaadott zárójeles kifejezések értéke:\n" +
                              "1. tárgy értéke\n" +
                              "2. tárgy súlya\n");

            int rekurzivLCS = MohoDinamikusAlgoritmusok.RekurzivLCS();
            int dinamikusLCS = MohoDinamikusAlgoritmusok.DinamikusLCS();
            Console.WriteLine("Rekurzív LCS eredménye: {0}, Dinamikus LCS eredménye: {1}", rekurzivLCS, dinamikusLCS);
        }

        private static void TesztKeresoFak()
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();

            bst.AddNode(10);
            bst.AddNode(4);
            bst.AddNode(6);
            bst.AddNode(3);
            bst.AddNode(1);
            bst.AddNode(7);
            bst.AddNode(5);
            bst.AddNode(-1);

            List<BinaryTreeNode<int>> inOrderResult = new List<BinaryTreeNode<int>>();
            bst.TraverseNode(TraverseMode.InOrder, inOrderResult.Add);
            Console.WriteLine("Inorder eredmény: [{0}]", Util.Util.GenArrayStringResult(inOrderResult.ToArray()));

            List<BinaryTreeNode<int>> preOrderResult = new List<BinaryTreeNode<int>>();
            bst.TraverseNode(TraverseMode.PreOrder, preOrderResult.Add);
            Console.WriteLine("Preorder eredmény: [{0}]", Util.Util.GenArrayStringResult(preOrderResult.ToArray()));

            List<BinaryTreeNode<int>> postOrderResult = new List<BinaryTreeNode<int>>();
            bst.TraverseNode(TraverseMode.PostOrder, postOrderResult.Add);
            Console.WriteLine("Postorder eredmény: [{0}]", Util.Util.GenArrayStringResult(postOrderResult.ToArray()));

            var searchResult = bst.SearchNode(-20);
            searchResult = bst.SearchNode(4);
            bst.RemoveNode(4);
            searchResult = bst.SearchNode(4);
            searchResult = bst.SearchNode(5);
            bst.RemoveNode(6);
            searchResult = bst.SearchNode(6);
            bst.RemoveNode(-1);

            inOrderResult = new List<BinaryTreeNode<int>>();
            bst.TraverseNode(TraverseMode.InOrder, inOrderResult.Add);
            Console.WriteLine("Inorder eredmény: [{0}]", Util.Util.GenArrayStringResult(inOrderResult.ToArray()));
        }

        private static void TesztOsszetettRendezesek()
        {
            const int tombMeret = 10;
            int[] tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] quickResult = OsszetettRendezesek.QuickRendezes(tomb, 0, tomb.Length - 1);
            Console.WriteLine("Quick rendezés: [{0}]\n", Util.Util.GenArrayStringResult(quickResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] mergeResult = OsszetettRendezesek.MergeRendezes(tomb, 0, tomb.Length - 1);
            Console.WriteLine("Merge rendezés: [{0}]\n", Util.Util.GenArrayStringResult(mergeResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] kupacresult = OsszetettRendezesek.KupacRendezes(tomb, tomb.Length);
            Console.WriteLine("Kupac rendezés: [{0}]\n", Util.Util.GenArrayStringResult(kupacresult));
        }

        private static void TesztEgyszeruRendezesek()
        {
            const int tombMeret = 10;
            int[] tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] egyszeruResult = EgyszeruRendezesek.CseresRendezes(tomb, tomb.Length);
            Console.WriteLine("Egyszerű rendezés: [{0}]\n", Util.Util.GenArrayStringResult(egyszeruResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] minkivalasztResult = EgyszeruRendezesek.MinimumKivalasztas(tomb, tomb.Length);
            Console.WriteLine("Minimum kiválasztásos rendezés: [{0}]\n", Util.Util.GenArrayStringResult(minkivalasztResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] buborekResult = EgyszeruRendezesek.BuborekRendezes(tomb, tomb.Length);
            Console.WriteLine("Buborék rendezés: [{0}]\n", Util.Util.GenArrayStringResult(buborekResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] javitottBuborekResult = EgyszeruRendezesek.JavitottBuborekRendezes(tomb, tomb.Length);
            Console.WriteLine("Javított Buborék rendezés: [{0}]\n", Util.Util.GenArrayStringResult(javitottBuborekResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] beillesztesesResult = EgyszeruRendezesek.BeillesztesesRendezes(tomb, tomb.Length);
            Console.WriteLine("Beillesztéses rendezés: [{0}]\n", Util.Util.GenArrayStringResult(beillesztesesResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] javitottBeillesztesesResult = EgyszeruRendezesek.JavitottBeillesztesesRendezes(tomb, tomb.Length);
            Console.WriteLine("Javított Beillesztéses rendezés: [{0}]\n", Util.Util.GenArrayStringResult(javitottBeillesztesesResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", Util.Util.GenArrayStringResult(tomb));

            int[] shellResult = EgyszeruRendezesek.ShellRendezes(tomb, tomb.Length);
            Console.WriteLine("Shell rendezés: [{0}]\n", Util.Util.GenArrayStringResult(shellResult));
        }

        private static void TesztOsszetettTetelek()
        {
            
            int[] tomb = {1, 2, 3, 4, 5, 6};
            int[] masikTomb = {3, 4, 5, 6, 7, 8};
            Console.WriteLine("Összetett tételek: \nkezdeti tömb1: [{0}],\nkezdeti tömb2: [{1}]\n", Util.Util.GenArrayStringResult(tomb),
                Util.Util.GenArrayStringResult(masikTomb));

            //Másolás
            int[] masolasResult = OsszetettTetelek.Masolas(tomb, tomb.Length, i => i + 4);
            Console.WriteLine("Másolás: {0}\n", Util.Util.GenArrayStringResult(masolasResult));

            //Kiválogatás
            Tuple<int, int[]> kivalogatasResult = OsszetettTetelek.Kivalogatas(tomb, tomb.Length, i => i%2 == 1);
            Console.WriteLine("Kiválogatás: {0}db,  {1}\n", kivalogatasResult.Item1, Util.Util.GenArrayStringResult(kivalogatasResult.Item2));

            //Szétválogatás
            Tuple<Tuple<int, int[]>, Tuple<int, int[]>> szetvalogatasResult = OsszetettTetelek.Szetvalogatas(tomb,
                tomb.Length, i => i%2 == 0);
            Console.WriteLine("Szétválogatás: Y tömb:{0}db [{1}], Z tömb: {2}db [{3}]\n", szetvalogatasResult.Item1.Item1,
                Util.Util.GenArrayStringResult(szetvalogatasResult.Item1.Item2), szetvalogatasResult.Item2.Item1,
                Util.Util.GenArrayStringResult(szetvalogatasResult.Item2.Item2));

            //Metszet
            Tuple<int, int[]> metszetResult = OsszetettTetelek.Metszet(tomb, tomb.Length, masikTomb, masikTomb.Length);
            Console.WriteLine("Metszet: {0}db [{1}]\n", metszetResult.Item1, Util.Util.GenArrayStringResult(metszetResult.Item2));

            //Egyesítés
            Tuple<int, int[]> egyesitesResult = OsszetettTetelek.Egyesites(tomb, tomb.Length, masikTomb, masikTomb.Length);
            Console.WriteLine("Egyesítés: {0}db [{1}]\n", egyesitesResult.Item1, Util.Util.GenArrayStringResult(egyesitesResult.Item2));

            //Összefuttatás
            Tuple<int, int[]> osszefuttatasResult = OsszetettTetelek.Osszefuttatas(tomb, tomb.Length, masikTomb,
                masikTomb.Length);
            Console.WriteLine("Összefuttatás: {0}db [{1}]\n", osszefuttatasResult.Item1,
                Util.Util.GenArrayStringResult(osszefuttatasResult.Item2));
        }

        private static void TesztEgyszeruTetelek()
        {
            int[] tomb = new[] {1, 6, 3, 2, 5};

            //Sorozatszamitas
            int sorozatSzamitasResult = EgyszeruTetelek.SorozatSzamitas(0, tomb, tomb.Length, (i, j) => i + j);
            Console.WriteLine("Sorozatszámítás: {0}\n", sorozatSzamitasResult);

            //Eldontes
            bool eldontesResult = EgyszeruTetelek.Eldontes(tomb, tomb.Length, i => i%2 == 0);
            Console.WriteLine("Eldöntés: {0}\n", eldontesResult);

            //Kivalasztas
            int kivalasztasResult = EgyszeruTetelek.Kivalasztas(tomb, tomb.Length, i => i == 5);
            Console.WriteLine("Kiválasztás: {0}\n", kivalasztasResult);

            //Linearis kereses
            Tuple<bool, int> linKeresesResult = EgyszeruTetelek.LinearisKereses(tomb, tomb.Length, i => i > 3);
            Console.WriteLine("Lineáris keresés: {0}, {1}\n", linKeresesResult.Item1, linKeresesResult.Item2);

            //Megszámlálás
            int megszamlalasResult = EgyszeruTetelek.Megszamlalas(tomb, tomb.Length, i => i > 3);
            Console.WriteLine("Megszámlálás: {0}db\n", megszamlalasResult);

            //Maximumkiválasztás
            int maximumKivalasztasResult = EgyszeruTetelek.MaximumKivalasztas(tomb, tomb.Length);
            Console.WriteLine("Maximumkiválasztás: {0}\n", maximumKivalasztasResult);
        }
    }
}
