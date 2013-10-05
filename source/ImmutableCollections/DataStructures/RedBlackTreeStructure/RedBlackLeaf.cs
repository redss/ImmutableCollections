using System;
using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.RedBlackTreeStructure
{
    /// <summary>
    /// Red Black Tree leaf, ar just empty subtree. Every leaf is black.
    /// </summary>
    /// <typeparam name="T">Type of values stored in the tree.</typeparam>
    class RedBlackLeaf<T> : IRedBlack<T>
    {
        // Constructor

        private RedBlackLeaf() { }

        // Singleton

        public static readonly IRedBlack<T> Instance = new RedBlackLeaf<T>();

        // IRedBlack

        public bool IsBlack
        {
            get { return true; }
        }

        public T Value
        {
            get { throw new InvalidOperationException(); }
        }

        public IRedBlack<T> Left
        {
            get { throw new InvalidOperationException(); }
        }

        public IRedBlack<T> Right
        {
            get { throw new InvalidOperationException(); }
        }

        public bool TryFind(T searched, IComparer<T> comparer, out T value)
        {
            value = default(T);
            return false;
        }

        public IRedBlack<T> Update(T value, IComparer<T> comparer)
        {
            return new RedBlackNode<T>(false, value, Instance, Instance);
        }

        public IEnumerable<T> GetValues()
        {
            yield break;
        }
    }
}
