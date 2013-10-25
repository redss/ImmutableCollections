using System;

namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    /// <summary>
    /// Empty list.
    /// </summary>
    /// <typeparam name="T">Type contained in the list.</typeparam>
    class EmptyList<T> : IListNode<T>
    {
        // Singleton

        public static readonly EmptyList<T> Instance = new EmptyList<T>();

        private EmptyList() { }

        // IListNode

        public T Value
        {
            get { throw new InvalidOperationException(); }
        }

        public IListNode<T> Tail
        {
            get { throw new InvalidOperationException(); }
        }

        public IListNode<T> Prepend(T value)
        {
            return new ListNode<T>(value, this);
        }

        public IListNode<T> Change(T value)
        {
            throw new InvalidOperationException();
        }

        public IListNode<T> Change(IListNode<T> tail)
        {
            throw new InvalidOperationException();
        }

        // Public members

        public override string ToString()
        {
            return "Empty";
        }
    }
}
