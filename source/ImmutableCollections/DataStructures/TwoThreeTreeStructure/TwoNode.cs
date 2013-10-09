using System.Collections.Generic;
using System.Diagnostics;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    /// <summary>
    /// 2-node from 2-3 Tree. Can grow to 3-node if value is inserted into it.
    /// </summary>
    /// <typeparam name="T">Type stored in the tree.</typeparam>
    class TwoNode<T> : ITwoThree<T>
    {
        private enum Side { Left, Right, Same }

        private readonly T _value;

        private readonly ITwoThree<T> _left, _right;

        // Constructors

        public TwoNode(T value, ITwoThree<T> left, ITwoThree<T> right)
        {
            _value = value;

            _left = left;
            _right = right;
        }

        // ITwoThree

        public IEnumerable<T> GetValues()
        {
            foreach (var value in _left.GetValues())
                yield return value;

            yield return _value;

            foreach (var value in _right.GetValues())
                yield return value;
        }

        public bool TryFind(T item, IComparer<T> comparer, out T value)
        {
            var side = GetSide(item, comparer);

            if (side == Side.Same)
            {
                value = _value;
                return true;
            }

            return GetChild(side).TryFind(item, comparer, out value);
        }

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> splitLeft, out ITwoThree<T> splitRight, out T splitValue)
        {
            var side = GetSide(item, comparer);

            if (side == Side.Same)
            {
                splitLeft = null;
                splitRight = null;
                splitValue = default(T);

                return this;
            }

            var child = GetChild(side);
            var node = child.Insert(item, comparer, out splitLeft, out splitRight, out splitValue);

            if (node == null)
            {
                // Child node split.
                return side == Side.Left
                    ? new ThreeNode<T>(splitValue, _value, splitLeft, splitRight, _right)
                    : new ThreeNode<T>(_value, splitValue, _left, splitLeft, splitRight);
            }

            // Single node was splitValue.

            if (node == child)
                return this;

            return side == Side.Left
                ? new TwoNode<T>(_value, node, _right)
                : new TwoNode<T>(_value, _left, node);
        }

        public ITwoThree<T> Update(T item, IComparer<T> comparer)
        {
            var side = GetSide(item, comparer);

            if (side == Side.Same)
                return item.Equals(_value) ? this : new TwoNode<T>(item, _left, _right);

            var child = GetChild(side);
            var node = child.Update(item, comparer);

            return child == node ? this : node;
        }

        // Private members

        private Side GetSide(T item, IComparer<T> comparer)
        {
            var result = comparer.Compare(item, _value);

            if (result == 0)
                return Side.Same;

            return result < 0 ? Side.Left : Side.Right;
        }

        private ITwoThree<T> GetChild(Side side)
        {
            Debug.Assert(side != Side.Same);

            return side == Side.Left ? _left : _right;
        }
    }
}
