using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    class Empty<T> : ITwoThree<T>
    {
        public static Empty<T> Instance = new Empty<T>();

        // Constructor

        private Empty() { }

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
    }
}
