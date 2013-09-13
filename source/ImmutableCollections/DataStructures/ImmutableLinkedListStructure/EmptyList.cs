using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    class EmptyList<T> : IListNode<T>
    {
        public IEnumerable<T> GetValues()
        {
            yield break;
        }

        public int Count { get { return 0; } }
        
        public IListNode<T> Prepend(T item)
        {
            return new ListNode<T>(item, this);
        }

        public IListNode<T> Append(T item)
        {
            return Prepend(item);
        }

        public IListNode<T> Insert(int index, T item)
        {
            if (index == 0)
                return Append(item);

            throw new ArgumentOutOfRangeException("index");
        }

        public IListNode<T> UpdateAt(int index, T item)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        public IListNode<T> Remove(T item)
        {
            return this;
        }

        public IListNode<T> RemoveAt(int index)
        {
            throw new ArgumentOutOfRangeException("index");
        }

        public bool Contains(T item)
        {
            return false;
        }

        public int IndexOf(T item, int counter)
        {
            return -1;
        }

        public T ElementAt(int index)
        {
            throw new ArgumentOutOfRangeException("index");
        }
    }
}
