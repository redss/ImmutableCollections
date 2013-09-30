using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ImmutableCollections
{
    /// <summary>
    /// Represents a generic immutable collection of key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in dictionary.</typeparam>
    public interface IImmutableDictionary<TKey, TValue> : IImmutableCollection<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Adds an item to a new dictionary.
        /// </summary>
        /// <param name="item">Item to be added.</param>
        /// <returns>This, or new dictionary.</returns>
        [Pure]
        new IImmutableDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item);

        /// <summary>
        /// Adds an element with provided key and value to a new dictionary.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <returns>This, or new dictionary.</returns>
        [Pure]
        IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

        /// <summary>
        /// Removes the element with the specified key from a new dictionary.
        /// </summary>
        /// <param name="key">The key of the element to be removed.</param>
        /// <returns>This, or new set.</returns>
        [Pure]
        IImmutableDictionary<TKey, TValue> Remove(TKey key);

        /// <summary>
        /// Sets the value associated with the given key.
        /// </summary>
        /// <param name="key">The key of the element to be changed.</param>
        /// <param name="value">New value associated with the key.</param>
        /// <returns>This, or new dictionary.</returns>
        [Pure]
        IImmutableDictionary<TKey, TValue> SetValue(TKey key, TValue value);

        /// <summary>
        /// Gets value associated with given key. If it's not found, KeyNotFoundException is thrown.
        /// </summary>
        /// <param name="k">The key of the value to get.</param>
        /// <returns>Value associated with a key.</returns>
        [Pure]
        TValue this[TKey k] { get; }

        /// <summary>
        /// Gets the value associated with given key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">Value associated with given key, or default value if it was not found.</param>
        /// <returns>True, if value was found.</returns>
        [Pure]
        bool TryGetValue(TKey key, out TValue value);

        /// <summary>
        /// Gets a collection of dictionary keys.
        /// </summary>
        [Pure]
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// Gets a collection of dictionary values.
        /// </summary>
        [Pure]
        IEnumerable<TValue> Values { get; } 

        /// <summary>
        /// Determines whether the dictionary contains given key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>True, if key was found.</returns>
        [Pure]
        bool ContainsKey(TKey key);

        /// <summary>
        /// Determines whether the dictionary contains at least one item with given value.
        /// </summary>
        /// <param name="value">The value to locate.</param>
        /// <returns>True, if value was found.</returns>
        [Pure]
        bool ContainsValue(TValue value);
    }
}
