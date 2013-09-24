using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.ImmutableLinkedListStructure;

namespace ImmutableCollections.DataStructures.AssociativeBackendStructure
{
    /// <summary>
    /// Backend for creating dictionaries.
    /// </summary>
    /// <typeparam name="TKey">Dictionary key type.</typeparam>
    /// <typeparam name="TValue">Dictionary value type.</typeparam>
    class DictionaryBackend<TKey, TValue> : IAssociativeBackend<KeyValuePair<TKey, TValue>>
    {
        private readonly IListNode<KeyValuePair<TKey, TValue>> _list;
 
        // Constructors

        public DictionaryBackend()
        {
            _list = new EmptyList<KeyValuePair<TKey, TValue>>();
        }

        private DictionaryBackend(IListNode<KeyValuePair<TKey, TValue>> list)
        {
            _list = list;
        }

        // IAssociativeBackend

        public IEnumerable<KeyValuePair<TKey, TValue>> GetValues()
        {
            return _list.GetValues();
        }

        public int Count()
        {
            return _list.Count;
        }

        public IAssociativeBackend<KeyValuePair<TKey, TValue>> Insert(KeyValuePair<TKey, TValue> item)
        {
            var newList = _list.Prepend(item);
            return new DictionaryBackend<TKey, TValue>(newList);
        }

        public IAssociativeBackend<KeyValuePair<TKey, TValue>> Remove(KeyValuePair<TKey, TValue> item)
        {
            var newList = _list.Remove(item);
            return new DictionaryBackend<TKey, TValue>(newList);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _list.GetValues().Any(x => x.Key.Equals(item.Key));
        }

        public bool IsSingle()
        {
            return _list.Count == 1;
        }

        // Public methods

        public override string ToString()
        {
            return IsSingle() ? GetValues().First().ToString() : string.Join(" ", GetValues());
        }
    }
}
