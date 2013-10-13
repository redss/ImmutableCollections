using System.Collections;
using System.Collections.Generic;
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

        public IEnumerator<T> GetEnumerator()
        {
            return _first.GetValues().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableList

        public ImmutableLinkedList<T> Add(T item)
        {
            var newFirst = _first.Append(item);
            return new ImmutableLinkedList<T>(newFirst, _count + 1);
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
            var newFirst = _first.Insert(index, item);
            return new ImmutableLinkedList<T>(newFirst, _count + 1);
        }

        IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
        {
            return Insert(index, item);
        }

        public ImmutableLinkedList<T> UpdateAt(int index, T item)
        {
            var newFirst = _first.UpdateAt(index, item);
            return new ImmutableLinkedList<T>(newFirst, _count);
        }

        IImmutableList<T> IImmutableList<T>.UpdateAt(int index, T item)
        {
            return UpdateAt(index, item);
        }
        
        public ImmutableLinkedList<T> Remove(T item)
        {
            var newFirst = _first.Remove(item);
            return new ImmutableLinkedList<T>(newFirst, _count - 1);
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
            var newFirst = _first.RemoveAt(index);
            return new ImmutableLinkedList<T>(newFirst, _count - 1);
        }

        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        public int Length { get { return _count; } }

        public bool Contains(T item)
        {
            return _first.Contains(item);
        }

        public T this[int index]
        {
            get { return _first.ElementAt(index); }
        }

        public int IndexOf(T item)
        {
            return _first.IndexOf(item);
        }
    }
}
