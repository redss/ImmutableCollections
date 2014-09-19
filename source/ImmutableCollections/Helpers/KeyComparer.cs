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
        public readonly IComparer<TKey> InnerComparer;

        public KeyComparer()
        {
            InnerComparer = Comparer<TKey>.Default;
        }

        public KeyComparer(IComparer<TKey> keyComparer)
        {
            InnerComparer = keyComparer ?? Comparer<TKey>.Default;
        }

        [Pure]
        public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
        {
            return InnerComparer.Compare(x.Key, y.Key);
        }
    }
}
