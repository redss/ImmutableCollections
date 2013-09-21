using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    class EmptyPatriciaTrie<T> : IPatriciaNode<T>
    {
        public bool Contains(int key, T item)
        {
            return false;
        }

        public IPatriciaNode<T> Insert(int key, T item)
        {
            return new PatriciaLeaf<T>(key, item);
        }

        public IEnumerable<T> GetItems()
        {
            yield break;
        }

        public IPatriciaNode<T> Remove(int key, T item)
        {
            return null;
        }

        public IPatriciaNode<T> Promote(int prefix, int mask)
        {
            throw new InvalidOperationException("Trie is empty.");
        }
    }
}
