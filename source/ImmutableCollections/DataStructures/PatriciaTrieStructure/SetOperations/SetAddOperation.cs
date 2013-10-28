using ImmutableCollections.Helpers;
using System.Linq;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure.SetOperations
{
    /// <summary>
    /// Operation adding new element to the set.
    /// </summary>
    /// <typeparam name="T">Type of items stored in the set.</typeparam>
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
