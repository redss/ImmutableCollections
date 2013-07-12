using System.Collections.Generic;
using System.Linq;

namespace ImmutableCollections.Tests.TestInfrastructure
{
    /// <summary>
    /// Base class for all immutable structure testing.
    /// </summary>
    /// <typeparam name="TCollection">Type of tested collection.</typeparam>
    /// <typeparam name="TValue">Type of tested collection.</typeparam>
    public class BaseTests<TCollection, TValue> where TCollection : IImmutableCollection<TValue>
    {
        protected readonly ImmutableCollectionFactory CollectionFactory;

        public BaseTests()
        {
            CollectionFactory = new ImmutableCollectionFactory();
        }

        protected TCollection NewCollection(IEnumerable<TValue> values)
        {
            return NewCollection(values.ToArray());
        }

        protected TCollection NewCollection(params TValue[] values)
        {
            return CollectionFactory.CreateCollection<TCollection, TValue>(values);
        }
    }
}
