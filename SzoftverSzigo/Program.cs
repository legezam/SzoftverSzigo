using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzoftverSzigo.BTree;


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
        static Random rand = new Random();

        static void Main(string[] args)
        {
            TesztEgyszeruTetelek();

            TesztOsszetettTetelek();

            TesztEgyszeruRendezesek();

            TesztOsszetettRendezesek();

            TesztKeresoFak();

            Console.ReadLine();
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
            Console.WriteLine("Inorder eredmény: [{0}]", GenArrayStringResult(inOrderResult.ToArray()));

            List<BinaryTreeNode<int>> preOrderResult = new List<BinaryTreeNode<int>>();
            bst.TraverseNode(TraverseMode.PreOrder, preOrderResult.Add);
            Console.WriteLine("Preorder eredmény: [{0}]", GenArrayStringResult(preOrderResult.ToArray()));

            List<BinaryTreeNode<int>> postOrderResult = new List<BinaryTreeNode<int>>();
            bst.TraverseNode(TraverseMode.PostOrder, postOrderResult.Add);
            Console.WriteLine("Postorder eredmény: [{0}]", GenArrayStringResult(postOrderResult.ToArray()));

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
            Console.WriteLine("Inorder eredmény: [{0}]", GenArrayStringResult(inOrderResult.ToArray()));
        }

        private static void TesztOsszetettRendezesek()
        {
            int tombMeret = 10;
            int[] tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] quickResult = OsszetettRendezesek.QuickRendezes(tomb, 0, tomb.Length - 1);
            Console.WriteLine("Quick rendezés: [{0}]\n", GenArrayStringResult(quickResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] mergeResult = OsszetettRendezesek.MergeRendezes(tomb, 0, tomb.Length - 1);
            Console.WriteLine("Merge rendezés: [{0}]\n", GenArrayStringResult(mergeResult));
        }

        private static void TesztEgyszeruRendezesek()
        {
            int tombMeret = 10;
            int[] tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] egyszeruResult = EgyszeruRendezesek.CseresRendezes(tomb, tomb.Length);
            Console.WriteLine("Egyszerű rendezés: [{0}]\n", GenArrayStringResult(egyszeruResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] minkivalasztResult = EgyszeruRendezesek.MinimumKivalasztas(tomb, tomb.Length);
            Console.WriteLine("Minimum kiválasztásos rendezés: [{0}]\n", GenArrayStringResult(minkivalasztResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] buborekResult = EgyszeruRendezesek.BuborekRendezes(tomb, tomb.Length);
            Console.WriteLine("Buborék rendezés: [{0}]\n", GenArrayStringResult(buborekResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] javitottBuborekResult = EgyszeruRendezesek.JavitottBuborekRendezes(tomb, tomb.Length);
            Console.WriteLine("Javított Buborék rendezés: [{0}]\n", GenArrayStringResult(javitottBuborekResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] beillesztesesResult = EgyszeruRendezesek.BeillesztesesRendezes(tomb, tomb.Length);
            Console.WriteLine("Beillesztéses rendezés: [{0}]\n", GenArrayStringResult(beillesztesesResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] javitottBeillesztesesResult = EgyszeruRendezesek.JavitottBeillesztesesRendezes(tomb, tomb.Length);
            Console.WriteLine("Javított Beillesztéses rendezés: [{0}]\n", GenArrayStringResult(javitottBeillesztesesResult));

            tomb = Enumerable.Repeat(0, tombMeret).Select(i => rand.Next(20)).ToArray();
            Console.WriteLine("Összetett tételek: \nkezdeti tömb: [{0}]", GenArrayStringResult(tomb));

            int[] shellResult = EgyszeruRendezesek.ShellRendezes(tomb, tomb.Length);
            Console.WriteLine("Shell rendezés: [{0}]\n", GenArrayStringResult(shellResult));
        }

        private static void TesztOsszetettTetelek()
        {
            int[] tomb = new[] {1, 2, 3, 4, 5, 6};
            int[] masikTomb = new[] {3, 4, 5, 6, 7, 8};
            Console.WriteLine("Összetett tételek: \nkezdeti tömb1: [{0}],\nkezdeti tömb2: [{1}]", GenArrayStringResult(tomb),
                GenArrayStringResult(masikTomb));

            //Másolás
            int[] masolasResult = OsszetettTetelek.Masolas(tomb, tomb.Length, i => i + 4);
            Console.WriteLine("Másolás: {0}", GenArrayStringResult(masolasResult));

            //Kiválogatás
            Tuple<int, int[]> kivalogatasResult = OsszetettTetelek.Kivalogatas(tomb, tomb.Length, i => i%2 == 1);
            Console.WriteLine("Kiválogatás: {0}db,  {1}", kivalogatasResult.Item1, GenArrayStringResult(kivalogatasResult.Item2));

            //Szétválogatás
            Tuple<Tuple<int, int[]>, Tuple<int, int[]>> szetvalogatasResult = OsszetettTetelek.Szetvalogatas(tomb,
                tomb.Length, i => i%2 == 0);
            Console.WriteLine("Szétválogatás: Y tömb:{0}db [{1}], Z tömb: {2}db [{3}]", szetvalogatasResult.Item1.Item1,
                GenArrayStringResult(szetvalogatasResult.Item1.Item2), szetvalogatasResult.Item2.Item1,
                GenArrayStringResult(szetvalogatasResult.Item2.Item2));

            //Metszet
            Tuple<int, int[]> metszetResult = OsszetettTetelek.Metszet(tomb, tomb.Length, masikTomb, masikTomb.Length);
            Console.WriteLine("Metszet: {0}db [{1}]", metszetResult.Item1, GenArrayStringResult(metszetResult.Item2));

            //Egyesítés
            Tuple<int, int[]> egyesitesResult = OsszetettTetelek.Egyesites(tomb, tomb.Length, masikTomb, masikTomb.Length);
            Console.WriteLine("Egyesítés: {0}db [{1}]", egyesitesResult.Item1, GenArrayStringResult(egyesitesResult.Item2));

            //Összefuttatás
            Tuple<int, int[]> osszefuttatasResult = OsszetettTetelek.Osszefuttatas(tomb, tomb.Length, masikTomb,
                masikTomb.Length);
            Console.WriteLine("Összefuttatás: {0}db [{1}]", osszefuttatasResult.Item1,
                GenArrayStringResult(osszefuttatasResult.Item2));
        }

        private static void TesztEgyszeruTetelek()
        {
            int[] tomb = new[] {1, 6, 3, 2, 5};

            //Sorozatszamitas
            int sorozatSzamitasResult = EgyszeruTetelek.SorozatSzamitas(0, tomb, tomb.Length, (i, j) => i + j);
            Console.WriteLine("Sorozatszámítás: {0}", sorozatSzamitasResult);

            //Eldontes
            bool eldontesResult = EgyszeruTetelek.Eldontes(tomb, tomb.Length, i => i%2 == 0);
            Console.WriteLine("Eldöntés: {0}", eldontesResult);

            //Kivalasztas
            int kivalasztasResult = EgyszeruTetelek.Kivalasztas(tomb, tomb.Length, i => i == 5);
            Console.WriteLine("Kiválasztás: {0}", kivalasztasResult);

            //Linearis kereses
            Tuple<bool, int> linKeresesResult = EgyszeruTetelek.LinearisKereses(tomb, tomb.Length, i => i > 3);
            Console.WriteLine("Lineáris keresés: {0}, {1}", linKeresesResult.Item1, linKeresesResult.Item2);

            //Megszámlálás
            int megszamlalasResult = EgyszeruTetelek.Megszamlalas(tomb, tomb.Length, i => i > 3);
            Console.WriteLine("Megszámlálás: {0}db", megszamlalasResult);

            //Maximumkiválasztás
            int maximumKivalasztasResult = EgyszeruTetelek.MaximumKivalasztas(tomb, tomb.Length);
            Console.WriteLine("Maximumkiválasztás: {0}", maximumKivalasztasResult);
        }

        private static StringBuilder GenArrayStringResult<T>(T[] masolasResult)
        {
            StringBuilder masolasOut = new StringBuilder();
            Array.ForEach(masolasResult, item => masolasOut.Append(string.Format("{0, 2}, ", item)));
            return masolasOut;
        }
    }
}
