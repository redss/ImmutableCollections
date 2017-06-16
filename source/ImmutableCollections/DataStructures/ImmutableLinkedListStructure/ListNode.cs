namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    /// <summary>
    /// Concrete node, i. e. list having at least one element.
    /// </summary>
    /// <typeparam name="T">Type contained in the list.</typeparam>
    class ListNode<T> : IListNode<T>
    {
        // Constructor

        public ListNode(T value, IListNode<T> tail)
        {
            Value = value;
            Tail = tail;
        }

        // IListNode

        public T Value { get; }

        public IListNode<T> Tail { get; }

        public IListNode<T> Prepend(T value)
        {
            return new ListNode<T>(value, this);
        }

        public IListNode<T> ChangeValue(T value)
        {
            return new ListNode<T>(value, Tail);
        }

        public IListNode<T> ChangeTail(IListNode<T> tail)
        {
            return new ListNode<T>(Value, tail);
        }

        // Public members

        public override string ToString()
        {
            return $"Node({Value})";
        }
    }
}
