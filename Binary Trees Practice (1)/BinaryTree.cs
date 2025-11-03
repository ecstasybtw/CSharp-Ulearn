using System;

namespace BinaryTrees
{
    public class BinaryTree<T> where T : IComparable
    {
        private class Node
        {
            public T Value;
            public Node Left;
            public Node Right;

            public Node(T value) {
                Value = value;
            }
        }

        private Node root;

        public void Add(T key) {
            if (root == null) {
                root = new Node(key);
                return;
            }
            Insert(root, key);
        }

        private void Insert(Node node, T key) {
            while (true) {
                var cmp = key.CompareTo(node.Value);
                if (cmp < 0) {
                    if (node.Left != null)
                        node = node.Left;
                    else {
                        node.Left = new Node(key);
                        break;
                    }
                }
                else {
                    if (node.Right != null)
                        node = node.Right;
                    else {
                        node.Right = new Node(key);
                        break;
                    }
                }
            }
        }

        public bool Contains(T key) {
            Node current = root;
            while (current != null) {
                var cmp = key.CompareTo(current.Value);
                if (cmp == 0) return true;
                current = cmp < 0 ? current.Left : current.Right;
            }
            return false;
        }
    }
}