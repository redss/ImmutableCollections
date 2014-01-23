using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ImmutableCollections.Helpers
{
    /// <summary>
    /// Comparer comparing only keys of given key-value pairs.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys.</typeparam>
    /// <typeparam name="TValue">Type of the values.</typeparam>
    class KeyComparer<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
    {
        private readonly IComparer<TKey> _keyComparer;

        /// <summary>
        /// Creates new instance of KeyComparer.
        /// </summary>
        /// <param name="keyComparer">Key comparer; if null, will be set to default comparer.</param>
        public KeyComparer(IComparer<TKey> keyComparer = null)
        {
            _keyComparer = keyComparer ?? Comparer<TKey>.Default;
        }

        [Pure]
        public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
        {
            return _keyComparer.Compare(x.Key, y.Key);
        }
    }
}
