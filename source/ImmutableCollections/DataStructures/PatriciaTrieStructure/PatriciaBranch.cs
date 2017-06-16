using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    /// <summary>
    /// Branch node of the Patricia Trie grouping nodes having same prefix.
    /// </summary>
    /// <typeparam name="T">Type of items associated with keys.</typeparam>
    class PatriciaBranch<T> : IPatriciaNode<T>
    {
        public readonly int Prefix;

        public readonly int Mask;

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public readonly IPatriciaNode<T>[] Children;

        // Constructors

        public PatriciaBranch(int prefix, int mask, IPatriciaNode<T>[] children)
        {
            if (children == null)
                throw new ArgumentNullException(nameof(children), "Patricia branch cannot be empty.");
                
            if (children.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(children), "Patricia branch must have exactly two children.");

            if (children.OfType<EmptyPatriciaTrie<T>>().Any())
                throw new ArgumentException("Patricia branch child cannot be EmptyPatriciaTrie.", nameof(children));

            Prefix = prefix;
            Mask = mask;
            Children = children;
        }

        public PatriciaBranch(int prefix, int mask, IPatriciaNode<T> left, IPatriciaNode<T> right)
            : this(prefix, mask, new[] { left, right }) { }

        // IPatriciaNode

        public T[] Find(int key)
        {
            return PropagationNode(key).Find(key);
        }

        public IPatriciaNode<T> Modify(int key, IPatriciaOperation<T> operation)
        {
            if (!PatriciaHelper.MatchPrefix(key, Prefix, Mask))
            {
                // Key doesn't match the prefix, so we need to create new leaf and join with it.
                var item = operation.OnInsert();

                // Unless operation doesn't create any item.
                if (item == null)
                    return this;

                var leaf = new PatriciaLeaf<T>(key, item);
                return PatriciaHelper.Join(key, leaf, Prefix, this);
            }
            
            var index = PropagationIndex(key);
            var child = Children[index];
            var propagation = child.Modify(key, operation);

            // Child item was removed.
            if (propagation == null)
                return Children[OtherIndex(key)];

            // Child didn't change.
            if (propagation == child)
                return this;

            // Child changed.
            var newChildren = Children.Change(propagation, index);
            return new PatriciaBranch<T>(Prefix, Mask, newChildren);
        }

        public IEnumerable<T> GetItems()
        {
            return Children.SelectMany(c => c.GetItems());
        }

        // Public methods

        public override string ToString()
        {
            return string.Format("Br({0}, {1})", Prefix, Mask);
        }

        // Private methods

        private IPatriciaNode<T> PropagationNode(int key)
        {
            return Children[PropagationIndex(key)];
        }

        private int PropagationIndex(int key)
        {
            return (key & Mask) == 0 ? 0 : 1;
        }

        private int OtherIndex(int key)
        {
            return (key & Mask) == 0 ? 1 : 0;
        }
    }
}
