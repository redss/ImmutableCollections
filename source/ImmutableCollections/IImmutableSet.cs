using System.Diagnostics.Contracts;

namespace ImmutableCollections
{
    /// <summary>
    /// Represents immutable set.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public interface IImmutableSet<T> : IImmutableCollection<T>
    {
        /// <summary>
        /// Adds an item to a new set.
        /// </summary>
        /// <param name="item">Item to be added to a new set.</param>
        /// <returns>This, or new set.</returns>
        [Pure]
        new IImmutableSet<T> Add(T item);

        /// <summary>
        /// Removes an item from new set.
        /// </summary>
        /// <param name="item">Item to be removed.</param>
        /// <returns>This, or new set.</returns>
        [Pure]
        new IImmutableSet<T> Remove(T item);
    }
}
