using System.Collections.Generic;

namespace ImmutableCollections.DataStructures.ImmutableLinkedListStructure
{
    interface IListNode<T>
    {
        IEnumerable<T> GetValues();

        int Count { get; }

        IListNode<T> Prepend(T item);

        IListNode<T> Append(T item);

        IListNode<T> Insert(int index, T item);

        IListNode<T> UpdateAt(int index, T item);

        IListNode<T> Remove(T item);
        
        IListNode<T> RemoveAt(int index);

        bool Contains(T item);

        int IndexOf(T item, int counter = 0);

        T ElementAt(int index);
    }
}
