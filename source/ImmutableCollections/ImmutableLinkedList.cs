using System;
using System.Collections;
using System.Collections.Generic;

namespace ImmutableCollections
{
    public class ImmutableLinkedList<T> : IImmutableList<T>
    {
        // IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableList

        public ImmutableLinkedList<T> Add(T item)
        {
            throw new NotImplementedException();
        }

        IImmutableList<T> IImmutableList<T>.Add(T item)
        {
            return Add(item);
        }

        IImmutableCollection<T> IImmutableCollection<T>.Add(T item)
        {
            return Add(item);
        }

        public ImmutableLinkedList<T> Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
        {
            return Insert(index, item);
        }

        public ImmutableLinkedList<T> UpdateAt(int index, T item)
        {
            throw new NotImplementedException();
        }

        IImmutableList<T> IImmutableList<T>.UpdateAt(int index, T item)
        {
            return UpdateAt(index, item);
        }
        
        public ImmutableLinkedList<T> Remove(T item)
        {
            throw new NotImplementedException();
        }

        IImmutableList<T> IImmutableList<T>.Remove(T item)
        {
            return Remove(item);
        }

        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        public ImmutableLinkedList<T> RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        public int Length { get { throw new NotImplementedException(); } }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public T this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public int IndexOf(T item)
        {
            throw new NotImplementedException();
        }
    }
}
