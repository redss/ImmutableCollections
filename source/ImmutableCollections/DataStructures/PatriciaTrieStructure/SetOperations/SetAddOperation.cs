using ImmutableCollections.Helpers;
using System.Linq;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure.SetOperations
{
    class SetAddOperation<T> : IPatriciaOperation<T>
    {
        private readonly T _item;

        public SetAddOperation(T item)
        {
            _item = item;
        }

        public T[] OnFound(T[] items)
        {
            return items.Contains(_item) ? items : items.Append(_item);
        }

        public T[] OnInsert()
        {
            return new[] { _item };
        }
    }
}
