using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmutableCollections.Tests.TestInfrastructure
{
    /// <summary>
    /// Abstracts the creation of new collection objects (as it may change during developement).
    /// </summary>
    interface IImmutableCollectionFactory
    {
        TCollection CreateCollection<TCollection, TValue>(IEnumerable<TValue> values) where TCollection : IImmutableCollection<TValue>;
    }

    class ImmutableCollectionFactory : IImmutableCollectionFactory
    {
        public TCollection CreateCollection<TCollection, TValue>(IEnumerable<TValue> values) where TCollection : IImmutableCollection<TValue>
        {
            IImmutableCollection<TValue> colleciton = Activator.CreateInstance<TCollection>();
            return (TCollection) values.Aggregate(colleciton, (current, value) => current.Add(value));
        }
    }
}
