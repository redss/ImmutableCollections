﻿using ImmutableCollections.Helpers;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure.DictionaryOperations
{
    /// <summary>
    /// Operation modifying value associated with a key in a dictionary.
    /// </summary>
    /// <typeparam name="TKey">Type of the keys stored in the dictionary.</typeparam>
    /// <typeparam name="TValue">Type of the values associated with the keys.</typeparam>
    class DictionarySetValueOperation<TKey, TValue> : IPatriciaOperation<KeyValuePair<TKey, TValue>>
    {
        private readonly TKey _key;

        private readonly TValue _value;

        public DictionarySetValueOperation(TKey key, TValue value)
        {
            _key = key;
            _value = value;
        }

        public KeyValuePair<TKey, TValue>[] OnFound(KeyValuePair<TKey, TValue>[] items)
        {
            var item = GetItem();

            for (var i = 0; i < items.Length; i++)
                if (items[i].Key.Equals(_key))
                    return item.Equals(items[i]) ? items : items.Change(item, i);

            return items.Append(GetItem());
        }

        public KeyValuePair<TKey, TValue>[] OnInsert()
        {
            return new[] { GetItem() };
        }

        private KeyValuePair<TKey, TValue> GetItem()
        {
            return new KeyValuePair<TKey, TValue>(_key, _value);
        }
    }
}
