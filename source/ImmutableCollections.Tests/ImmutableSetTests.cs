using System;
using NUnit.Framework;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace ImmutableCollections.Tests
{
    [TestFixture(typeof(ImmutableHashSet<string>))]
    [TestFixture(typeof(ImmutableSortedSet<string>))]
    class ImmutableSetTests<TCollection>
        where TCollection : IImmutableSet<string>, new()
    {
        [Test]
        public void AddingNull_ThrowsArgumentNullException()
        {
            var set = new TCollection();
            Assert.Throws<ArgumentNullException>(() => set.Add(null));
        }

        [Test]
        public void RemovingNull_ThrowsArgumentNullException()
        {
            var set = new TCollection();
            Assert.Throws<ArgumentNullException>(() => set.Remove(null));
        }
    }
}
