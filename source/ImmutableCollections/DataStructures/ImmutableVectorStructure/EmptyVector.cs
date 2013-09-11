using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.ImmutableVectorStructure
{
    class EmptyVector<T> : IVectorNode<T>
    {
        // IVectorNode

        public int Level { get { return 0; } }

        public IEnumerable<T> GetValues()
        {
            yield break;
        }

        public IVectorNode<T> Append(T elem, int count)
        {
            return new VectorLeaf<T>(elem);
        }

        public IVectorNode<T> UpdateAt(T elem, int index)
        {
            throw GetException();
        }

        public T Nth(int index)
        {
            throw new ArgumentOutOfRangeException("index", "Vector is empty.");
        }

        public IVectorNode<T> UpdateAndRemove(T item, int index)
        {
            throw GetException();
        }

        public IVectorNode<T> Remove(int index)
        {
            throw GetException();
        }

        // Object

        public override string ToString()
        {
            return "Empty vector.";
        }

        // Private methods

        private static InvalidOperationException GetException()
        {
            return new InvalidOperationException("Vector is empty.");
        }
    }
}
