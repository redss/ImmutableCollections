using System;
using ImmutableCollections.DataStructures.PatriciaTrieStructure;

namespace ImmutableCollections.Tests.DataStructures.PatriciaTrieStructure
{
    class OperationStub<T> : IPatriciaOperation<T>
    {
        private readonly Func<T[], T[]> _onFound;

        private readonly Func<T[]> _onInsert;

        public OperationStub(Func<T[], T[]> onFound, Func<T[]> onInsert = null)
        {
            _onFound = onFound;
            _onInsert = onInsert ?? (() => null);
        }

        public T[] OnFound(T[] items)
        {
            return _onFound(items);
        }

        public T[] OnInsert()
        {
            return _onInsert();
        }
    }
}
