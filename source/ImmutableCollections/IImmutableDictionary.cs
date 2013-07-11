using System.Collections.Generic;

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
        /// <returns>This, or new set.</returns>
        new IImmutableDictionary<TKey, TValue> Add(KeyValuePair<TKey, TValue> item);

        /// <summary>
        /// Adds an element with provided key and value to a new dictionary.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <returns>This, or new set.</returns>
        IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

        /// <summary>
        /// Removes the element with given key and value from a new dictionary.
        /// </summary>
        /// <param name="item">Item to be remved.</param>
        /// <returns>This, or new set.</returns>
        new IImmutableDictionary<TKey, TValue> Remove(KeyValuePair<TKey, TValue> item);

        /// <summary>
        /// Removes the element with the specified key from a new dictionary.
        /// </summary>
        /// <param name="key">The key of the element to be removed.</param>
        /// <returns>This, or new set.</returns>
        IImmutableDictionary<TKey, TValue> Remove(TKey key);

        bool TryGetValue(TKey key, out TValue value);

        bool ContainsKey(TKey key);
    }
}
