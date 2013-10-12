using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    /// <summary>
    /// 2-3 Tree node.
    /// </summary>
    /// <typeparam name="T">Type stored in the tree.</typeparam>
    interface ITwoThree<T>
    {
        /// <summary>
        /// Yields all the values stored in this subtree.
        /// </summary>
        /// <returns>Collection of values.</returns>
        [Pure]
        IEnumerable<T> GetValues();

        /// <summary>
        /// Finds node matching given value (i. e. value that is equal according to given comparer).
        /// </summary>
        /// <param name="item">Value, that is searched.</param>
        /// <param name="comparer">Comperer used for comparing values.</param>
        /// <param name="value">Found value. Note, that it can be diffrent value than the one searched.</param>
        /// <returns>True, if value was found.</returns>
        [Pure]
        bool TryFind(T item, IComparer<T> comparer, out T value);

        /// <summary>
        /// Gets the minimal value of the subtree.
        /// </summary>
        /// <returns>Smallest value.</returns>
        [Pure]
        T Min();

        /// <summary>
        /// Inserts given value into the tree.
        /// </summary>
        /// <param name="item">Inserted value.</param>
        /// <param name="comparer">Comparer used for comparing values.</param>
        /// <param name="splitLeft">Left node of splitting operation result.</param>
        /// <param name="splitRight">Right node of splitting operation result.</param>
        /// <param name="splitValue">Value propagated from splitting operation.</param>
        /// <returns>Propagated node; same node if item was in this subtree;
        /// or null, if child node split (use out parameters in this case).</returns>
        [Pure]
        ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> splitLeft, out ITwoThree<T> splitRight, out T splitValue);

        /// <summary>
        /// Updates given item in the tree.
        /// </summary>
        /// <param name="item">Updated item. Note, that it can be different
        /// from found value (but equal according to comparer).</param>
        /// <param name="comparer">Comparer used for comparing values.</param>
        /// <returns>Propagated node, if value was updated; same node, if items
        /// were the same; or null, if item was not found.</returns>
        [Pure]
        ITwoThree<T> Update(T item, IComparer<T> comparer);

        /// <summary>
        /// Removes the given item from the tree.
        /// </summary>
        /// <param name="item">Item equivalent to removed item.</param>
        /// <param name="comparer">Comparer used to compare values.</param>
        /// <param name="removed">True, if returned subtree depth was lessened 
        /// due to removing (and thus redistributing or merging is needed).</param>
        /// <returns>Subtree without given item; same tree, if it wasn't found; 
        /// or empty node, if whole tree was deleted.</returns>
        [Pure]
        ITwoThree<T> Remove(T item, IComparer<T> comparer, out bool removed);

        /// <summary>
        /// Diagnostic method checking if 2-3 Tree is balanced.
        /// </summary>
        /// <param name="depth">Depth of the tree.</param>
        /// <returns>True, if tree is balanced.</returns>
        [Pure]
        bool IsBalanced(out int depth);
    }
}
