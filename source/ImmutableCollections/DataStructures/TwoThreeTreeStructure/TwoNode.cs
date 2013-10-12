using System;
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

        public readonly T Value;

        public readonly ITwoThree<T> Left, Right;

        // Constructors

        public TwoNode(T value, ITwoThree<T> left, ITwoThree<T> right)
        {
            Value = value;

            Left = left;
            Right = right;
        }

        // ITwoThree

        public IEnumerable<T> GetValues()
        {
            foreach (var value in Left.GetValues())
                yield return value;

            yield return Value;

            foreach (var value in Right.GetValues())
                yield return value;
        }

        public bool TryFind(T item, IComparer<T> comparer, out T value)
        {
            var side = GetSide(item, comparer);

            if (side == Side.Same)
            {
                value = Value;
                return true;
            }

            return GetChild(side).TryFind(item, comparer, out value);
        }

        public T Min()
        {
            return Left is Empty<T> ? Value : Left.Min();
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
                    ? new ThreeNode<T>(splitValue, Value, splitLeft, splitRight, Right)
                    : new ThreeNode<T>(Value, splitValue, Left, splitLeft, splitRight);
            }

            // Single node was splitValue.

            if (node == child)
                return this;

            return side == Side.Left
                ? new TwoNode<T>(Value, node, Right)
                : new TwoNode<T>(Value, Left, node);
        }

        public ITwoThree<T> Update(T item, IComparer<T> comparer)
        {
            var side = GetSide(item, comparer);

            if (side == Side.Same)
                return item.Equals(Value) ? this : new TwoNode<T>(item, Left, Right);

            var child = GetChild(side);
            var node = child.Update(item, comparer);

            return child == node ? this : node;
        }

        public ITwoThree<T> Remove(T item, IComparer<T> comparer, out bool removed)
        {
            removed = false;
            var side = GetSide(item, comparer);

            if (side == Side.Left)
            {
                bool leftRemoved;
                var newLeft = Left.Remove(item, comparer, out leftRemoved);

                if (newLeft == Left)
                    return this;

                if (!leftRemoved)
                    return new TwoNode<T>(Value, newLeft, Right);

                return RedistributeOrMerge(Value, newLeft, Right, side, out removed);
            }

            if (side == Side.Right)
            {
                bool rightRemoved;
                var newRight = Right.Remove(item, comparer, out rightRemoved);

                if (newRight == Right)
                    return this;

                if (!rightRemoved)
                    return new TwoNode<T>(Value, Left, newRight);

                return RedistributeOrMerge(Value, Left, newRight, side, out removed);
            }

            // Item was found.
            
            if (IsLeaf())
            {
                // Since node is a leaf, we can just return an empty tree.
                removed = true;
                return Empty<T>.Instance;
            }

            // Node is not leaf - we need to find item's consequent.
            var consequent = Right.Min();

            // Since we're going to replace removed value with 
            // the consequent, we must delete it from the subtree.
            bool consRemoved;
            var newCons = Right.Remove(consequent, comparer, out consRemoved);
            
            if (consRemoved)
            {
                // Removing consequent shortened the right subtree,
                // we need to fix it (redistribute or merge nodes).
                return RedistributeOrMerge(consequent, Left, newCons, Side.Right, out removed);
            }

            // Consequent was removed smoothly - we can just propagate changes.
            return new TwoNode<T>(consequent, Left, newCons);
        }

        public bool IsBalanced(out int depth)
        {
            int ld, rd;

            var lv = Left.IsBalanced(out ld);
            var rv = Right.IsBalanced(out rd);

            depth = ld + 1;
            return lv && rv && ld == rd;
        }

        // Public methods

        public override string ToString()
        {
            return string.Format("2-node({0})", Value);
        }

        // Private members

        private Side GetSide(T item, IComparer<T> comparer)
        {
            var result = comparer.Compare(item, Value);

            if (result == 0)
                return Side.Same;

            return result < 0 ? Side.Left : Side.Right;
        }

        private ITwoThree<T> GetChild(Side side)
        {
            Debug.Assert(side != Side.Same);

            return side == Side.Left ? Left : Right;
        }

        private bool IsLeaf()
        {
            return Left is Empty<T> && Right is Empty<T>;
        }

        private ITwoThree<T> RedistributeOrMerge(T value, ITwoThree<T> left, ITwoThree<T> right, Side removedSide, out bool removed)
        {
            removed = false;

            if (removedSide == Side.Right)
            {
                if (left is ThreeNode<T>)
                {
                    // Right redistribute case.

                    var red = (ThreeNode<T>) left;
                    var l = new TwoNode<T>(red.First, red.Left, red.Middle);
                    var r = new TwoNode<T>(value, red.Right, right);

                    return new TwoNode<T>(red.Second, l, r);
                }

                // Right merge case.

                var m = (TwoNode<T>) left;

                removed = true;
                return new ThreeNode<T>(m.Value, value, m.Left, m.Right, right);
            }

            if (removedSide == Side.Left)
            {
                if (right is ThreeNode<T>)
                {
                    // Left redistribute case.

                    var led = (ThreeNode<T>) right;
                    var l = new TwoNode<T>(value, left, led.Left);
                    var r = new TwoNode<T>(led.Second, led.Middle, led.Right);

                    return new TwoNode<T>(led.First, l, r);
                }

                // Left merge case.

                var m = (TwoNode<T>) right;

                removed = true;
                return new ThreeNode<T>(value, m.Value, left, m.Left, m.Right);
            }

            throw new InvalidOperationException();
        }
    }
}
