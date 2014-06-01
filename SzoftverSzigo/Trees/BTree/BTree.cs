using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;


namespace SzoftverSzigo.Trees.BTree
{
    ///The BTree is based on Rodrigo De Castro's (https://github.com/rdcastro) impelmentation.
    public class BTree<T> where T: IComparable<T>
    {
        public BTree(int degree)
        {
            if (degree < 2)
            {
                throw new ArgumentException("Degree must be at least 2!", "degree");
            }

            this.Root = new BTreeNode<T>(degree);
            this.Degree = degree;
            this.Height = 1;
        }

        public BTreeNode<T> Root { get; private set; }

        public int Degree { get; private set; }

        public int Height { get; private set; }

        #region Search
        public BTreeEntry<T> Search(T key)
        {
            return SearchInternal(this.Root, key);
        }

        private static BTreeEntry<T> SearchInternal(BTreeNode<T> node, T key)
        {
            int i = node.Entries.TakeWhile(entry => key.CompareTo(entry.Value) > 0).Count();

            if (i < node.Entries.Count && node.Entries[i].Value.CompareTo(key) == 0)
            {
                return node.Entries[i];
            }

            return node.IsLeaf ? null : SearchInternal(node.Children[i] as BTreeNode<T>, key);
        } 
        #endregion

        #region Insert
        public void Insert(T newKey)
        {
            // there is space in the root node
            if (!this.Root.HasReachedMaxEntries)
            {
                InsertNonFull(this.Root, newKey, this.Degree);
                return;
            }

            // need to create new node and have it split
            BTreeNode<T> oldRoot = this.Root;
            this.Root = new BTreeNode<T>(this.Degree);
            this.Root.Children.Add(oldRoot);
            SplitChild(this.Root, 0, oldRoot, this.Degree);
            InsertNonFull(this.Root, newKey, this.Degree);

            this.Height++;
        }

        private static void InsertNonFull(BTreeNode<T> node, T newKey, int degree)
        {
            int positionToInsert = node.Entries.TakeWhile(entry => newKey.CompareTo(entry.Value) >= 0).Count();

            // leaf node
            if (node.IsLeaf)
            {
                node.Entries.Insert(positionToInsert, new BTreeEntry<T>() {Value = newKey});
                return;
            }

            // non-leaf
            BTreeNode<T> child = node.Children[positionToInsert] as BTreeNode<T>;
            if (child.HasReachedMaxEntries)
            {
                SplitChild(node, positionToInsert, child, degree);
                if (newKey.CompareTo(node.Entries[positionToInsert].Value) > 0)
                {
                    positionToInsert++;
                }
            }

            InsertNonFull(node.Children[positionToInsert] as BTreeNode<T>, newKey, degree);
        }

        private static void SplitChild(BTreeNode<T> parentNode, int nodeToBeSplitIndex, BTreeNode<T> nodeToBeSplit, int degree)
        {
            var newNode = new BTreeNode<T>(degree);

            parentNode.Entries.Insert(nodeToBeSplitIndex, nodeToBeSplit.Entries[degree - 1]);
            parentNode.Children.Insert(nodeToBeSplitIndex + 1, newNode);

            newNode.Entries.AddRange(nodeToBeSplit.Entries.GetRange(degree, degree - 1));

            // remove also Entries[this.Degree - 1], which is the one to move up to the parent
            nodeToBeSplit.Entries.RemoveRange(degree - 1, degree);

            if (!nodeToBeSplit.IsLeaf)
            {
                newNode.Children.AddRange(nodeToBeSplit.Children.GetRange(degree, degree));
                nodeToBeSplit.Children.RemoveRange(degree, degree);
            }
        }
        #endregion

        #region Delete
        public void Delete(T keyToDelete)
        {
            this.DeleteInternal(this.Root, keyToDelete);

            // if root's last entry was moved to a child node, remove it
            if (this.Root.Entries.Count == 0 && !this.Root.IsLeaf)
            {
                this.Root = this.Root.Children.Single() as BTreeNode<T>;
                this.Height--;
            }
        }

