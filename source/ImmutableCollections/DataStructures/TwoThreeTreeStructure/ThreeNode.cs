using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    /// <summary>
    /// 3-node from 2-3 Tree. Can split into two 2-nodes if value is inserted into it.
    /// </summary>
    /// <typeparam name="T">Type stored in the tree.</typeparam>
    class ThreeNode<T> : ITwoThree<T>
    {
        private enum Side { Same, Left, Middle, Right }

        private readonly T _first, _second;

        private readonly ITwoThree<T> _left, _middle, _right; 

        // Constructors

        public ThreeNode(T first, T second, ITwoThree<T> left, ITwoThree<T> middle, ITwoThree<T> right)
        {
            _first = first;
            _second = second;

            _left = left;
            _middle = middle;
            _right = right;

            Debug.Assert(left.IsNullOrEmpty() && middle.IsNullOrEmpty() && right.IsNullOrEmpty()
                || !left.IsNullOrEmpty() && !middle.IsNullOrEmpty() && !right.IsNullOrEmpty());
        }

        // ITwoThree

        public IEnumerable<T> GetValues()
        {
            foreach (var value in _left.GetValues())
                yield return value;

            yield return _first;

            foreach (var value in _middle.GetValues())
                yield return value;

            yield return _second;

            foreach (var value in _right.GetValues())
                yield return value;
        }

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> splitLeft, out ITwoThree<T> splitRight, out T splitValue)
        {
            // Default split values.
            splitLeft = splitRight = null;
            splitValue = default(T);

            // Get side to insert node.
            var side = GetSide(item, comparer);

            // Values are equal, no need to change tree.
            if (side == Side.Same)
                return this;

            // Insert value into proper node.
            T pv;
            ITwoThree<T> pl, pr;
            var child = GetChild(side);
            var node = child.Insert(item, comparer, out pl, out pr, out pv);

            // Insert propagated single node.
            if (node != null)
                return node == child ? this : NewChangedNode(node, side);

            // Insert propagated two nodes and value, meaning it split.
            // Sinde this is 3-node, we are at full capacity and need to split too.
            switch (side)
            {
                case Side.Left:
                    splitLeft = new TwoNode<T>(pv, pl, pr);
                    splitRight = new TwoNode<T>(_second, _middle, _right);
                    splitValue = _first;
                    break;

                case Side.Middle:
                    splitLeft = new TwoNode<T>(_first, _left, pl);
                    splitRight = new TwoNode<T>(_second, pr, _right);
                    splitValue = pv;
                    break;

                case Side.Right:
                    splitLeft = new TwoNode<T>(_first, _left, _middle);
                    splitRight = new TwoNode<T>(pv, pl, pr);
                    splitValue = _second;
                    break;
            }

            return null;
        }

        // Private methods

        private Side GetSide(T item, IComparer<T> comparer)
        {
            var firstResult = comparer.Compare(item, _first);
            var secondResult = comparer.Compare(item, _second);

            if (firstResult * secondResult == 0)
                return Side.Same;

            if (firstResult < 0)
                return Side.Left;
            
            if (secondResult > 0)
                return Side.Right;
            
            return Side.Middle;
        }

        private ITwoThree<T> GetChild(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    return _left;

                case Side.Middle:
                    return _middle;

                case Side.Right:
                    return _right;

                default:
                    throw new InvalidOperationException();
            }
        }

        private ITwoThree<T> NewChangedNode(ITwoThree<T> node, Side side)
        {
            switch (side)
            {
                case Side.Left:
                    return new ThreeNode<T>(_first, _second, node, _middle, _right);

                case Side.Middle:
                    return new ThreeNode<T>(_first, _second, _left, node, _right);

                case Side.Right:
                    return new ThreeNode<T>(_first, _second, _left, _middle, node);

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
