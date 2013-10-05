namespace ImmutableCollections.DataStructures.RedBlackTreeStructure
{
    /// <summary>
    /// Extension methods for Red Black Tree nodes.
    /// </summary>
    static class RedBlackExtensions
    {
        /// <summary>
        /// Checks if instance is black node.
        /// </summary>
        /// <typeparam name="T">Type contained in Red Black Tree.</typeparam>
        /// <param name="node">Checked node.</param>
        /// <returns>True, if node is not a leaf and it's black.</returns>
        public static bool IsRedNode<T>(this IRedBlack<T> node)
        {
            return node is RedBlackNode<T> && !node.IsBlack;
        }
    }
}