        /// <summary>
        /// Internal method to delete keys from the BTree
        /// </summary>
        /// <param name="node">Node to use to start search for the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        private void DeleteInternal(BTreeNode<T> node, T keyToDelete)
        {
            int i = node.Entries.TakeWhile(entry => keyToDelete.CompareTo(entry.Value) > 0).Count();

            // found key in node, so delete if from it
            if (i < node.Entries.Count && node.Entries[i].Value.CompareTo(keyToDelete) == 0)
            {
                this.DeleteKeyFromNode(node, keyToDelete, i);
                return;
            }

            // delete key from subtree
            if (!node.IsLeaf)
            {
                this.DeleteKeyFromSubtree(node, keyToDelete, i);
            }
        }

        /// <summary>
        /// Helper method that deletes a key from a subtree.
        /// </summary>
        /// <param name="parentNode">Parent node used to start search for the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        /// <param name="subtreeIndexInNode">Index of subtree node in the parent node.</param>
        private void DeleteKeyFromSubtree(BTreeNode<T> parentNode, T keyToDelete, int subtreeIndexInNode)
        {
            BTreeNode<T> childNode = parentNode.Children[subtreeIndexInNode] as BTreeNode<T>;

            // node has reached min # of entries, and removing any from it will break the btree property,
            // so this block makes sure that the "child" has at least "degree" # of nodes by moving an
            // entry from a sibling node or merging nodes
            if (childNode.HasReachedMinEntries)
            {
                int leftIndex = subtreeIndexInNode - 1;
                BTreeNode<T> leftSibling = subtreeIndexInNode > 0 ? parentNode.Children[leftIndex] as BTreeNode<T> : null;

                int rightIndex = subtreeIndexInNode + 1;
                BTreeNode<T> rightSibling = subtreeIndexInNode < parentNode.Children.Count - 1
                                                ? parentNode.Children[rightIndex] as BTreeNode<T>
                                                : null;

                if (leftSibling != null && leftSibling.Entries.Count > this.Degree - 1)
                {
                    // left sibling has a node to spare, so this moves one node from left sibling
                    // into parent's node and one node from parent into this current node ("child")
                    childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
                    parentNode.Entries[subtreeIndexInNode] = leftSibling.Entries.Last();
                    leftSibling.Entries.RemoveAt(leftSibling.Entries.Count - 1);

                    if (!leftSibling.IsLeaf)
                    {
                        childNode.Children.Insert(0, leftSibling.Children.Last());
                        leftSibling.Children.RemoveAt(leftSibling.Children.Count - 1);
                    }
                }
                else if (rightSibling != null && rightSibling.Entries.Count > this.Degree - 1)
                {
                    // right sibling has a node to spare, so this moves one node from right sibling
                    // into parent's node and one node from parent into this current node ("child")
                    childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
                    parentNode.Entries[subtreeIndexInNode] = rightSibling.Entries.First();
                    rightSibling.Entries.RemoveAt(0);

                    if (!rightSibling.IsLeaf)
                    {
                        childNode.Children.Add(rightSibling.Children.First());
                        rightSibling.Children.RemoveAt(0);
                    }
                }
                else
                {
                    // this block merges either left or right sibling into the current node "child"
                    if (leftSibling != null)
                    {
                        childNode.Entries.Insert(0, parentNode.Entries[subtreeIndexInNode]);
                        var oldEntries = childNode.Entries;
                        childNode.Entries = leftSibling.Entries;
                        childNode.Entries.AddRange(oldEntries);
                        if (!leftSibling.IsLeaf)
                        {
                            var oldChildren = childNode.Children;
                            childNode.Children = leftSibling.Children;
                            childNode.Children.AddRange(oldChildren);
                        }

                        parentNode.Children.RemoveAt(leftIndex);
                        parentNode.Entries.RemoveAt(subtreeIndexInNode);
                    }
                    else
                    {
                        Debug.Assert(rightSibling != null, "Node should have at least one sibling");
                        childNode.Entries.Add(parentNode.Entries[subtreeIndexInNode]);
                        childNode.Entries.AddRange(rightSibling.Entries);
                        if (!rightSibling.IsLeaf)
                        {
                            childNode.Children.AddRange(rightSibling.Children);
                        }

                        parentNode.Children.RemoveAt(rightIndex);
                        parentNode.Entries.RemoveAt(subtreeIndexInNode);
                    }
                }
            }

            // at this point, we know that "child" has at least "degree" nodes, so we can
            // move on - this guarantees that if any node needs to be removed from it to
            // guarantee BTree's property, we will be fine with that
            this.DeleteInternal(childNode, keyToDelete);
        }

