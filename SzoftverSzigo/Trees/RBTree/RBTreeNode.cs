using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzoftverSzigo.Trees.BinaryTree;
using SzoftverSzigo.Trees.Default;

namespace SzoftverSzigo.Trees.RBTree
{
    public enum Color
    {
        RED,
        BLACK
    }

    

    public class RBTreeNode<T> : BinaryTreeNode<T>
    {
        private static RBTreeNode<T> sentinelNode;

        public static RBTreeNode<T> SentinelNode
        {
            get
            {
                if (sentinelNode == null)
                {
                    sentinelNode = new RBTreeNode<T>()
                    {
                        Color = Color.BLACK,
                        Parent = null,
                        Left = null,
                        Right = null,
                        Value = default(T)
                    };
                }
                return sentinelNode;
            }
        } 

        public RBTreeNode<T> Parent { get; set; }

        public Color Color { get; set; }

        public RBTreeNode() : base()
        {
            this.Color = Color.RED;
        }

        public RBTreeNode(T data) : base(data)
        {
            this.Color = Color.RED;
        }

        public RBTreeNode(T data, RBTreeNode<T> left, RBTreeNode<T> right)
        {
            base.Value = data;
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;

            base.Neighbors = children;
            this.Color = Color.RED;
        }

        public RBTreeNode(T data, RBTreeNode<T> left, RBTreeNode<T> right, RBTreeNode<T> parent): this(data, left, right)
        {
            this.Parent = parent;
            this.Color = Color.RED;
        }

        public override string ToString()
        {
            if (this.Equals(SentinelNode))
            {
                return "";
            }
            return string.Format("{0}: {1}", base.ToString() , Enum.GetName(typeof(Color), this.Color));
        }
    }
}
