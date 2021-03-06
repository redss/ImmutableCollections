﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ImmutableCollections.DataStructures.ImmutableLinkedListStructure;
using ImmutableCollections.Helpers;

namespace ImmutableCollections
{
    public class ImmutableLinkedList<T> : IImmutableList<T>
    {
        private readonly IListNode<T> _first;

        // Constructors

        public ImmutableLinkedList()
        {
            _first = EmptyList<T>.Instance;

            Length = 0;
        }

        private ImmutableLinkedList(IListNode<T> first, int count)
        {
            _first = first;

            Length = count;
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
            return Insert(Length, item);
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
            if (index < 0)
                throw ExceptionHelper.GetIndexNegativeException(index, "item");

            if (index > Length)
                throw ExceptionHelper.GetIndexTooBigException(index, Length, "item");

            var nodes = new IListNode<T>[index];
            var current = _first;

            for (var i = 0; i < index; i++)
            {
                nodes[i] = current;
                current = current.Tail;
            }

            current = current.Prepend(item);

            for (var i = index - 1; i >= 0; i--)
                current = nodes[i].ChangeTail(current);

            return new ImmutableLinkedList<T>(current, Length + 1);
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
                throw ExceptionHelper.GetIndexNegativeException(index, "item");

            if (index >= Length)
                throw ExceptionHelper.GetIndexTooBigException(index, Length - 1, "item");

            var nodes = new IListNode<T>[index + 1];
            var current = _first;

            for (var i = 0; i < index; i++)
            {
                nodes[i] = current;
                current = current.Tail;
            }

            current = current.ChangeValue(item);

            for (var i = index - 1; i >= 0; i--)
                current = nodes[i].ChangeTail(current);

            return new ImmutableLinkedList<T>(current, Length);
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
                    var newNode = stack.Aggregate(node.Tail, (current, n) => n.ChangeTail(current));
                    return new ImmutableLinkedList<T>(newNode, Length - 1);
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
                throw ExceptionHelper.GetIndexNegativeException(index, "item");

            if (index >= Length)
                throw ExceptionHelper.GetIndexTooBigException(index, Length - 1, "item");

            var nodes = new IListNode<T>[index];
            var current = _first;

            for (var i = 0; i < index; i++)
            {
                nodes[i] = current;
                current = current.Tail;
            }

            current = current.Tail;

            for (var i = index - 1; i >= 0; i--)
                current = nodes[i].ChangeTail(current);

            return new ImmutableLinkedList<T>(current, Length - 1);
        }

        [Pure]
        IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
        {
            return RemoveAt(index);
        }

        [Pure]
        public int Length { get; }

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
                    throw ExceptionHelper.GetIndexNegativeException(index, "item");

                if (index >= Length)
                    throw ExceptionHelper.GetIndexTooBigException(index, Length - 1, "item");

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
