using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure.DictionaryOperations
{
    /// <summary>
    /// Operation adding item to the dictionary.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">Type of the values associated with the keys.</typeparam>
    class DictionaryAddOperation<TKey, TValue> : IPatriciaOperation<KeyValuePair<TKey, TValue>>
    {
        private readonly KeyValuePair<TKey, TValue> _item;

        public DictionaryAddOperation(KeyValuePair<TKey, TValue> item)
        {
            _item = item;
        }

        public KeyValuePair<TKey, TValue>[] OnFound(KeyValuePair<TKey, TValue>[] items)
        {
            return items.Any(t => t.Key.Equals(_item.Key)) ? items : items.Append(_item);
        }

        public KeyValuePair<TKey, TValue>[] OnInsert()
        {
            return new[] { _item };
        }
    }
}
