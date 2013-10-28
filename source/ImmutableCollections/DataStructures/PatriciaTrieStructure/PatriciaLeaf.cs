using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Patricia leaf node storing key and associated values.
    /// </summary>
    /// <typeparam name="T">Type of items associated with keys.</typeparam>
    class PatriciaLeaf<T> : IPatriciaNode<T>
    {
        public readonly int Key;

        public readonly T[] Items;

        // Constructor

        public PatriciaLeaf(int key, T[] items)
        {
            Debug.Assert(items != null);

            Key = key;
            Items = items;
        }

        // IPatriciaNode

        public T[] Find(int key)
        {
            return key == Key ? Items : null;
        }

        public IPatriciaNode<T> Modify(int key, IPatriciaOperation<T> operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");

            if (key != Key)
            {
                var result = operation.OnInsert();

                // This situation shouldn't occur, maybe it should throw exception instead?
                if (result == null)
                    return this;

                // Create new leaf with new item.
                var newLeaf = new PatriciaLeaf<T>(key, result);
                return PatriciaHelper.Join(Key, this, key, newLeaf);
            }
            else
            {
                // Modify item.
                var result = operation.OnFound(Items);

                // Remove case.
                if (result == null)
                    return null;

                // Nothing changed.
                if (result == Items)
                    return this;

                return new PatriciaLeaf<T>(key, result);
            }
        }

        public IEnumerable<T> GetItems()
        {
            return Items;
        }

        // Public methods

        public override string ToString()
        {
            var items = string.Join(", ", Items);
            return string.Format("Lf({0} -> {1})", Key, items);
        }
    }
}
