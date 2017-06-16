using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.BitmappedVectorTrieStructure
{
    class VectorLevel<T> : IVectorNode<T>
    {
        private readonly IVectorNode<T>[] _children;

        // Constructors

        public VectorLevel(T elem, int level)
        {
            Level = level;

            _children = new[] { CreateChildNode(elem) };
        }

        public VectorLevel(IVectorNode<T>[] children, int level)
        {
            Level = level;

            _children = children;
        }

        // IVectorNode

        public int Level { get; }

        public IEnumerable<T> GetValues()
        {
            return _children.SelectMany(child => child.GetValues());
        }

        public IVectorNode<T> Append(T elem, int count)
        {
            var length = _children.Length;

            // We need to create node with new child on given index.
            var index = CountIndex(count);

            // Maximum capacity reached, this can only occur on top-level (root) node.
            if (count == 1 << ImmutableVectorHelper.Shift * (Level + 1))
            {
                // Create new top-level node and propagate it.
                var children = new IVectorNode<T>[] { this, new VectorLevel<T>(elem, Level) };
                return new VectorLevel<T>(children, Level + 1);
            }
            
            // Extend current children array.
            if (index == length)
                return AppendedNode(elem);

            // Or change it.
            var result = _children[index].Append(elem, count);
            return ChangedNode(result, index);
        }

        public IVectorNode<T> UpdateAt(T elem, int index)
        {
            var nodeIndex = CountIndex(index);
            var newChild = _children[nodeIndex].UpdateAt(elem, index);

            return ChangedNode(newChild, nodeIndex);
        }

        public T Nth(int index)
        {
            var nodeIndex = CountIndex(index);
            return _children[nodeIndex].Nth(index);
        }

        public IVectorNode<T> UpdateAndRemove(T item, int index)
        {
            var nodeIndex = CountIndex(index);
            var node = _children[nodeIndex].UpdateAndRemove(item, index);

            // We shouldn't keep node with one child at the top.
            if (nodeIndex == 0)
                return node;

            var newChildren = _children.TakeAndChange(node, nodeIndex, nodeIndex + 1);
            return new VectorLevel<T>(newChildren, Level);
        }

        public IVectorNode<T> Remove(int index)
        {
            var nodeIndex = CountIndex(index);
            var node = _children[nodeIndex].Remove(index);

            if (nodeIndex == 0)
                return node;
            
            var newChildren = _children.TakeAndChange(node, nodeIndex, nodeIndex + 1);
            return new VectorLevel<T>(newChildren, Level);
        }

        // Object

        public override string ToString()
        {
            return $"Level {Level}[{_children.Length}]";
        }

        // Private methods

        private VectorLevel<T> ChangedNode(IVectorNode<T> item, int index)
        {
            var children = _children.Change(item, index);
            return new VectorLevel<T>(children, Level);
        }

        private VectorLevel<T> AppendedNode(IVectorNode<T> item)
        {
            var children = _children.Append(item);
            return new VectorLevel<T>(children, Level);
        }

        private VectorLevel<T> AppendedNode(T elem)
        {
            return AppendedNode(CreateChildNode(elem));
        }

        private IVectorNode<T> CreateChildNode(T elem)
        {
            if (Level == 1)
                return new VectorLeaf<T>(elem);

            return new VectorLevel<T>(elem, Level - 1);
        }

        private int CountIndex(int index)
        {
            return ImmutableVectorHelper.ComputeIndex(index, Level);
        }
    }
}
