using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    interface IPatriciaNode<T>
    {
        /// <summary>
        /// Finds element with a given key, or returns null if it cannot be found.
        /// </summary>
        /// <param name="key">Usually hash key of searched item.</param>
        /// <returns>Found element or null, if it was not found.</returns>
        //T Find(int key);

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
    }
}
