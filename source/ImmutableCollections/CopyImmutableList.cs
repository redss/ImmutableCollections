using System.Collections.Generic;

namespace ImmutableCollections
{
    public class CopyImmutableList<T> : BaseImmutableList<T>
    {
        private readonly List<T> _list;

        public CopyImmutableList(List<T> list = null)
        {
            _list = list ?? new List<T>();
        }

        // IEnumerable

        public override IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        // IImmutableCollection

        public override int IndexOf(T item)
        {
            return _list.IndexOf(item);
        }

        public override int Count
        {
            get { return _list.Count; }
        }

        // IImmutableList

        public override IImmutableList<T> Add(T item)
        {
            var newList = new List<T>(_list) {item};

            return new CopyImmutableList<T>(newList);
        }

        public override IImmutableList<T> Insert(int index, T item)
        {
            var newList = new List<T>(_list);
            newList.Insert(index, item);

            return new CopyImmutableList<T>(newList);
        }

        public override IImmutableList<T> Remove(T item)
        {
            var newList = new List<T>(_list);
            newList.Remove(item);

            return new CopyImmutableList<T>(newList);
        }

        public override IImmutableList<T> RemoveAt(int index)
        {
            var newList = new List<T>(_list);
            newList.RemoveAt(index);

            return new CopyImmutableList<T>(newList);
        }

        public override T this[int index]
        {
            get { return _list[index]; }
        }

        public override bool Contains(T item)
        {
            return _list.Contains(item);
        }
    }
}
