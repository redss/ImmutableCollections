using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmutableCollections.Tests.TestInfrastructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests
{
    [TestFixture(typeof(ImmutableHashSet<int>))]
    public class ImmutableSetTests<TCollection> : BaseTests<TCollection, int> where TCollection : IImmutableSet<int>
    {
        [Test]
        public void ExceptWith_Test()
        {
            var elements = Enumerable.Range(0, 10).ToArray();
            var except = elements.Take(5).ToArray();
            var collection = NewCollection(elements);

            var newCollection = collection.ExceptWith(except);

            CollectionAssert.AreEquivalent(elements.Except(except), newCollection);
        }

        [Test]
        public void IntersectWith_Test()
        {
            var first = Enumerable.Range(0, 10).ToArray();
            var second = Enumerable.Range(5, 10).ToArray();
            var collection = NewCollection(first);

            var newCollection = collection.IntersectWith(second);

            CollectionAssert.AreEquivalent(first.Intersect(second), newCollection);
        }

        [Test]
        public void SymetricExceptionWith_Test()
        {
            var first = Enumerable.Range(0, 10).ToArray();
            var second = Enumerable.Range(5, 10).ToArray();
            var collection = NewCollection(first);

            var newCollection = collection.SymmetricExceptWith(second);
            var symetricException = first.Union(second).Except(first.Intersect(second));

            CollectionAssert.AreEquivalent(symetricException, newCollection);
        }

        [Test]
        public void UnionWith_Test()
        {
            var first = Enumerable.Range(0, 10).ToArray();
            var second = Enumerable.Range(5, 10).ToArray();
            var collection = NewCollection(first);

            var newCollection = collection.UnionWith(second);
            var union = first.Union(second);

            CollectionAssert.AreEquivalent(union, newCollection);
        }
    }
}
