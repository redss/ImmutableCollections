namespace ImmutableCollections
{
    /// <summary>
    /// Represents an immutable collection of objects that can be individually accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public interface IImmutableList<T> : IImmutableCollection<T>
    {
        /// <summary>
        /// Adds item to at the tail of a new list.
        /// </summary>
        /// <param name="item">The object to add.</param>
        /// <returns>New list.</returns>
        new IImmutableList<T> Add(T item);

        /// <summary>
        /// Inserts item at given index of a new list.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the list.</param>
        /// <returns>New list.</returns>
        IImmutableList<T> Insert(int index, T item);

        /// <summary>
        /// Removes first occurence of specified element from a new list.
        /// </summary>
        /// <param name="item">The object to remove.</param>
        /// <returns>New list.</returns>
        new IImmutableList<T> Remove(T item);

        /// <summary>
        /// Removes element at specified index of a new list.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <returns>New list.</returns>
        IImmutableList<T> RemoveAt(int index);

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element at specified index.</returns>
        T this[int index] { get; set; }

        /// <summary>
        /// Determines the index of the specified item in the list.
        /// </summary>
        /// <param name="item">The object to locate.</param>
        /// <returns>The index of item if found, otherwise -1.</returns>
        int IndexOf(T item);
    }
}
