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
    }
}
