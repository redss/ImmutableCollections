using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    interface ITwoThree<T>
    {
        ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> left, out ITwoThree<T> right, out T propagated);
    }
}