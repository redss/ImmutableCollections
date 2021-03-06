﻿using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.BitmappedVectorTrieStructure
{
    class VectorLeaf<T> : IVectorNode<T>
    {
        private readonly T[] _elements;

        // Constructors

        public VectorLeaf(T element)
        {
            _elements = new[] { element };
        }

        public VectorLeaf(T[] elements)
        {
            _elements = elements;
        }

        // IVectorNode

        public int Level => 0;

        public IEnumerable<T> GetValues()
        {
            return _elements.AsEnumerable();
        }

        public IVectorNode<T> Append(T elem, int count)
        {
            if (_elements.Length == ImmutableVectorHelper.Fragmentation)
            {
                var children = new IVectorNode<T>[] { this, new VectorLeaf<T>(elem) };

                return new VectorLevel<T>(children, Level + 1);
            }

            var elements = _elements.Append(elem);

            return new VectorLeaf<T>(elements);
        }

        public IVectorNode<T> UpdateAt(T elem, int index)
        {
            var nodeIndex = CountIndex(index);
            var newElements = _elements.Change(elem, nodeIndex);

            return new VectorLeaf<T>(newElements);
        }

        public T Nth(int index)
        {
            var nodeIndex = CountIndex(index);

            return _elements[nodeIndex];
        }

        public IVectorNode<T> UpdateAndRemove(T item, int index)
        {
            var itemIndex = CountIndex(index);
            var newElements = _elements.TakeAndChange(item, itemIndex, itemIndex + 1);

            return new VectorLeaf<T>(newElements);
        }

        public IVectorNode<T> Remove(int index)
        {
            var nodeIndex = CountIndex(index);
            var newElements = _elements.ArrayTake(nodeIndex + 1);

            return new VectorLeaf<T>(newElements);
        }

        // Object

        public override string ToString()
        {
            return $"Leaf[{_elements.Length}]";
        }

        // Private methods

        private int CountIndex(int index)
        {
            return ImmutableVectorHelper.ComputeIndex(index, 0);
        }
    }
}