        /// <summary>
        /// Helper method that deletes key from a node that contains it, be this
        /// node a leaf node or an internal node.
        /// </summary>
        /// <param name="node">Node that contains the key.</param>
        /// <param name="keyToDelete">Key to be deleted.</param>
        /// <param name="keyIndexInNode">Index of key within the node.</param>
        private void DeleteKeyFromNode(BTreeNode<T> node, T keyToDelete, int keyIndexInNode)
        {
            // if leaf, just remove it from the list of entries (we're guaranteed to have
            // at least "degree" # of entries, to BTree property is maintained
            if (node.IsLeaf)
            {
                node.Entries.RemoveAt(keyIndexInNode);
                return;
            }

            BTreeNode<T> predecessorChild = node.Children[keyIndexInNode] as BTreeNode<T>;
            if (predecessorChild.Entries.Count >= this.Degree)
            {
                BTreeEntry<T> predecessor = this.DeletePredecessor(predecessorChild);
                node.Entries[keyIndexInNode] = predecessor;
            }
            else
            {
                BTreeNode<T> successorChild = node.Children[keyIndexInNode + 1] as BTreeNode<T>;
                if (successorChild.Entries.Count >= this.Degree)
                {
                    BTreeEntry<T> successor = this.DeleteSuccessor(predecessorChild);
                    node.Entries[keyIndexInNode] = successor;
                }
                else
                {
                    predecessorChild.Entries.Add(node.Entries[keyIndexInNode]);
                    predecessorChild.Entries.AddRange(successorChild.Entries);
                    predecessorChild.Children.AddRange(successorChild.Children);

                    node.Entries.RemoveAt(keyIndexInNode);
                    node.Children.RemoveAt(keyIndexInNode + 1);

                    this.DeleteInternal(predecessorChild, keyToDelete);
                }
            }
        }

        /// <summary>
        /// Helper method that deletes a predecessor key (i.e. rightmost key) for a given node.
        /// </summary>
        /// <param name="node">Node for which the predecessor will be deleted.</param>
        /// <returns>Predecessor entry that got deleted.</returns>
        private BTreeEntry<T> DeletePredecessor(BTreeNode<T> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[node.Entries.Count - 1];
                node.Entries.RemoveAt(node.Entries.Count - 1);
                return result;
            }

            return this.DeletePredecessor(node.Children.Last() as BTreeNode<T>);
        }

        /// <summary>
        /// Helper method that deletes a successor key (i.e. leftmost key) for a given node.
        /// </summary>
        /// <param name="node">Node for which the successor will be deleted.</param>
        /// <returns>Successor entry that got deleted.</returns>
        private BTreeEntry<T> DeleteSuccessor(BTreeNode<T> node)
        {
            if (node.IsLeaf)
            {
                var result = node.Entries[0];
                node.Entries.RemoveAt(0);
                return result;
            }

            return this.DeletePredecessor(node.Children.First() as BTreeNode<T>);
        }
        #endregion Delete

        public override string ToString()
        {
            if (this.Root == null)
            {
                return base.ToString();
            }
            var builder = new StringBuilder();
            TraverseNodes(this.Root, (node, i) => builder.Append(string.Format("{0}|->{1}\n", new String(' ', i * 3), node.ToString())), 0);
            return builder.ToString();
        }

        public static void TraverseNodes(BTreeNode<T> node, Action<BTreeNode<T>, int> func, int depth = 0)
        {
            func(node, depth);
            node.Children.Where(item => item != null).ToList().ForEach(item => TraverseNodes(item as BTreeNode<T>, func, depth + 1));
        }
    }
}
