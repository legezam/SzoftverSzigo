using System.Collections.Generic;
using System.Linq;
using System.Text;
using SzoftverSzigo.Trees.Default;

namespace SzoftverSzigo.Trees.BTree
{
    public class BTreeNode<T> : Node<T>
    {
        private readonly int degree;

        public BTreeNode(int degree)
        {
            this.degree = degree;
            this.Children = new NodeList<T>();
            this.Entries = new List<BTreeEntry<T>>(degree);
        }

        public NodeList<T> Children { get; set; }

        public List<BTreeEntry<T>> Entries { get; set; }

        public bool IsLeaf
        {
            get { return this.Children.Count(item => item != null) == 0; }
        }

        public bool HasReachedMaxEntries
        {
            get { return this.Entries.Count(item => item != null) >= (2*degree) - 1; }
        }

        public bool HasReachedMinEntries
        {
            get
            {
                return this.Entries.Count(item => item != null) <= this.degree - 1;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            Entries.Where(item => item != null).ToList().ForEach(item => result.Append(string.Format("{0} ", item.ToString())));
            return result.ToString();
        }
    }
}
