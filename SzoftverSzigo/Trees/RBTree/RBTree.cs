using System;
using System.ComponentModel.Design.Serialization;
using SzoftverSzigo.Trees.BinaryTree;

namespace SzoftverSzigo.Trees.RBTree
{
    //TODO Not finished
    public class RBTree<T> : BinaryTree<T>
        where T : IComparable
    {

        public RBTree()
        {
            Root = RBTreeNode<T>.SentinelNode;
        }

        //private void AddNode(T value)
        //{
        //    if (this.Root == null)
        //    {
        //        this.Root = new RBTreeNode<T>(value);
        //        return;
        //    }
        //    AddNode(this.Root as RBTreeNode<T>, value);
        //}

        public void AddNode( T value)
        {
            var x = new RBTreeNode<T>()
            {
                Value = value,
                Left = RBTreeNode<T>.SentinelNode,
                Right = RBTreeNode<T>.SentinelNode,
                Parent = null
            };

            AddNode(Root as RBTreeNode<T>, x);
            RestoreRedBlackProperty(x);
        }

        private void RestoreRedBlackProperty(RBTreeNode<T> x)
        {
            RBTreeNode<T> y;

            while (x != this.Root && x.Parent.Color == Color.RED)
            {
                if (x.Parent == x.Parent.Parent.Left)
                {
                    y = x.Parent.Parent.Right as RBTreeNode<T>;
                    if (y != null && y.Color == Color.RED)
                    {
                        x.Parent.Color = Color.BLACK;
                        y.Color = Color.BLACK;

                        x.Parent.Parent.Color = Color.RED;
                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x.Equals(x.Parent.Right))
                        {
                            x = x.Parent;
                            RotateLeft(x);
                        }
                        x.Parent.Color = Color.BLACK;
                        x.Parent.Parent.Color = Color.RED;
                        RotateRight(x.Parent.Parent);
                    }
                }
                else
                {
                    y = x.Parent.Parent.Left as RBTreeNode<T>;
                    if (y != null && y.Color == Color.RED)
                    {
                        x.Parent.Color = Color.BLACK;
                        y.Color = Color.BLACK;
                        x.Parent.Parent.Color = Color.RED;
                        x = x.Parent.Parent;
                    }
                    else
                    {
                        if (x.Equals(x.Parent.Left))
                        {
                            x = x.Parent;
                            RotateRight(x);
                        }
                        x.Parent.Color = Color.BLACK;
                        x.Parent.Parent.Color = Color.RED;
                        RotateLeft(x.Parent.Parent);
                    }
                }

            }
            (Root as RBTreeNode<T>).Color = Color.BLACK;
        }

        private void RotateRight(RBTreeNode<T> x)
        {
            RBTreeNode<T> y = x.Left as RBTreeNode<T>;			// get x's Left node, this becomes y

            // set x's Right link
            x.Left = y.Right;					// y's Right child becomes x's Left child

            // modify parents
            if (y.Right != RBTreeNode<T>.SentinelNode)
                (y.Right as RBTreeNode<T>).Parent = x;				// sets y's Right Parent to x

            if (!y.Equals(RBTreeNode<T>.SentinelNode))
                y.Parent = x.Parent;			// set y's Parent to x's Parent

            if (x.Parent != null)				// null=rbTree, could also have used rbTree
            {	// determine which side of it's Parent x was on
                if (x == x.Parent.Right)
                    x.Parent.Right = y;			// set Right Parent to y
                else
                    x.Parent.Left = y;			// set Left Parent to y
            }
            else
                Root = y;						// at rbTree, set it to y

            // link x and y 
            y.Right = x;						// put x on y's Right
            if (!x.Equals(RBTreeNode<T>.SentinelNode))				// set y as x's Parent
                x.Parent = y;		
        }

        private void RotateLeft(RBTreeNode<T> x)
        {
            RBTreeNode<T> y = x.Right as RBTreeNode<T>;			// get x's Right node, this becomes y

            // set x's Right link
            x.Right = y.Left;					// y's Left child's becomes x's Right child

            // modify parents
            if (!RBTreeNode<T>.SentinelNode.Equals(y.Left as RBTreeNode<T>))
            {
                (y.Left as RBTreeNode<T>).Parent = x;
            } // sets y's Left Parent to x

            if (!y.Equals(RBTreeNode<T>.SentinelNode))
                y.Parent = x.Parent;			// set y's Parent to x's Parent

            if (x.Parent != null)
            {	// determine which side of it's Parent x was on
                if (x == x.Parent.Left)
                    x.Parent.Left = y;			// set Left Parent to y
                else
                    x.Parent.Right = y;			// set Right Parent to y
            }
            else
                Root = y;						// at rbTree, set it to y

            // link x and y 
            y.Left = x;							// put x on y's Left 
            if (!x.Equals(RBTreeNode<T>.SentinelNode))						// set y as x's Parent
                x.Parent = y;		
        }

        private void AddNode(RBTreeNode<T> toNode, RBTreeNode<T> newNode)
        {
            RBTreeNode<T> tmp = this.Root as RBTreeNode<T>;

            while (!tmp.Equals(RBTreeNode<T>.SentinelNode))
            {
                newNode.Parent = tmp;
                int result = newNode.Value.CompareTo(tmp.Value);
                if (result == 0)
                {
                    throw new Exception();
                }
                if (result > 0)
                {
                    tmp = tmp.Right as RBTreeNode<T>;
                }
                else
                {
                    tmp = tmp.Left as RBTreeNode<T>;
                }

            }

            if (newNode.Parent != null)
            {
                int result = newNode.Value.CompareTo(newNode.Parent.Value);
                if (result > 0)
                {
                    newNode.Parent.Right = newNode;

                }
                else
                {
                    newNode.Parent.Left = newNode;
                }
            }
            else
            {
                this.Root = newNode;
            }
        }
    }
}
