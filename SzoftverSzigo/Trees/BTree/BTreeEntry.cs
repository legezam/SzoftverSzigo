using System;

namespace SzoftverSzigo.Trees.BTree
{
    public class BTreeEntry<T> : IEquatable<BTreeEntry<T>>
    {
        public T Value { get; set; }


        public bool Equals(BTreeEntry<T> other)
        {
            return this.Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
