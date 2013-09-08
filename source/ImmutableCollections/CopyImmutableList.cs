using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ImmutableCollections
{
    /// <summary>
    /// Simplest possible immutable list implementation, for tests and comparison purposes only.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public class CopyImmutableList<T> : IImmutableList<T>
    {
        private readonly List<T> _list;

        // Constructors

        public CopyImmutableList()
        {
            _list = new List<T>();
        }

        public CopyImmutableList(List<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            _list = list;
        }

        // IEnumerable

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableList

        [Pure]
        public CopyImmutableList<T> Add(T item)
        {
            var newList = new List<T>(_list) {item};

            return new CopyImmutableList<T>(newList);
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
        public CopyImmutableList<T> Insert(int index, T item)
        {
            var newList = new List<T>(_list);
            newList.Insert(index, item);

            return new CopyImmutableList<T>(newList);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
        {
            return Insert(index, item);
        }

        [Pure]
        public CopyImmutableList<T> Remove(T item)
        {
            var newList = new List<T>(_list);
            newList.Remove(item);

            return new CopyImmutableList<T>(newList);
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
        public CopyImmutableList<T> RemoveAt(int index)
        {
            var newList = new List<T>(_list);
            newList.RemoveAt(index);

            return new CopyImmutableList<T>(newList);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        [Pure]
        public int Length
        {
            get { return _list.Count; }
        }

        [Pure]
        public T this[int index]
        {
            get { return _list[index]; }
        }

        [Pure]
        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        [Pure]
        public int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }
    }
}
