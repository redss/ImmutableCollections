namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    /// <summary>
    /// Immutable list, or its part.
    /// </summary>
    /// <typeparam name="T">Type contained in the list.</typeparam>
    public interface IListNode<T>
    {
        /// <summary>
        /// Gets the value stored in the node.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Gets the tail of the list.
        /// </summary>
        IListNode<T> Tail { get; }

        /// <summary>
        /// Inserts new node with given value before this node.
        /// </summary>
        /// <param name="value">Inserted value.</param>
        /// <returns>New node.</returns>
        IListNode<T> Prepend(T value);

        /// <summary>
        /// Creates new node with changed value.
        /// </summary>
        /// <param name="value">Changed value.</param>
        /// <returns>New node.</returns>
        IListNode<T> ChangeValue(T value);

        /// <summary>
        /// Creates new node with changed tail.
        /// </summary>
        /// <param name="tail">Changed tail.</param>
        /// <returns>New node.</returns>
        IListNode<T> ChangeTail(IListNode<T> tail);
    }
}
