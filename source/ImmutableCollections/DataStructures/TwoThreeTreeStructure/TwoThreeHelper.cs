using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    static class TwoThreeHelper
    {
        public static ITwoThree<T> Insert<T>(ITwoThree<T> root, T item, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;

            T propagated;
            ITwoThree<T> left, right;
            var node = root.Insert(item, comparer, out left, out right, out propagated);

            return node ?? new TwoNode<T>(propagated, left, right);
        }

        public static ITwoThree<T> Remove<T>(ITwoThree<T> root, T item, IComparer<T> comparer = null)
        {
            comparer = comparer ?? Comparer<T>.Default;

            bool removed;
            return root.Remove(item, comparer, out removed);
        }
    }
}
