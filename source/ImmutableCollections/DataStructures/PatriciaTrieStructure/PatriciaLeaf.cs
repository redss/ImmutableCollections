using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    class PatriciaLeaf<T> : IPatriciaNode<T>
    {
        public readonly int Key;

        public readonly ImmutableLinkedList<T> Values;

        // Constructor

        public PatriciaLeaf(int key, ImmutableLinkedList<T> values)
        {
            Key = key;
            Values = values;
        }

        public PatriciaLeaf(int key, T item)
        {
            Key = key;
            Values = new ImmutableLinkedList<T>().Add(item);
        }

        // IPatriciaNode

        public bool Contains(int key, T item)
        {
            return (key == Key) && Values.Contains(item);
        }

        public IPatriciaNode<T> Insert(int key, T item)
        {
            if (key != Key)
                return PatriciaHelper.Join(Key, this, key, new PatriciaLeaf<T>(key, item));

            if (Values.Contains(item))
                return null;

            var newValues = Values.Insert(0, item);
            return new PatriciaLeaf<T>(key, newValues);
        }

        public IEnumerable<T> GetItems()
        {
            return Values;
        }
    }
}
