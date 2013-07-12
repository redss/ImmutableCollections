using System;
using System.Collections.Generic;

namespace ImmutableCollections.Tests.TestInfrastructure
{
    /// <summary>
    /// Abstracts the creation of new collection objects (as it may change during developement).
    /// </summary>
    public interface IImmutableCollectionFactory
    {
        TCollection CreateCollection<TCollection, TValue>(IEnumerable<TValue> values) where TCollection : IImmutableCollection<TValue>;
    }

    public class ImmutableCollectionFactory : IImmutableCollectionFactory
    {
        public TCollection CreateCollection<TCollection, TValue>(IEnumerable<TValue> values) where TCollection : IImmutableCollection<TValue>
        {
            IImmutableCollection<TValue> colleciton = Activator.CreateInstance<TCollection>();

            foreach (var value in values)
                colleciton = colleciton.Add(value);

            return (TCollection) colleciton;
        }
    }
}
