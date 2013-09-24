using System.Collections.Generic;
using System.Linq;
using ImmutableCollections.DataStructures.ImmutableLinkedListStructure;

namespace ImmutableCollections.DataStructures.AssociativeBackendStructure
{
    /// <summary>
    /// Backend for creating sets.
    /// </summary>
    /// <typeparam name="T">Type stored in backend, usually same type as one stored in container.</typeparam>
    class SetBackend<T> : IAssociativeBackend<T>
    {
        private readonly IListNode<T> _list;

        // Constructors

        public SetBackend()
        {
            _list = new EmptyList<T>();
        }

        private SetBackend(IListNode<T> list)
        {
            _list = list;
        }

        // IAccociativeBackend

        public IEnumerable<T> GetValues()
        {
            return _list.GetValues();
        }

        public int Count()
        {
            return _list.Count;
        }

        public IAssociativeBackend<T> Insert(T item)
        {
            var newList = _list.Prepend(item);
            return new SetBackend<T>(newList);
        }

        public IAssociativeBackend<T> Remove(T item)
        {
            var newList = _list.Remove(item);
            return new SetBackend<T>(newList);
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }

        public bool IsSingle()
        {
            return _list.Count == 1;
        }

        // Public methods

        public override string ToString()
        {
            return IsSingle() ? GetValues().First().ToString() : string.Join(" ", GetValues());
        }
    }
}
