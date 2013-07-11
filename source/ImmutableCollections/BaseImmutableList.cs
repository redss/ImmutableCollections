using System.Collections;
using System.Collections.Generic;

namespace ImmutableCollections
{
    /// <summary>
    /// Base class for immutable lists.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public abstract class BaseImmutableList<T> : IImmutableList<T>
    {
        // IEnumerable

        public abstract IEnumerator<T> GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // IImmutableCollection

        public abstract int Count { get; }

        public abstract bool Contains(T item);

        // IImmutableList

        public abstract IImmutableList<T> Add(T item);

        IImmutableCollection<T> IImmutableCollection<T>.Add(T item)
        {
            return Add(item);
        }

        public abstract IImmutableList<T> Insert(int index, T item);

        public abstract IImmutableList<T> Remove(T item);

        IImmutableCollection<T> IImmutableCollection<T>.Remove(T item)
        {
            return Remove(item);
        }

        public abstract IImmutableList<T> RemoveAt(int index);

        public abstract T this[int index] { get; }

        public abstract int IndexOf(T item);
    }
}
