using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ImmutableCollections.Tests
{
    [TestFixture]
    public class ImmutableVectorTests : TestInfrastructure.BaseTests<ImmutableVector<int>, int>
    {
        private const int Count = 1000;

        [Test]
        public void EnumerateFrom_Test()
        {
            const int index = 500;

            var elements = Enumerable.Range(0, Count).ToArray();
            var vector = NewCollection(elements);
            
            var newVector = vector.EnumerateFrom(index);
            var skippedElements = elements.Skip(index);

            CollectionAssert.AreEqual(skippedElements, newVector);
        }

        [Test]
        public void AddRange_Test()
        {
            var elements = Enumerable.Range(0, Count).ToArray();
            var vector = NewCollection(elements);

            var newVector = vector.AddRange(elements);
            var newElements = elements.Concat(elements);

            CollectionAssert.AreEqual(newElements, newVector);
        }
    }
}
