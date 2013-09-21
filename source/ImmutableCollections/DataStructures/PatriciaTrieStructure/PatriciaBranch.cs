using System.Collections.Generic;
using System.Linq;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
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
            return (((key & Mask) == 0) ? Left : Right).Contains(key, item);
        }

        public IPatriciaNode<T> Insert(int key, T item)
        {
            if (PatriciaHelper.MatchPrefix(key, Prefix, Mask))
            {
                if ((key & Mask) == 0)
                {
                    var propagate = Left.Insert(key, item);
                    return new PatriciaBranch<T>(Prefix, Mask, propagate, Right);
                }
                else
                {
                    var propagate = Right.Insert(key, item);
                    return new PatriciaBranch<T>(Prefix, Mask, Left, propagate);
                }
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
            var propagateLeft = (key & Mask) == 0;

            var child = propagateLeft ? Left : Right;
            var other = propagateLeft ? Right : Left;

            var propagate = child.Remove(key, item);
            
            if (propagate == child)
                return this;

            if (propagate == null)
                return other.Promote(Prefix, Mask);

            var newLeft = propagateLeft ? propagate : other;
            var newRight = propagateLeft ? other : propagate;

            return new PatriciaBranch<T>(Prefix, Mask, newLeft, newRight);
        }

        public IPatriciaNode<T> Promote(int prefix, int mask)
        {
            return new PatriciaBranch<T>(prefix, mask, Left, Right);
        }
    }
}
