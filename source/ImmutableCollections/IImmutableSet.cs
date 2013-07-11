using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        new IImmutableSet<T> Add(T item);

        /// <summary>
        /// Removes an item from new set.
        /// </summary>
        /// <param name="item">Item to be removed.</param>
        /// <returns>This, or new set.</returns>
        new IImmutableSet<T> Remove(T item);

        /// <summary>
        /// Removes all the elements from a new set contained in the given collection.
        /// </summary>
        /// <param name="other">Collection of items to remove form the set.</param>
        /// <returns>This, or new set.</returns>
        IImmutableSet<T> ExceptWith(IEnumerable<T> other);

        /// <summary>
        /// Creates new set, so it only contains elements that are also in given collection.
        /// </summary>
        /// <param name="other">The collection to compare to current set.</param>
        /// <returns>This, or new set.</returns>
        IImmutableSet<T> IntersectWith(IEnumerable<T> other);

        /// <summary>
        /// Creates new set so that it contains only elements that are present either in 
        /// the current set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to current set.</param>
        /// <returns>This, or new set.</returns>
        IImmutableSet<T> SymmetricExceptWith(IEnumerable<T> other);

        /// <summary>
        /// Creates new set, so it contains all elements from given collection.
        /// </summary>
        /// <param name="other">The collection to compare to current set.</param>
        /// <returns>This, or new set.</returns>
        IImmutableSet<T> UnionWith(IEnumerable<T> other);

        bool IsSubsetOf(IEnumerable<T> other);
        
        bool IsProperSubsetOf(IEnumerable<T> other);

        bool IsSupersetOf(IEnumerable<T> other);

        bool IsProperSupersetOf(IEnumerable<T> other);

        bool Overlaps(IEnumerable<T> other);

        bool SetEquals(IEnumerable<T> other);
    }
}
