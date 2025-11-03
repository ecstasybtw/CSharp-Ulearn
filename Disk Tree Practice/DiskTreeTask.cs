using System;
using System.Collections.Generic;
using System.Linq;

namespace DiskTree
{
    public class DiskTreeTask {
        private class TreeNode {
            public Dictionary<string, TreeNode> Children = new();
        }

        public static List<string> Solve(List<string> input) {
            var root = new TreeNode();
            foreach (var fullPath in input) {
                AddPath(root, fullPath.Split('\\'));
            }

            var output = new List<string>();
            TraverseTree(root, output, "");
            return output;
        }

        private static void AddPath(TreeNode root, string[] segments) {
            var current = root;
            foreach (var segment in segments) {
                if (!current.Children.ContainsKey(segment))
                    current.Children[segment] = new TreeNode();
                current = current.Children[segment];
            }
        }

        private static void TraverseTree(TreeNode node, List<string> result, string indent) {
            var sortedKeys = node.Children.Keys.ToList();
            sortedKeys.Sort(StringComparer.Ordinal);

            foreach (var key in sortedKeys) {
                result.Add(indent + key);
                TraverseTree(node.Children[key], result, indent + " ");
            }
        }
    }
}
