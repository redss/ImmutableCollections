using System.Linq;
using NUnit.Framework;
using ImmutableCollections.Tests.TestInfrastructure;

namespace ImmutableCollections.Tests
{
    [TestFixture(typeof(ImmutableCopyList<int>))]
    [TestFixture(typeof(ImmutableVector<int>))]
    [TestFixture(typeof(ImmutableLinkedList<int>))]
    [TestFixture(typeof(ImmutableHashSet<int>))]
    [TestFixture(typeof(ImmutableSortedSet<int>))]
    [TestFixture(typeof(ImmutableCopyHashSet<int>))]
    public class ImmutableCollectionTests<TCollection> : BaseTests<TCollection, int> where TCollection : IImmutableCollection<int>
    {
        [Test]
        public void Add_Test()
        {
            var collection = NewCollection();
            var modified = collection.Add(10);

            Assert.False(collection.AsEnumerable().Contains(10));
            Assert.True(modified.AsEnumerable().Contains(10));
        }

        [Test]
        public void Remove_Test()
        {
            var collection = NewCollection(10, 20);
            var modified = collection.Remove(10);

            Assert.False(modified.AsEnumerable().Contains(10));
            Assert.True(collection.AsEnumerable().Contains(10));
        }

        [Test]
        public void Remove_RemovesOnlyOneElement()
        {
            const int count = 10;
            var items = Enumerable.Range(0, count).ToArray();

            var collection = NewCollection(items);
            var modified = collection.Remove(items.Last()).ToArray();
           
            Assert.AreEqual(count - 1, modified.Count());
            CollectionAssert.IsSubsetOf(modified, items);
        }

        [Test]
        public void Contains_Test()
        {
            var collection = NewCollection(10, 20, 30);
            var result = collection.Contains(10);

            Assert.True(result);
        }

        [Test]
        public void Count_Test()
        {
            var collection = NewCollection(Enumerable.Range(1, 20));

            Assert.AreEqual(20, collection.Length);
        }
    }
}
