using System.Collections.Generic;

namespace ImmutableCollections
{
    /// <summary>
    /// Represents an immutable collection of elements.
    /// </summary>
    /// <remarks>
    /// A lot of this interface implementations will have methods returning object of their types.
    /// The general rule is, that if action could not affect collection in any way (e. g. add
    /// elemenent that is already on the set), it will return reference to a current collection.
    /// </remarks>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public interface IImmutableCollection<T> : IEnumerable<T>
    {
        /// <summary>
        /// Add element to a new collection.
        /// </summary>
        /// <param name="item">The object to add.</param>
        /// <returns>New collection.</returns>
        IImmutableCollection<T> Add(T item);

        /// <summary>
        /// Removes the first occurrence of specified element from a new collection.
        /// </summary>
        /// <param name="item">The object to remove.</param>
        /// <returns>New collection.</returns>
        IImmutableCollection<T> Remove(T item);

        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines whether the collection contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the colection.</param>
        /// <returns>True, if item is found in the collection.</returns>
        bool Contains(T item);
    }
}
