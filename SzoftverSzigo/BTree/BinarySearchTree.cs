using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzoftverSzigo.BTree
{
    public class BinarySearchTree<T> : BinaryTree<T>
        where T : IComparable
    {
        public void AddNode(T value)
        {
            if (this.Root == null)
            {
                this.Root = new BinaryTreeNode<T>(value);
                return;
            }
            AddNode(this.Root, value);
        }

        public static void AddNode(BinaryTreeNode<T> toNode, T value)
        {
            AddNode(toNode, new BinaryTreeNode<T>(value));
        }

        public static void AddNode(BinaryTreeNode<T> toNode, BinaryTreeNode<T> newNode)
        {
            if (toNode.Value.CompareTo(newNode.Value) < 0)
            {
                if (toNode.Right == null)
                {
                    toNode.Right = newNode;
                }
                else
                {
                    AddNode(toNode.Right, newNode);
                }
            }
            else
            {
                if (toNode.Value.CompareTo(newNode.Value) > 0)
                {
                    if (toNode.Left == null)
                    {
                        toNode.Left = newNode;
                    }
                    else
                    {
                        AddNode(toNode.Left, newNode);
                    }
                }
            }
        }

        public BinaryTreeNode<T> SearchNode(T value)
        {
            if (Root == null)
            {
                return null;
            }
            return SearchNode(this.Root, value);
        }

        public static BinaryTreeNode<T> SearchNode(BinaryTreeNode<T> node, T value)
        {
            if (node.Value.Equals(value))
            {
                return node;
            }
            else
            {
                if (node.Value.CompareTo(value) > 0)
                {
                    if (node.Left != null)
                    {
                        return SearchNode(node.Left, value);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (node.Right != null)
                    {
                        return SearchNode(node.Right, value);
                    }
                    else
                    {
                        return null;
                    }
                }
                
            }
        }

        public void RemoveNode(T value)
        {
            if (Root == null)
            {
                return; 
            }
            RemoveNode(this.Root, value);
        }

        public static void RemoveNode(BinaryTreeNode<T> fromNode, T value)
        {
            if (fromNode.Value.CompareTo(value) > 0)
            {
                if (fromNode.Left != null)
                {
                    if (fromNode.Left.Value.Equals(value) && fromNode.Left.Left == null && fromNode.Left.Right == null)
                    {
                        fromNode.Left = null;
                    }
                    else
                    {
                        RemoveNode(fromNode.Left, value);
                    }

                }
            }
            else if (fromNode.Value.CompareTo(value) < 0)
            {
                if (fromNode.Right.Value.Equals(value) && fromNode.Right.Left == null &&
                    fromNode.Right.Right == null)
                {
                    fromNode.Right = null;
                }
                else
                {
                    RemoveNode(fromNode.Right, value);
                }
            }
            else
            {
                if (fromNode.Left == null && fromNode.Right != null)
                {
                    fromNode.Value = fromNode.Right.Value;
                    fromNode.Left = fromNode.Right.Left;
                    fromNode.Right = fromNode.Right.Right;
                }
                else if (fromNode.Right == null && fromNode.Left != null)
                {
                    fromNode.Value = fromNode.Left.Value;
                    fromNode.Right = fromNode.Left.Right;
                    fromNode.Left = fromNode.Left.Left;
                }
                else if (fromNode.Right != null && fromNode.Left != null)
                {
                    RemoveExtra(fromNode, fromNode.Left);
                }
            }
        }


        private static void RemoveExtra(BinaryTreeNode<T> fromNode, BinaryTreeNode<T> fromNodeLeft)
        {
            if (fromNodeLeft.Right != null)
            {
                RemoveExtra(fromNode, fromNodeLeft.Right);
            }
            else
            {
                fromNode.Value = fromNodeLeft.Value;
                fromNode.Left = fromNodeLeft.Left;
            }
        }
    }
}
