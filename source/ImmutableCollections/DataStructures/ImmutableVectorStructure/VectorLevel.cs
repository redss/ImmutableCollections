using System;
using ImmutableCollections.DataStructures.Helpers;

namespace ImmutableCollections.DataStructures.ImmutableVectorStructure
{
    public class VectorLevel<T> : IVectorNode<T>
    {
        private readonly IVectorNode<T>[] _children;

        private readonly int _level;

        public VectorLevel(T elem, int level)
        {
            _level = level;
            _children = new[] { CreateChildNode(elem) };
        }

        public VectorLevel(IVectorNode<T>[] children, int level)
        {
            _level = level;
            _children = children;
        }

        public int Level { get { return _level; } }

        public IVectorNode<T> Append(T elem, int count)
        {
            var length = _children.Length;

            // Maximum capacity reached, this can only occur on top-level (root) node.
            if (length == ImmutableVectorHelper.Fragmentation)
            {
                // Create new top-level node and propagate it.
                var children = new[] { this, CreateChildNode(elem) };
                return new VectorLevel<T>(children, _level + 1);
            }

            // We need to create node with new child on given index.
            var index = ImmutableVectorHelper.CountIndex(count, _level);

            // Extend current children array.
            if (index == length)
                return AppendedNode(elem);

            // Or change it.
            var result = _children[index].Append(elem, count);
            return ChangedNode(result, index);
        }

        public T Nth(int index)
        {
            throw new NotImplementedException();
        }

        private VectorLevel<T> ChangedNode(IVectorNode<T> item, int index)
        {
            var children = _children.Change(item, index);
            return new VectorLevel<T>(children, _level);
        }

        private VectorLevel<T> AppendedNode(IVectorNode<T> item)
        {
            var children = _children.Append(item);
            return new VectorLevel<T>(children, _level);
        }

        private VectorLevel<T> AppendedNode(T elem)
        {
            return AppendedNode(CreateChildNode(elem));
        }

        private IVectorNode<T> CreateChildNode(T elem)
        {
            if (_level == 1)
                return new VectorLeaf<T>(elem);

            return new VectorLevel<T>(elem, _level - 1);
        }

        public override string ToString()
        {
            return string.Format("Level {0}[{1}]", _level, _children.Length);
        }
    }
}
