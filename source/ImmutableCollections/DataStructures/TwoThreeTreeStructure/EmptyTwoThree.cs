using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    class EmptyTwoThree<T> : ITwoThree<T>
    {
        // Singleton

        public static readonly EmptyTwoThree<T> Instance = new EmptyTwoThree<T>();

        private EmptyTwoThree() { }

        // ITwoThree

        public IEnumerable<T> GetValues()
        {
            yield break;
        }

        public bool TryFind(T item, IComparer<T> comparer, out T value)
        {
            value = default(T);

            return false;
        }

        public T Min()
        {
            throw new InvalidOperationException();
        }

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> splitLeft, out ITwoThree<T> splitRight, out T splitValue)
        {
            splitLeft = splitRight = Instance;
            splitValue = item;

            return null;
        }

        public ITwoThree<T> Update(T item, IComparer<T> comparer)
        {
            return null;
        }

        public ITwoThree<T> Remove(T item, IComparer<T> comparer, out bool removed)
        {
            // Item wasn't found.
            removed = false;

            return Instance;
        }

        public bool IsBalanced(out int depth)
        {
            depth = 0;

            return true;
        }

        // Public methods

        public override string ToString()
        {
            return "Empty";
        }
    }
}
