using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.RedBlackTreeStructure
{
    /// <summary>
    /// Red Black Tree node.
    /// </summary>
    /// <typeparam name="T">Type of values stored in the tree.</typeparam>
    interface IRedBlack<T>
    {
        /// <summary>
        /// True, if node is black; false, if node is red.
        /// </summary>
        bool IsBlack { get; }

        /// <summary>
        /// Value contained in the node.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Left child.
        /// </summary>
        IRedBlack<T> Left { get; }

        /// <summary>
        /// Right child.
        /// </summary>
        IRedBlack<T> Right { get; }

        /// <summary>
        /// Finds node matching given value (i. e. value that is equal according to given comparer).
        /// </summary>
        /// <param name="searched">Value, that is searched.</param>
        /// <param name="comparer">Comperer used for comparing values.</param>
        /// <param name="value">Found value. Note, that it can be diffrent value than the one searched.</param>
        /// <returns>True, if value was found.</returns>
        bool TryFind(T searched, IComparer<T> comparer, out T value);

        /// <summary>
        /// Updated or inserts node with given value (i. e. value that is equal according to given comparer).
        /// </summary>
        /// <param name="value">Inserted or updated value.</param>
        /// <param name="comparer">Comparer used for comparing values.</param>
        /// <returns>Propagated node.</returns>
        IRedBlack<T> Update(T value, IComparer<T> comparer);

        /// <summary>
        /// Gets all the values stored in this subtree.
        /// </summary>
        /// <returns>Collection of stored values.</returns>
        IEnumerable<T> GetValues();

        /// <summary>
        /// Checks is given tree is valid Red Black Tree.
        /// </summary>
        /// <returns>Number of black nodes from this node to the leafs.</returns>
        int Validate(int blackNodes = 0);
    }
}
