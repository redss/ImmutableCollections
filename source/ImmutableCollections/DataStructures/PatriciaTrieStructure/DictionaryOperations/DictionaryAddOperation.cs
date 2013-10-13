using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure.DictionaryOperations
{
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
