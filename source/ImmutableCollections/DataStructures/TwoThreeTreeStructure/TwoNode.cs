using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    class TwoNode<T> : ITwoThree<T>
    {
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

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> left, out ITwoThree<T> right, out T propagated)
        {
            var result = comparer.Compare(item, _value);

            if (result == 0)
            {
                left = null;
                right = null;
                propagated = default(T);

                return this;
            }

            var child = result < 0 ? _left : _right;
            var node = child.Insert(item, comparer, out left, out right, out propagated);

            if (node == null)
            {
                // Child node split and propagated value.
                return result < 0
                    ? new ThreeNode<T>(propagated, _value, left, right, _right)
                    : new ThreeNode<T>(_value, propagated, _left, left, right);
            }

            // Single node was propagated.
            return result < 0
                ? new TwoNode<T>(_value, node, _right)
                : new TwoNode<T>(_value, _left, node);
        }
    }
}
