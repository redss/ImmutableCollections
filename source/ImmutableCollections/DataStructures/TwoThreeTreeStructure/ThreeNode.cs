using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ImmutableCollections.DataStructures.TwoThreeTreeStructure
{
    class ThreeNode<T> : ITwoThree<T>
    {
        private readonly T[] _values;

        private readonly ITwoThree<T>[] _children;

        // Constructors

        public ThreeNode(T first, T second, ITwoThree<T> left, ITwoThree<T> middle, ITwoThree<T> right)
        {
            _values = new[] { first, second };
            _children = new[] { left, middle, right };

            Debug.Assert(_children.All(c => c is Empty<T>) || _children.All(c => !(c is Empty<T>)));
        }

        public ThreeNode(T[] values, ITwoThree<T>[] children)
        {
            Debug.Assert(values.Length == 2);
            Debug.Assert(children.Length == 3);
            Debug.Assert(children.All(c => c is Empty<T>) || children.All(c => !(c is Empty<T>)));

            _values = values;
            _children = children;
        }

        // ITwoThree

        public ITwoThree<T> Insert(T item, IComparer<T> comparer, out ITwoThree<T> left, out ITwoThree<T> right, out T propagated)
        {
            left = right = null;
            propagated = default(T);

            int index;

            var firstResult = comparer.Compare(item, _values[0]);
            var secondResult = comparer.Compare(item, _values[1]);

            if (firstResult * secondResult == 0)
                return this;

            if (firstResult < 0)
                index = 0;
            else if (secondResult > 0)
                index = 2;
            else
                index = 1;

            T propValue;
            ITwoThree<T> propLeft, propRight;
            var node = _children[index].Insert(item, comparer, out propLeft, out propRight, out propValue);

            if (node != null)
            {
                var newChildren = _children.ToArray();
                newChildren[index] = node;

                return new ThreeNode<T>(_values, newChildren);
            }

            // Split

            switch (index)
            {
                case 0:
                    left = new TwoNode<T>(propValue, propLeft, propRight);
                    right = new TwoNode<T>(_values[1], _children[1], _children[2]);
                    propagated = _values[0];
                    return null;

                case 1:
                    left = new TwoNode<T>(_values[0], _children[0], propLeft);
                    right = new TwoNode<T>(_values[0], propRight, _children[2]);
                    propagated = propValue;
                    return null;

                default:
                    left = new TwoNode<T>(_values[0], _children[0], _children[1]);
                    right = new TwoNode<T>(propValue, propLeft, propRight);
                    propagated = _values[1];
                    return null;
            }
        }
    }
}
