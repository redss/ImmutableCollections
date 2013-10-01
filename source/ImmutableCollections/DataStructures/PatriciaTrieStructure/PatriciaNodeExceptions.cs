using System;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    static class PatriciaNodeExceptions
    {
        public static IPatriciaNode<T> Modify<T>(this IPatriciaNode<T> node, int key, Func<T, T> found, Func<T> insert)
            where T : class
        {
            return node.Modify(key, i => i != null ? found(i) : insert());
        }
    }
}
