namespace _02.RangeInTree
{
    using System;
    using System.Collections.Generic;
    using AvlTreeLab;

    public static class AvlTreeExtensions
    {
        public static List<T> GetRange<T>(
            this AvlTree<T> avlTree,
            T min,
            T max) where T : IComparable<T>
        {
            var result = new List<T>();
            InOrderBfs(avlTree.Root, result, min, max);

            return result;
        }

        private static void InOrderBfs<T>(
            Node<T> node,
            ICollection<T> range,
            T min,
            T max) where T : IComparable<T>
        {
            if (node.LeftChild != null && node.Value.CompareTo(min) > 0)
            {
                InOrderBfs<T>(node.LeftChild, range, min, max);
            }

            if (node.Value.CompareTo(min) >= 0 &&
                node.Value.CompareTo(max) <= 0)
            {
                range.Add(node.Value);
            }

            if (node.RightChild != null && node.Value.CompareTo(max) < 0)
            {
                InOrderBfs<T>(node.RightChild, range, min, max);
            }
        }
    }
}