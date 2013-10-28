namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Operation (strategy) used on node's items in IPatriciaNode Modify method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IPatriciaOperation<T>
    {
        /// <summary>
        /// Operation to be triggered when leaf associated with the key was found.
        /// </summary>
        /// <param name="items">Items contained in the leaf.</param>
        /// <returns>New items; if null, node will be removed.</returns>
        T[] OnFound(T[] items);

        /// <summary>
        /// Operation to be triggered when key was not present in the tree.
        /// </summary>
        /// <returns>Content of the inserted leaf; if null, leaf won't be inserted.</returns>
        T[] OnInsert();
    }
}