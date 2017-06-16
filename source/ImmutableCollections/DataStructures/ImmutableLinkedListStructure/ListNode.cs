namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    /// <summary>
    /// Concrete node, i. e. list having at least one element.
    /// </summary>
    /// <typeparam name="T">Type contained in the list.</typeparam>
    class ListNode<T> : IListNode<T>
    {
        private readonly T _value;

        private readonly IListNode<T> _tail;

        // Constructor

        public ListNode(T value, IListNode<T> tail)
        {
            _value = value;
            _tail = tail;
        }

        // IListNode

        public T Value
        {
            get { return _value; }
        }

        public IListNode<T> Tail
        {
            get { return _tail; }
        }

        public IListNode<T> Prepend(T value)
        {
            return new ListNode<T>(value, this);
        }

        public IListNode<T> ChangeValue(T value)
        {
            return new ListNode<T>(value, _tail);
        }

        public IListNode<T> ChangeTail(IListNode<T> tail)
        {
            return new ListNode<T>(Value, tail);
        }

        // Public members

        public override string ToString()
        {
            return $"Node({_value})";
        }
    }
}
