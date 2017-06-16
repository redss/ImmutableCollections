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
        // Constructor

        public RedBlackNode(bool isBlack, T value, IRedBlack<T> left, IRedBlack<T> right)
        {
            Debug.Assert(left != null && right != null, "Children cannot be null.");

            IsBlack = isBlack;
            Value = value;
            Left = left;
            Right = right;
        }

        // IRedBlack

        public bool IsBlack { get; }

        public T Value { get; }

        public IRedBlack<T> Left { get; }

        public IRedBlack<T> Right { get; }

        public bool TryFind(T searched, IComparer<T> comparer, out T value)
        {
            Debug.Assert(comparer != null);

            var result = comparer.Compare(searched, Value);

            // Values are equal.
            if (result == 0)
            {
                value = Value;
                return true;
            }

            var node = result < 0 ? Left : Right;
            return node.TryFind(searched, comparer, out value);
        }

        public IRedBlack<T> Insert(T value, IComparer<T> comparer)
        {
            Debug.Assert(comparer != null);

            var result = comparer.Compare(value, Value);

            if (result < 0)
                return Balance(IsBlack, Value, Left.Insert(value, comparer), Right);

            if (result > 0)
                return Balance(IsBlack, Value, Left, Right.Insert(value, comparer));

            return this;
        }

        public IRedBlack<T> Update(T value, IComparer<T> comparer)
        {
            Debug.Assert(comparer != null);

            var result = comparer.Compare(value, Value);

            if (result < 0)
                return Balance(IsBlack, Value, Left.Update(value, comparer), Right);

            if (result > 0)
                return Balance(IsBlack, Value, Left, Right.Update(value, comparer));

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
            return $"{(IsBlack ? "Black" : "Red")}({Value})";
        }

        // Private methods

        private IRedBlack<T> Balance(bool isBlack, T value, IRedBlack<T> left, IRedBlack<T> right)
        {
            if (Value.Equals(value) && Left == left && Right == right)
                return this;

            return RedBlackHelper.Balance(isBlack, value, left, right);
        }
    }
}
