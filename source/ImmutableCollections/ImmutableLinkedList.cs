using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using ImmutableCollections.DataStructures.ImmutableLinkedListStructure;

namespace ImmutableCollections
{
    public class ImmutableLinkedList<T> : IImmutableList<T>
    {
        private readonly IListNode<T> _first;

        private readonly int _count;

        // Constructors

        public ImmutableLinkedList()
        {
            _first = new EmptyList<T>();
            _count = 0;
        }

        private ImmutableLinkedList(IListNode<T> first, int count)
        {
            _first = first;
            _count = count;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            return _first.GetValues().GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableList

        [Pure]
        public ImmutableLinkedList<T> Add(T item)
        {
            var newFirst = _first.Append(item);
            return new ImmutableLinkedList<T>(newFirst, _count + 1);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Add(T item)
        {
            return Add(item);
        }

        [Pure]
        IImmutableCollection<T> IImmutableCollection<T>.Add(T item)
        {
            return Add(item);
        }

        [Pure]
        public ImmutableLinkedList<T> Insert(int index, T item)
        {
            var newFirst = _first.Insert(index, item);
            return new ImmutableLinkedList<T>(newFirst, _count + 1);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
        {
            return Insert(index, item);
        }

        [Pure]
        public ImmutableLinkedList<T> UpdateAt(int index, T item)
        {
            var newFirst = _first.UpdateAt(index, item);
            return new ImmutableLinkedList<T>(newFirst, _count);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.UpdateAt(int index, T item)
        {
            return UpdateAt(index, item);
        }

        [Pure]
        public ImmutableLinkedList<T> Remove(T item)
        {
            var newFirst = _first.Remove(item);
            return new ImmutableLinkedList<T>(newFirst, _count - 1);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        [Pure]
        public ImmutableLinkedList<T> RemoveAt(int index)
        {
            var newFirst = _first.RemoveAt(index);
            return new ImmutableLinkedList<T>(newFirst, _count - 1);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        [Pure]
        public int Length { get { return _count; } }

        [Pure]
        public bool Contains(T item)
        {
            return _first.Contains(item);
        }

        [Pure]
        public T this[int index]
        {
            get { return _first.ElementAt(index); }
        }

        [Pure]
        public int IndexOf(T item)
        {
            return _first.IndexOf(item);
        }
    }
}
