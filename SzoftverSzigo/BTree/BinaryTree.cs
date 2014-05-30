using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzoftverSzigo.BTree
{
    public class BinaryTree<T>
    {
        private BinaryTreeNode<T> root;

        public BinaryTree()
        {
            root = null;
        }

        public virtual void Clear()
        {
            root = null;
        }

        public BinaryTreeNode<T> Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }

        public static void PreOrderTraverseNode<T>(BinaryTreeNode<T> root, Action<BinaryTreeNode<T>> func)
        {
            if (root == null)
            {
                return;
            }
            func(root);
            PreOrderTraverseNode(root.Left, func);
            PreOrderTraverseNode(root.Right, func);
        }

        public static void InOrderTraverseNode<T>(BinaryTreeNode<T> root, Action<BinaryTreeNode<T>> func)
        {
            if (root == null)
            {
                return;
            }
            
            InOrderTraverseNode(root.Left, func);
            func(root);
            InOrderTraverseNode(root.Right, func);
        }

        public static void PostOrderTraverseNode<T>(BinaryTreeNode<T> root, Action<BinaryTreeNode<T>> func)
        {
            if (root == null)
            {
                return;
            }
            
            PostOrderTraverseNode(root.Left, func);
            PostOrderTraverseNode(root.Right, func);
            func(root);
        }

        public void TraverseNode(TraverseMode traverseMode,  Action<BinaryTreeNode<T>> func)
        {
            if(root == null) return;
            switch (traverseMode)
            {
                case TraverseMode.PreOrder:
                    PreOrderTraverseNode(this.Root, func);
                    break;
                case TraverseMode.InOrder:
                    InOrderTraverseNode(this.Root, func);
                    break;
                case TraverseMode.PostOrder:
                    PostOrderTraverseNode(this.Root, func);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("traverseMode");
            }
        }
    }

    public enum TraverseMode
    {
        PreOrder,
        InOrder,
        PostOrder
    }
}
