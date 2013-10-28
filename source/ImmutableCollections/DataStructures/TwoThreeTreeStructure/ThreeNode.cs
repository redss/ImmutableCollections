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
            return Left is EmptyTwoThree<T> ? First : Left.Min();
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
            var side = GetSide(item, comparer);

            // Element was not found, continue search.
            if (IsSide(side))
                return RemoveFromSide(item, comparer, side, out removed);

            // Element was found.

            // We are at leaf - just shrink to 2-node.
            if (IsLeaf())
                return Shrink(side, out removed);

            // We aren't at leaf - find removed value consequent.
            return RemoveElement(comparer, side, out removed);
        }

        public bool IsBalanced(out int depth)
        {
            int ld, md, rd;

            var lv = Left.IsBalanced(out ld);
            var mv = Middle.IsBalanced(out md);
            var rv = Right.IsBalanced(out rd);

            depth = ld + 1;
            return lv && mv && rv && (ld == rd) && (ld == md);
        }

        // Public methods

        public override string ToString()
        {
            return string.Format("3-node({0}, {1})", First, Second);
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

        private Side GetConsequentSide(Side side)
        {
            switch (side)
            {
                case Side.SameFirst:
                    return Side.Middle;
                    
                case Side.SameSecond:
                    return Side.Right;

                default:
                    throw new ArgumentOutOfRangeException("side");
            }
        }

        private static bool IsSame(Side side)
        {
            return side == Side.SameFirst || side == Side.SameSecond;
        }

        private static bool IsSide(Side side)
        {
            return side == Side.Left || side == Side.Middle || side == Side.Right;
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

        private ITwoThree<T> NewChangedWithConsequent(T value, ITwoThree<T> node, Side side)
        {
            switch (side)
            {
                case Side.SameFirst:
                    return new ThreeNode<T>(value, Second, Left, node, Right);

                case Side.SameSecond:
                    return new ThreeNode<T>(First, value, Left, Middle, node);

                default:
                    throw new ArgumentOutOfRangeException("side");
            }
        }

        private bool IsLeaf()
        {
            return Left is EmptyTwoThree<T> && Right is EmptyTwoThree<T>;
        }

        private TwoNode<T> Shrink(Side side, out bool removed)
        {
            removed = false;

            switch (side)
            {
                case Side.SameFirst:
                    return new TwoNode<T>(Second, EmptyTwoThree<T>.Instance, EmptyTwoThree<T>.Instance);

                case Side.SameSecond:
                    return new TwoNode<T>(First, EmptyTwoThree<T>.Instance, EmptyTwoThree<T>.Instance);

                default:
                    throw new ArgumentOutOfRangeException("side");
            }
        }

        private ITwoThree<T> RemoveFromSide(T item, IComparer<T> comparer, Side side, out bool removed)
        {
            removed = false;

            var child = GetChild(side);

            // Remove item from child.
            bool childRemoved;
            var newChild = child.Remove(item, comparer, out childRemoved);

            // Element was not found and nothing was removed.
            if (newChild == child)
                return this;

            // Child subtree was not shortened, so we just propagate change.
            if (!childRemoved)
                return NewChangedNode(newChild, side);

            // Child subtree was shortened, we need to redistribute nodes.
            return RedistributeFrom(newChild, side, out removed);
        }

        private ITwoThree<T> RemoveElement(IComparer<T> comparer, Side side, out bool removed)
        {
            Debug.Assert(side == Side.SameFirst || side == Side.SameSecond);

            var consequentSide = GetConsequentSide(side);
            var child = GetChild(consequentSide);

            var consequent = GetChild(consequentSide).Min();

            bool consequentRemoved;
            var newChild = child.Remove(consequent, comparer, out consequentRemoved);

            if (!consequentRemoved)
            {
                removed = false;
                return NewChangedWithConsequent(consequent, newChild, side);
            }

            return RedistributeCons(consequent, newChild, side, out removed);
        }

        private ITwoThree<T> RedistributeFrom(ITwoThree<T> changed, Side side, out bool removed)
        {
            switch (side)
            {
                case Side.Left:
                    return Redistribute(First, Second, changed, Middle, Right, side, out removed);

                case Side.Middle:
                    return Redistribute(First, Second, Left, changed, Right, side, out removed);

                case Side.Right:
                    return Redistribute(First, Second, Left, Middle, changed, side, out removed);

                default:
                    throw new ArgumentOutOfRangeException("side");
            }
        }

        private ITwoThree<T> RedistributeCons(T value, ITwoThree<T> changed, Side side, out bool removed)
        {
            switch (side)
            {
                case Side.SameFirst:
                    return Redistribute(value, Second, Left, changed, Right, Side.Middle, out removed);

                case Side.SameSecond:
                    return Redistribute(First, value, Left, Middle, changed, Side.Right, out removed);

                default:
                    throw new ArgumentOutOfRangeException("side");
            }
        }

        private static ITwoThree<T> Redistribute(T first, T second, ITwoThree<T> left, 
            ITwoThree<T> middle, ITwoThree<T> right, Side side, out bool removed)
        {
            removed = false;

            if (side == Side.Left)
                return RedistributeLeft(first, second, left, middle, right);

            if (side == Side.Middle)
                return RedistributeMiddle(first, second, left, middle, right);

            if (side == Side.Right)
                return RedistributeRight(first, second, left, middle, right);
            
            throw new ArgumentOutOfRangeException("side");
        }

        private static ITwoThree<T> RedistributeLeft(T first, T second, ITwoThree<T> left, ITwoThree<T> middle, ITwoThree<T> right)
        {
            // Case A
            if (middle is TwoNode<T> && right is TwoNode<T>)
            {
                var sm = (TwoNode<T>) middle;

                var l = new ThreeNode<T>(first, sm.Value, left, sm.Left, sm.Right);
                var r = right;

                return new TwoNode<T>(second, l, r);
            }

            // Case B
            if (middle is ThreeNode<T>)
            {
                var sm = (ThreeNode<T>) middle;

                var l = new TwoNode<T>(first, left, sm.Left);
                var m = new TwoNode<T>(sm.Second, sm.Middle, sm.Right);
                var r = right;

                return new ThreeNode<T>(sm.First, second, l, m, r);
            }

            // Case C
            if (middle is TwoNode<T> && right is ThreeNode<T>)
            {
                var sm = (TwoNode<T>) middle;
                var sr = (ThreeNode<T>) right;

                var l = new TwoNode<T>(first, left, sm.Left);
                var m = new TwoNode<T>(second, sm.Right, sr.Left);
                var r = new TwoNode<T>(sr.Second, sr.Middle, sr.Right);

                return new ThreeNode<T>(sm.Value, sr.First, l, m, r);
            }

            throw new InvalidOperationException();
        }

        private static ITwoThree<T> RedistributeMiddle(T first, T second, ITwoThree<T> left, ITwoThree<T> middle, ITwoThree<T> right)
        {
            // Case A
            if (left is TwoNode<T> && right is TwoNode<T>)
            {
                var sl = (TwoNode<T>) left;

                var l = new ThreeNode<T>(sl.Value, first, sl.Left, sl.Right, middle);
                var r = right;

                return new TwoNode<T>(second, l, r);
            }

            // Case B
            if (right is ThreeNode<T>)
            {
                var sr = (ThreeNode<T>) right;

                var l = left;
                var m = new TwoNode<T>(second, middle, sr.Left);
                var r = new TwoNode<T>(sr.Second, sr.Middle, sr.Right);

                return new ThreeNode<T>(first, sr.First, l, m, r);
            }

            // Case C
            if (left is ThreeNode<T> && right is TwoNode<T>)
            {
                var sl = (ThreeNode<T>) left;

                var l = new TwoNode<T>(sl.First, sl.Left, sl.Middle);
                var m = new TwoNode<T>(first, sl.Right, middle);
                var r = right;

                return new ThreeNode<T>(sl.Second, second, l, m, r);
            }

            throw new InvalidOperationException();
        }

        private static ITwoThree<T> RedistributeRight(T first, T second, ITwoThree<T> left, ITwoThree<T> middle, ITwoThree<T> right)
        {
            // Case A
            if (left is TwoNode<T> && middle is TwoNode<T>)
            {
                var sm = (TwoNode<T>) middle;

                var l = left;
                var r = new ThreeNode<T>(sm.Value, second, sm.Left, sm.Right, right);

                return new TwoNode<T>(first, l, r);
            }

            // Case B
            if (middle is ThreeNode<T>)
            {
                var sm = (ThreeNode<T>) middle;

                var l = left;
                var m = new TwoNode<T>(sm.First, sm.Left, sm.Middle);
                var r = new TwoNode<T>(second, sm.Right, right);

                return new ThreeNode<T>(first, sm.Second, l, m, r);
            }

            // Case C
            if (left is ThreeNode<T> && middle is TwoNode<T>)
            {
                var sl = (ThreeNode<T>) left;
                var sm = (TwoNode<T>) middle;

                var l = new TwoNode<T>(sl.First, sl.Left, sl.Middle);
                var m = new TwoNode<T>(first, sl.Right, sm.Left);
                var r = new TwoNode<T>(second, sm.Right, right);

                return new ThreeNode<T>(sl.Second, sm.Value, l, m, r);
            }

            throw new InvalidOperationException();
        }
    }
}
