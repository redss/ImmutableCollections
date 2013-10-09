using System.Collections.Generic;
using System.Diagnostics;

namespace ImmutableCollections.DataStructures.RedBlackTreeStructure
{
    /// <summary>
    /// Red Black Tree node, containing children and value.
    /// </summary>
    /// <typeparam name="T">Type of values stored in the tree.</typeparam>
    class RedBlackNode<T> : IRedBlack<T>
    {
        private readonly bool _isBlack;

        private readonly T _value;

        private readonly IRedBlack<T> _left, _right;

        // Constructor

        public RedBlackNode(bool isBlack, T value, IRedBlack<T> left, IRedBlack<T> right)
        {
            Debug.Assert(left != null && right != null, "Children cannot be null.");

            _isBlack = isBlack;
            _value = value;
            _left = left;
            _right = right;
        }

        // IRedBlack

        public bool IsBlack { get { return _isBlack; } }

        public T Value
        {
            get { return _value; }
        }

        public IRedBlack<T> Left { get { return _left; } }

        public IRedBlack<T> Right { get { return _right; } }

        public bool TryFind(T searched, IComparer<T> comparer, out T value)
        {
            Debug.Assert(comparer != null);

            var result = comparer.Compare(searched, _value);

            // Values are equal.
            if (result == 0)
            {
                value = _value;
                return true;
            }

            var node = result < 0 ? Left : Right;
            return node.TryFind(searched, comparer, out value);
        }

        public IRedBlack<T> Update(T value, IComparer<T> comparer)
        {
            Debug.Assert(comparer != null);

            var result = comparer.Compare(value, Value);

            if (result < 0)
                return Balance(IsBlack, _value, Left.Update(value, comparer), Right);

            if (result > 0)
                return Balance(IsBlack, _value, Left, Right.Update(value, comparer));

            return new RedBlackNode<T>(IsBlack, value, Left, Right);
        }

        public IEnumerable<T> GetValues()
        {
            foreach (var v in Left.GetValues())
                yield return v;

            yield return Value;

            foreach (var v in Right.GetValues())
                yield return v;
        }

        public int Validate(int blackNodes)
        {
            if (!IsBlack && (!Left.IsBlack || !Right.IsBlack))
                throw new InvalidRedBlackTreeException("Red node cannot have any red children.");

            var result = Left.Validate(blackNodes);

            if (result != Right.Validate(blackNodes))
                throw new InvalidRedBlackTreeException("Inconsistent number of black nodes from this node to the leafs.");

            return IsBlack ? result + 1 : result;
        }

        // Public methods

        public override string ToString()
        {
            return string.Format("{0}({1})", IsBlack ? "Black" : "Red", Value);
        }

        // Private methods

        private static IRedBlack<T> Balance(bool isBlack, T value, IRedBlack<T> left, IRedBlack<T> right)
        {
            return RedBlackHelper.Balance(isBlack, value, left, right);
        }
    }
}
