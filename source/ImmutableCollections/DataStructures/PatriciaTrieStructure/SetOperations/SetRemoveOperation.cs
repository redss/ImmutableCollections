using System;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure.SetOperations
{
    /// <summary>
    /// Operation removing item from the set.
    /// </summary>
    /// <typeparam name="T">Type of items stored in the set.</typeparam>
    class SetRemoveOperation<T> : IPatriciaOperation<T>
    {
        private readonly T _item;

        public SetRemoveOperation(T item)
        {
            _item = item;
        }

        public T[] OnFound(T[] items)
        {
            var index = Array.IndexOf(items, _item);

            if (index == -1)
                return items;

            if (items.Length == 1)
                return null;

            return items.RemoveAt(index);
        }

        public T[] OnInsert()
        {
            return null;
        }
    }
}
