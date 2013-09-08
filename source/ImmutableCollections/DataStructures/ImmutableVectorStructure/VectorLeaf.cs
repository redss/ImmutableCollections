using System;
using ImmutableCollections.DataStructures.Helpers;

namespace ImmutableCollections.DataStructures.ImmutableVectorStructure
{
    public class VectorLeaf<T> : IVectorNode<T>
    {
        private readonly T[] _elements;

        public VectorLeaf(T element)
        {
            _elements = new[] { element };
        }

        public VectorLeaf(T[] elements)
        {
            _elements = elements;
        }

        public int Level { get { return 0; } }

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

        public T Nth(int index)
        {
            var nodeIndex = ImmutableVectorHelper.CountIndex(index, 0);
            return _elements[nodeIndex];
        }

        public override string ToString()
        {
            return string.Format("Leaf[{0}]", _elements.Length);
        }
    }
}
