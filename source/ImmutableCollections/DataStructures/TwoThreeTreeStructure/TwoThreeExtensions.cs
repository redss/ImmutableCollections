namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    static class TwoThreeExtensions
    {
        public static bool IsNullOrEmpty<T>(this ITwoThree<T> node)
        {
            return node == null || node is Empty<T>;
        }

        public static bool IsBalanced<T>(this ITwoThree<T> node)
        {
            int depth;
            return node.IsBalanced(out depth);
        }
    }
}
