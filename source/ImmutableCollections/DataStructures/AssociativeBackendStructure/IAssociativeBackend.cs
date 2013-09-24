using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.AssociativeBackendStructure
{
    /// <summary>
    /// Backend for hashcode/key based data structures, so they can be used in different contexts.
    /// It should be used in case of storing values with the same hashcode/key.
    /// </summary>
    /// <typeparam name="T">Type stored in backend, usually same type as one stored in container.</typeparam>
    interface IAssociativeBackend<T>
    {
        /// <summary>
        /// Yields all the values in the backend.
        /// </summary>
        /// <returns>Values enumerator.</returns>
        IEnumerable<T> GetValues();

        /// <summary>
        /// Gets the number of items in the backend.
        /// </summary>
        int Count();

        /// <summary>
        /// Inserts new item into backend.
        /// </summary>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>Backend containing added item.</returns>
        IAssociativeBackend<T> Insert(T item);

        /// <summary>
        /// Removes given item from backend.
        /// </summary>
        /// <param name="item">Item to be removed.</param>
        /// <returns>Backend without specified item.</returns>
        IAssociativeBackend<T> Remove(T item); 

        /// <summary>
        /// Does backend contains given element?
        /// </summary>
        /// <param name="item">Searched item.</param>
        /// <returns>True is item is present in the backend.</returns>
        bool Contains(T item);

        /// <summary>
        /// Does backend contain exactly one element?
        /// </summary>
        /// <returns>True, if backend contains exactly one element.</returns>
        bool IsSingle();
    }
}
