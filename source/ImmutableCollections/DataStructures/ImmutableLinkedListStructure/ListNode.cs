using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    class ListNode<T> : IListNode<T>
    {
        private readonly T _value;

        private readonly IListNode<T> _tail;

        public ListNode(T value, IListNode<T> tail)
        {
            _value = value;
            _tail = tail;
        }

        public IEnumerable<T> GetValues()
        {
            yield return _value;

            foreach (var val in _tail.GetValues())
                yield return val;
        }

        public int Count { get { return _tail.Count + 1; } }
        
        public IListNode<T> Prepend(T item)
        {
            return new ListNode<T>(item, this);
        }

        public IListNode<T> Append(T item)
        {
            var newTail = _tail.Append(item);
            return new ListNode<T>(_value, newTail);
        }

        public IListNode<T> Insert(int index, T item)
        {
            if (index == 0)
                return Prepend(item);

            var newTail = _tail.Insert(index - 1, item);
            return new ListNode<T>(_value, newTail);
        }

        public IListNode<T> UpdateAt(int index, T item)
        {
            if (index == 0)
                return new ListNode<T>(item, _tail);

            var newTail = _tail.UpdateAt(index - 1, item);
            return new ListNode<T>(_value, newTail);
        }

        public IListNode<T> Remove(T item)
        {
            if (_value.Equals(item))
                return _tail;

            var newTail = _tail.Remove(item);
            return new ListNode<T>(_value, newTail);
        }

        public IListNode<T> RemoveAt(int index)
        {
            if (index == 0)
                return _tail;

            var newTail = _tail.RemoveAt(index - 1);
            return new ListNode<T>(_value, newTail);
        }

        public bool Contains(T item)
        {
            return _value.Equals(item) || _tail.Contains(item);
        }

        public int IndexOf(T item, int counter)
        {
            return _value.Equals(item) ? counter : _tail.IndexOf(item, counter + 1);
        }

        public T ElementAt(int index)
        {
            return index == 0 ? _value : _tail.ElementAt(index - 1);
        }
    }
}
