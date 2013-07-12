using ImmutableCollections.Tests.TestInfrastructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests
{
    [TestFixture(typeof(CopyImmutableList<int>))]
    public class ImmutableListTestses<TCollection> : BaseTests<TCollection, int> where TCollection : IImmutableList<int>
    {
    }
}
