using System.Collections.Generic;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure.DictionaryOperations
{
    class DictionaryRemoveOperation<TKey, TValue> : IPatriciaOperation<KeyValuePair<TKey, TValue>>
    {
        private readonly TKey _key;

        public DictionaryRemoveOperation(TKey key)
        {
            _key = key;
        }

        public KeyValuePair<TKey, TValue>[] OnFound(KeyValuePair<TKey, TValue>[] items)
        {
            var length = items.Length;

            for (var i = 0; i < length; i++)
                if (items[i].Key.Equals(_key))
                    return items.RemoveAt(i);

            return items;
        }

        public KeyValuePair<TKey, TValue>[] OnInsert()
        {
            return null;
        }
    }
}
