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
        private enum Side { Left, SameFirst, Middle, SameSecond, Right }

        public readonly T First, Second;

        public readonly ITwoThree<T> Left, Middle, Right; 

        // Constructors

        public ThreeNode(T first, T second, ITwoThree<T> left, ITwoThree<T> middle, ITwoThree<T> right)
        {
            First = first;
            Second = second;

            Left = left;
            Middle = middle;
            Right = right;

            Debug.Assert(left.IsNullOrEmpty() && middle.IsNullOrEmpty() && right.IsNullOrEmpty()
                || !left.IsNullOrEmpty() && !middle.IsNullOrEmpty() && !right.IsNullOrEmpty());
        }

        // ITwoThree

        public IEnumerable<T> GetValues()
        {
            foreach (var value in Left.GetValues())
                yield return value;

            yield return First;

            foreach (var value in Middle.GetValues())
                yield return value;

            yield return Second;

            foreach (var value in Right.GetValues())
                yield return value;
        }

        public bool TryFind(T item, IComparer<T> comparer, out T value)
        {
            var side = GetSide(item, comparer);

            if (IsSame(side))
            {
                value = side == Side.SameFirst ? First : Second;
                return true;
            }

            return GetChild(side).TryFind(item, comparer, out value);
        }

        public T Min()
        {
            return Left is Empty<T> ? First : Left.Min();
        }

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> splitLeft, out ITwoThree<T> splitRight, out T splitValue)
        {
            // Default split values.
            splitLeft = splitRight = null;
            splitValue = default(T);

            // Get side to insert node.
            var side = GetSide(item, comparer);

            // Values are equal, no need to change tree.
            if (IsSame(side))
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
                    splitRight = new TwoNode<T>(Second, Middle, Right);
                    splitValue = First;
                    break;

                case Side.Middle:
                    splitLeft = new TwoNode<T>(First, Left, pl);
                    splitRight = new TwoNode<T>(Second, pr, Right);
                    splitValue = pv;
                    break;

                case Side.Right:
                    splitLeft = new TwoNode<T>(First, Left, Middle);
                    splitRight = new TwoNode<T>(pv, pl, pr);
                    splitValue = Second;
                    break;
            }

            return null;
        }

        public ITwoThree<T> Update(T item, IComparer<T> comparer)
        {
            var side = GetSide(item, comparer);

            if (side == Side.SameFirst)
                return item.Equals(First) ? this : new ThreeNode<T>(item, Second, Left, Middle, Right);

            if (side == Side.SameSecond)
                return item.Equals(Second) ? this : new ThreeNode<T>(First, item, Left, Middle, Right);

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
                    return new ThreeNode<T>(First, Second, newLeft, Middle, Right);

                return Redistribute(First, Second, newLeft, Middle, Right, side, out removed);
            }

            if (side == Side.Middle)
            {
                bool middleRemoved;
                var newMiddle = Middle.Remove(item, comparer, out middleRemoved);

                if (newMiddle == Middle)
                    return this;

                if (!middleRemoved)
                    return new ThreeNode<T>(First, Second, Left, newMiddle, Right);

                return Redistribute(First, Second, Left, newMiddle, Right, side, out removed);
            }

            if (side == Side.Right)
            {
                bool rightRemoved;
                var newRight = Right.Remove(item, comparer, out rightRemoved);

                if (newRight == Right)
                    return this;

                if (!rightRemoved)
                    return new ThreeNode<T>(First, Second, Left, Middle, newRight);

                return Redistribute(First, Second, Left, Middle, newRight, side, out removed);
            }

            if (IsLeaf())
            {
                if (side == Side.SameFirst)
                {
                    return new TwoNode<T>(Second, Empty<T>.Instance, Empty<T>.Instance);
                }

                if (side == Side.SameSecond)
                {
                    return new TwoNode<T>(First, Empty<T>.Instance, Empty<T>.Instance);
                }
            }

            if (side == Side.SameFirst)
            {
                var cons = Middle.Min();

                bool consRemoved;
                var newCons = Middle.Remove(cons, comparer, out consRemoved);

                if (consRemoved)
                    return Redistribute(cons, Second, Left, newCons, Right, Side.Middle, out removed);

                return new ThreeNode<T>(cons, Second, Left, newCons, Right);
            }

            if (side == Side.SameSecond)
            {
                var cons = Right.Min();

                bool consRemoved;
                var newCons = Right.Remove(cons, comparer, out consRemoved);

                if (consRemoved)
                    return Redistribute(First, cons, Left, Middle, newCons, Side.Right, out removed);

                return new ThreeNode<T>(First, cons, Left, Middle, newCons);
            }

            throw new InvalidOperationException();
        }

        // Private methods

        private Side GetSide(T item, IComparer<T> comparer)
        {
            var firstResult = comparer.Compare(item, First);
            var secondResult = comparer.Compare(item, Second);

            if (firstResult == 0)
                return Side.SameFirst;

            if (secondResult == 0)
                return Side.SameSecond;

            if (firstResult < 0)
                return Side.Left;
            
            if (secondResult > 0)
                return Side.Right;
            
            return Side.Middle;
        }

        private static bool IsSame(Side side)
        {
            return side == Side.SameFirst || side == Side.SameSecond;
        }

        private ITwoThree<T> GetChild(Side side)
        {
            switch (side)
            {
                case Side.Left:
                    return Left;

                case Side.Middle:
                    return Middle;

                case Side.Right:
                    return Right;

                default:
                    throw new InvalidOperationException();
            }
        }

        private ITwoThree<T> NewChangedNode(ITwoThree<T> node, Side side)
        {
            switch (side)
            {
                case Side.Left:
                    return new ThreeNode<T>(First, Second, node, Middle, Right);

                case Side.Middle:
                    return new ThreeNode<T>(First, Second, Left, node, Right);

                case Side.Right:
                    return new ThreeNode<T>(First, Second, Left, Middle, node);

                default:
                    throw new InvalidOperationException();
            }
        }

        private bool IsLeaf()
        {
            return Left is Empty<T> && Right is Empty<T>;
        }

        private ITwoThree<T> Redistribute(T first, T second, ITwoThree<T> left, 
            ITwoThree<T> middle, ITwoThree<T> right, Side side, out bool removed)
        {
            removed = false;

            // TODO

            return null;
        }
    }
}
