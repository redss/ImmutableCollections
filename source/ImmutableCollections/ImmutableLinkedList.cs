using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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
            _first = EmptyList<T>.Instance;
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
            for (var node = _first; node is ListNode<T>; node = node.Tail)
                yield return node.Value;
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
            return Insert(_count, item);
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
            // TODO: Better exceptions.

            if (index < 0)
                throw new ArgumentOutOfRangeException();

            if (index > _count)
                throw new ArgumentOutOfRangeException();

            var nodes = new IListNode<T>[index];
            var current = _first;

            for (var i = 0; i < index; i++)
            {
                nodes[i] = current;
                current = current.Tail;
            }

            current = current.Prepend(item);

            for (var i = index - 1; i >= 0; i--)
                current = nodes[i].Change(current);

            return new ImmutableLinkedList<T>(current, _count + 1);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
        {
            return Insert(index, item);
        }

        [Pure]
        public ImmutableLinkedList<T> UpdateAt(int index, T item)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException();

            if (index >= _count)
                throw new ArgumentOutOfRangeException();

            var nodes = new IListNode<T>[index + 1];
            var current = _first;

            for (var i = 0; i < index; i++)
            {
                nodes[i] = current;
                current = current.Tail;
            }

            current = current.Change(item);

            for (var i = index - 1; i >= 0; i--)
                current = nodes[i].Change(current);

            return new ImmutableLinkedList<T>(current, _count);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.UpdateAt(int index, T item)
        {
            return UpdateAt(index, item);
        }

        [Pure]
        public ImmutableLinkedList<T> Remove(T item)
        {
            var stack = new Stack<IListNode<T>>(); 

            for (var node = _first; node is ListNode<T>; node = node.Tail)
            {
                if (node.Value.Equals(item))
                {
                    var newNode = stack.Aggregate(node.Tail, (current, n) => n.Change(current));
                    return new ImmutableLinkedList<T>(newNode, _count - 1);
                }

                stack.Push(node);
            }

            return this;
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
            if (index < 0)
                throw new ArgumentOutOfRangeException();

            if (index >= _count)
                throw new ArgumentOutOfRangeException();

            var nodes = new IListNode<T>[index];
            var current = _first;

            for (var i = 0; i < index; i++)
            {
                nodes[i] = current;
                current = current.Tail;
            }

            current = current.Tail;

            for (var i = index - 1; i >= 0; i--)
                current = nodes[i].Change(current);

            return new ImmutableLinkedList<T>(current, _count - 1);
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
            for (var node = _first; _first is ListNode<T>; node = node.Tail)
                if (node.Value.Equals(item))
                    return true;

            return false;
        }

        [Pure]
        public T this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();

                if (index >= _count)
                    throw new ArgumentOutOfRangeException();

                var current = _first;

                for (var i = 0; i != index; i++)
                    current = current.Tail;

                return current.Value;
            }
        }

        [Pure]
        public int IndexOf(T item)
        {
            var index = 0;

            for (var node = _first; node is ListNode<T>; node = node.Tail)
            {
                if (node.Value.Equals(item))
                    return index;
                
                index++;
            }

            return -1;
        }
    }
}
