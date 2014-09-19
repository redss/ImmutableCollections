using System;

namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    /// <summary>
    /// Empty list.
    /// </summary>
    /// <typeparam name="T">Type contained in the list.</typeparam>
    class EmptyList<T> : IListNode<T>
    {
        public static readonly EmptyList<T> Instance = new EmptyList<T>();

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

        public IListNode<T> ChangeValue(T value)
        {
            throw new InvalidOperationException();
        }

        public IListNode<T> ChangeTail(IListNode<T> tail)
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
