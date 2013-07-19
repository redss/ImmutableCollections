using System.Linq;
using ImmutableCollections.Tests.TestInfrastructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests
{
    [TestFixture(typeof(CopyImmutableList<int>))]
    public class ImmutableListTestses<TCollection> : BaseTests<TCollection, int> where TCollection : IImmutableList<int>
    {
        [Test]
        public void Add_InsertsElementAtTheTail()
        {
            int count = 10, element = 20, added = 30;
            var elements = Enumerable.Repeat(element, count);
            var list = NewCollection(elements);

            var newList = list.Add(added).ToArray();

            Assert.AreEqual(added, newList.Last());
            Assert.AreEqual(count + 1, newList.Count());
            Assert.AreEqual(count, newList.Count(x => x == element));
        }

        [Test]
        public void Insert_Test()
        {
            int count = 10, element = 20, inserted = 30;
            var elements = Enumerable.Repeat(element, count);
            var list = NewCollection(elements);

            foreach (var i in Enumerable.Range(0, count))
            {
                var newList = list.Insert(i, inserted).ToArray();

                Assert.AreEqual(inserted, newList.ElementAt(i));
                Assert.AreEqual(count + 1, newList.Count());
                Assert.AreEqual(count, newList.Count(x => x == element));
            }
        }

        [Test]
        public void RemoveAt_Test()
        {
            var count = 10;
            var elements = Enumerable.Range(0, count).ToArray();
            var list = NewCollection(elements);

            foreach (var i in elements)
            {
                var newList = list.RemoveAt(i).ToArray();

                Assert.False(newList.Contains(i));
                Assert.AreEqual(count - 1, newList.Count());
            }
        }

        [Test]
        public void Index_Test()
        {
            var count = 10;
            var elements = Enumerable.Range(0, count).ToArray();
            var list = NewCollection(elements);

            foreach (var i in elements)
            {
                Assert.AreEqual(i, list[i]);
            }
        }

        [Test]
        public void IndexOf_Test()
        {
            var count = 10;
            var elements = Enumerable.Range(0, count).ToArray();
            var list = NewCollection(elements);

            foreach (var i in elements)
            {
                Assert.AreEqual(i, list.IndexOf(i));
            }
        }
    }
}
