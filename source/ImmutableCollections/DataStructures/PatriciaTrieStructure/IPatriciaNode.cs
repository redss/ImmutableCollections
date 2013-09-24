using System.Collections.Generic;
using ImmutableCollections.DataStructures.AssociativeBackendStructure;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Patricia Trie node.
    /// </summary>
    /// <typeparam name="TValue">Type stored in trie's leafs.</typeparam>
    /// <typeparam name="TBackend">Type of the backend to store the values in.</typeparam>
    interface IPatriciaNode<TValue, TBackend>
        where TBackend : IAssociativeBackend<TValue>, new()
    {
        /// <summary>
        /// True, if this Patricia trie contains given item.
        /// </summary>
        /// <param name="key">Usually hash key of searched item.</param>
        /// <param name="item">Searched item.</param>
        /// <returns>True, if element is contained in the collection.</returns>
        bool Contains(int key, TValue item);

        /// <summary>
        /// Inserts given item to the trie returning null, or returns element with given key.
        /// </summary>
        /// <param name="key">Usually hash key of searched item.</param>
        /// <param name="item">Item to be inserted.</param>
        /// <returns>Prapageted node or null, if element was already in collection.</returns>
        IPatriciaNode<TValue, TBackend> Insert(int key, TValue item);

        /// <summary>
        /// Iterates over values in the trie.
        /// </summary>
        IEnumerable<TValue> GetItems();

        /// <summary>
        /// Removes item with given key and value.
        /// </summary>
        /// <returns>Propagated node. If null, it means tree was left empty
        /// and thus shall be replaced with EmptyParticiaTrie.</returns>
        IPatriciaNode<TValue, TBackend> Remove(int key, TValue item);

        /// <summary>
        /// Count items in the leafs.
        /// </summary>
        /// <returns>Number of items.</returns>
        int Count();
    }
}
