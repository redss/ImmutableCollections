using System.Collections.Generic;
using System.Linq;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Patricia Trie's branch, i. e. node containing common prefix for all keys stored in it.
    /// </summary>
    /// <typeparam name="T">Type of the values stored in the leaf.</typeparam>
    class PatriciaBranch<T> : IPatriciaNode<T>
    {
        public readonly int Prefix;

        public readonly int Mask;

        public readonly IPatriciaNode<T> Left;

        public readonly IPatriciaNode<T> Right;

        // Constructor

        public PatriciaBranch(int prefix, int mask, IPatriciaNode<T> left, IPatriciaNode<T> right)
        {
            Prefix = prefix;
            Mask = mask;
            Left = left;
            Right = right;
        }

        // IPatriciaNode

        public bool Contains(int key, T item)
        {
            return GetPropagationNode(key).Contains(key, item);
        }

        public IPatriciaNode<T> Insert(int key, T item)
        {
            if (PatriciaHelper.MatchPrefix(key, Prefix, Mask))
            {
                var propagate = GetPropagationNode(key).Insert(key, item);
                return CopyBranch(key, propagate);
            }

            var leaf = new PatriciaLeaf<T>(key, item);
            return PatriciaHelper.Join(key, leaf, Prefix, this);
        }

        public IEnumerable<T> GetItems()
        {
            return Left.GetItems().Concat(Right.GetItems());
        }

        public IPatriciaNode<T> Remove(int key, T item)
        {
            var propagateLeft = PropagateLeft(key);

            var child = propagateLeft ? Left : Right;
            var other = propagateLeft ? Right : Left;

            var propagate = child.Remove(key, item);

            if (propagate == child)
                return this;

            if (propagate == null)
                return other.Promote(Prefix, Mask);

            return CopyBranch(key, propagate);
        }

        public IPatriciaNode<T> Promote(int prefix, int mask)
        {
            return new PatriciaBranch<T>(prefix, mask, Left, Right);
        }

        // Private methods

        private IPatriciaNode<T> CopyBranch(int key, IPatriciaNode<T> changedNode)
        {
            var propagateLeft = PropagateLeft(key);

            var newLeft = propagateLeft ? changedNode : Left;
            var newRight = propagateLeft ? Right : changedNode;

            return new PatriciaBranch<T>(Prefix, Mask, newLeft, newRight);
        }

        private IPatriciaNode<T> GetPropagationNode(int key)
        {
            return PropagateLeft(key) ? Left : Right;
        }

        private bool PropagateLeft(int key)
        {
            return (key & Mask) == 0;
        }
    }
}
