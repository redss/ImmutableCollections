using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    class Empty<T> : ITwoThree<T>
    {
        public static Empty<T> Instance = new Empty<T>();

        // Constructor

        private Empty() { }

        // ITwoThree

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> left, out ITwoThree<T> right, out T propagated)
        {
            left = right = Instance;
            propagated = item;

            return null;
        }
    }
}
