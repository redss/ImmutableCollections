﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ImmutableCollections
{
    /// <summary>
    /// Simplest possible immutable list implementation, for tests and comparison purposes only.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public class CopyImmutableList<T> : BaseImmutableList<T>
    {
        private readonly List<T> _list;

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
        public override IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        // IImmutableCollection

        [Pure]
        public override int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        [Pure]
        public override int Count
        {
            get { return _list.Count; }
        }

        // IImmutableList

        [Pure]
        public override IImmutableList<T> Add(T item)
        {
            var newList = new List<T>(_list) {item};

            return new CopyImmutableList<T>(newList);
        }

        [Pure]
        public override IImmutableList<T> Insert(int index, T item)
        {
            var newList = new List<T>(_list);
            newList.Insert(index, item);

            return new CopyImmutableList<T>(newList);
        }

        [Pure]
        public override IImmutableList<T> Remove(T item)
        {
            var newList = new List<T>(_list);
            newList.Remove(item);

            return new CopyImmutableList<T>(newList);
        }

        [Pure]
        public override IImmutableList<T> RemoveAt(int index)
        {
            var newList = new List<T>(_list);
            newList.RemoveAt(index);

            return new CopyImmutableList<T>(newList);
        }

        [Pure]
        public override T this[int index]
        {
            get { return _list[index]; }
        }

        [Pure]
        public override bool Contains(T item)
        {
            return _list.Contains(item);
        }
    }
}
