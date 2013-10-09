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

        public IEnumerable<T> GetValues()
        {
            foreach (var value in _left.GetValues())
                yield return value;

            yield return _value;

            foreach (var value in _right.GetValues())
                yield return value;
        }

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> splitLeft, out ITwoThree<T> splitRight, out T splitValue)
        {
            var result = comparer.Compare(item, _value);

            if (result == 0)
            {
                splitLeft = null;
                splitRight = null;
                splitValue = default(T);

                return this;
            }

            var child = result < 0 ? _left : _right;
            var node = child.Insert(item, comparer, out splitLeft, out splitRight, out splitValue);

            if (node == null)
            {
                // Child node split and splitValue value.
                return result < 0
                    ? new ThreeNode<T>(splitValue, _value, splitLeft, splitRight, _right)
                    : new ThreeNode<T>(_value, splitValue, _left, splitLeft, splitRight);
            }

            // Single node was splitValue.
            return result < 0
                ? new TwoNode<T>(_value, node, _right)
                : new TwoNode<T>(_value, _left, node);
        }
    }
}
