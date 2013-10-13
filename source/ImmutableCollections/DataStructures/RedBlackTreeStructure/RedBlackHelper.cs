using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.RedBlackTreeStructure
{
    /// <summary>
    /// Helper methods for Red Black Tree operations.
    /// </summary>
    static class RedBlackHelper
    {
        public static IRedBlack<T> Insert<T>(IRedBlack<T> root, T item, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            return root.Insert(item, comparer).MakeRoot();
        }

        public static IRedBlack<T> Update<T>(IRedBlack<T> root, T item, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            return root.Update(item, comparer).MakeRoot();
        }

        /// <summary>
        /// Balances the given subtree.
        /// </summary>
        /// <typeparam name="T">Type of values stored in the tree.</typeparam>
        /// <param name="isBlack">Is checked node black?</param>
        /// <param name="value">Value stored in the node.</param>
        /// <param name="left">New left node.</param>
        /// <param name="right">New right node.</param>
        /// <returns>Propagated node.</returns>
        public static IRedBlack<T> Balance<T>(bool isBlack, T value, IRedBlack<T> left, IRedBlack<T> right)
        {
            if (isBlack && left.IsRedNode() && left.Left.IsRedNode())
            {
                var l = Black(left.Left.Value, left.Left.Left, left.Left.Right);
                var r = Black(value,           left.Right,     right);

                return Red(left.Value, l, r);
            }

            if (isBlack && left.IsRedNode() && left.Right.IsRedNode())
            {
                var l = Black(left.Value, left.Left,        left.Right.Left);
                var r = Black(value,      left.Right.Right, right);

                return Red(left.Right.Value, l, r);
            }

            if (isBlack && right.IsRedNode() && right.Left.IsRedNode())
            {
                var l = Black(value,       left,             right.Left.Left);
                var r = Black(right.Value, right.Left.Right, right.Right);

                return Red(right.Left.Value, l, r);
            }

            if (isBlack && right.IsRedNode() && right.Right.IsRedNode())
            {
                var l = Black(value,             left,             right.Left);
                var r = Black(right.Right.Value, right.Right.Left, right.Right.Right);

                return Red(right.Value, l, r);
            }

            // Tree is already balanced.

            return new RedBlackNode<T>(isBlack, value, left, right);
        }

        // Private methods

        /// <summary>
        /// Creates new black node.
        /// </summary>
        private static RedBlackNode<T> Black<T>(T value, IRedBlack<T> left, IRedBlack<T> right)
        {
            return new RedBlackNode<T>(true, value, left, right);
        }

        /// <summary>
        /// Creates new red node.
        /// </summary>
        private static RedBlackNode<T> Red<T>(T value, IRedBlack<T> left, IRedBlack<T> right)
        {
            return new RedBlackNode<T>(false, value, left, right);
        }
    }
}
