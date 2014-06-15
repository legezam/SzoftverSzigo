namespace SzoftverSzigo.Trees.Default
{
    public class Node<T>
    {
        // Private member-variables
        private T data;
        private NodeList<T> neighbors = null;

        public Node()
        {
        }

        public Node(T data) : this(data, null)
        {
        }

        public Node(T data, NodeList<T> neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
        }

        public T Value
        {
            get { return data; }
            set { data = value; }
        }

        protected NodeList<T> Neighbors
        {
            get { return neighbors; }
            set { neighbors = value; }
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Node<T>)
            {
                return this.Value.Equals((obj as Node<T>).Value);
            }

            return base.Equals(obj);
        }
    }
}

