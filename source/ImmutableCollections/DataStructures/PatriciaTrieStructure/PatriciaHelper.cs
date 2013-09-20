namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Helper methods related to Patritia Trie.
    /// </summary>
    static class PatriciaHelper
    {
        /// <summary>
        /// Gets the branching bit, i. e. first bit on which integers "disagree".
        /// </summary>
        /// <param name="prefixA">First prefix of the Patricia trie.</param>
        /// <param name="prefixB">Second prefix of the Patricia tire.</param>
        /// <returns>Integer with only one (branching) bit set.</returns>
        public static int BranchingBit(int prefixA, int prefixB)
        {
            return LowestBit(prefixA ^ prefixB);
        }

        /// <summary>
        /// Gets the lowest bit in the integer.
        /// </summary>
        /// <param name="number">Any positive integer.</param>
        /// <returns>Integer with only lowest bit set.</returns>
        public static int LowestBit(int number)
        {
            return number & (~number + 1);
        }

        /// <summary>
        /// Joins two Patricia Tries with common branch.
        /// </summary>
        /// <typeparam name="T">Type of the data stored in Patricia Trie.</typeparam>
        /// <param name="prefixA">Prfix of the first Trie.</param>
        /// <param name="nodeA">First Patricia Trie.</param>
        /// <param name="prefixB">Prefix of the second Trie.</param>
        /// <param name="nodeB">Second Trie.</param>
        /// <returns>New Patritia Trie node having given nodes as her children.</returns>
        public static IPatriciaNode<T> Join<T>(int prefixA, IPatriciaNode<T> nodeA, int prefixB, IPatriciaNode<T> nodeB)
        {
            var bb = BranchingBit(prefixA, prefixB);
            var newPrefix = prefixA & (bb - 1);

            return (prefixA & bb) == 0 ? new PatriciaBranch<T>(newPrefix, bb, nodeA, nodeB) : new PatriciaBranch<T>(newPrefix, bb, nodeB, nodeA);
        }

        /// <summary>
        /// Does the given key and prefix match?
        /// </summary>
        /// <param name="key">Key (or other node's prefix).</param>
        /// <param name="prefix">Node's prefix.</param>
        /// <param name="mask">Node's mask.</param>
        /// <returns>True, if key and prefix match.</returns>
        public static bool MatchPrefix(int key, int prefix, int mask)
        {
            return Mask(key, mask) == prefix;
        }

        /// <summary>
        /// Masks the key with given mask.
        /// </summary>
        /// <param name="key">Key (or any other integer).</param>
        /// <param name="mask">Node's mask (should be power of two).</param>
        /// <returns>Masked key.</returns>
        public static int Mask(int key, int mask)
        {
            return key & mask - 1;
        }
    }
}
