using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    static class TwoThreeExtensions
    {
        public static bool IsNullOrEmpty<T>(this ITwoThree<T> node)
        {
            return node == null || node is Empty<T>;
        }
    }
}
