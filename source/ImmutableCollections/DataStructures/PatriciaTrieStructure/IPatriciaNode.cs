using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Patricia Trie node.
    /// </summary>
    /// <typeparam name="T">Type stored in trie's leafs.</typeparam>
    interface IPatriciaNode<T>
    {
        /// <summary>
        /// True, if this Patricia trie contains given item.
        /// </summary>
        /// <param name="key">Usually hash key of searched item.</param>
        /// <param name="item">Searched item.</param>
        /// <returns>True, if element is contained in the collection.</returns>
        bool Contains(int key, T item);

        /// <summary>
        /// Inserts given item to the trie returning null, or returns element with given key.
        /// </summary>
        /// <param name="key">Usually hash key of searched item.</param>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>Prapageted node or null, if element was already in collection.</returns>
        IPatriciaNode<T> Insert(int key, T item);

        /// <summary>
        /// Iterates over values in the trie.
        /// </summary>
        IEnumerable<T> GetItems();

        /// <summary>
        /// Removes item with given key and value.
        /// </summary>
        /// <returns>Propagated node. If null, it means tree was left empty
        /// and thus shall be replaced with EmptyParticiaTrie.</returns>
        IPatriciaNode<T> Remove(int key, T item);

        /// <summary>
        /// Promote node to replace its parent. Updates node's prefix and mask if it's possible.
        /// </summary>
        /// <param name="prefix">New prefix.</param>
        /// <param name="mask">New mask.</param>
        /// <returns>Propagated Patricia node if it was branch, or unchanged leaf.</returns>
        IPatriciaNode<T> Promote(int prefix, int mask);
    }
}
