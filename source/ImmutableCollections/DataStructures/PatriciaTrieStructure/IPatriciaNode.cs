using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Patricia Trie node.
    /// </summary>
    /// <typeparam name="T">Type of items associated with keys. 
    /// Type should either be immutable, or not modified.</typeparam>
    interface IPatriciaNode<T>
        where T : class
    {
        /// <summary>
        /// Finds item associated with given key.
        /// </summary>
        /// <param name="key">Key associated with searched item.</param>
        /// <returns>Found item, or null if it was no found.</returns>
        T Find(int key);

        /// <summary>
        /// Modifies item associated with key using operation. If key was not found, null is passed to operation; if it returns non-null result, 
        /// node with given key is created. If key was found, but operation returns null, node associated with key is deleted.
        /// </summary>
        /// <param name="key">Key associated with modified or inserted item.</param>
        /// <param name="operation">Operation to be performed on existing item, or if parameter is null, an inserted item.</param>
        /// <returns>Propagated node, this, if nothing changed, or null if node was deleted.</returns>
        IPatriciaNode<T> Modify(int key, Func<T, T> operation);

        /// <summary>
        /// Ierates over all items stored in this trie.
        /// </summary>
        /// <returns>Collection of items.</returns>
        IEnumerable<T> GetItems();
    }
}
