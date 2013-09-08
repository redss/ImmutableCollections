using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.ImmutableVectorStructure
{
    public interface IVectorNode<T>
    {
        /// <summary>
        /// Level of the current node.
        /// </summary>
        int Level { get; }

        /// <summary>
        /// Get all bottom-level values for this subtree.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetValues();

        /// <summary>
        /// Apeends an element at the end of the structure.
        /// </summary>
        /// <param name="elem">Appended element.</param>
        /// <param name="count">Number of elements in the whole structure.</param>
        /// <returns>Propagated node.</returns>
        IVectorNode<T> Append(T elem, int count);

        /// <summary>
        /// Updates the element at given index.
        /// </summary>
        /// <param name="elem">Element to replace one in the structure.</param>
        /// <param name="index">Index of element to be updated.</param>
        /// <returns>Propagated node.</returns>
        IVectorNode<T> UpdateAt(T elem, int index);

        /// <summary>
        /// Gets the n-th element.
        /// </summary>
        /// <param name="index">Index of searched element.</param>
        /// <returns>Bottom-level trie value.</returns>
        T Nth(int index);
    }
}
