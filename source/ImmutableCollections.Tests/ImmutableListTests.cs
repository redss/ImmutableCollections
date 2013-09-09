using System.Linq;
using ImmutableCollections.Tests.TestInfrastructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests
{
    [TestFixture(typeof(CopyImmutableList<int>))]
    [TestFixture(typeof(ImmutableVector<int>))]
    public class ImmutableListTests<TCollection> : BaseTests<TCollection, int> where TCollection : IImmutableList<int>
    {
        private const int Count = 100;

        private const int FirstElement = 20;

        private const int SecondElement = 30;

        [Test]
        public void Add_InsertsElementAtTheTail()
        {
            var elements = Enumerable.Repeat(FirstElement, Count);
            var list = NewCollection(elements);

            var newList = list.Add(SecondElement).ToArray();

            Assert.AreEqual(SecondElement, newList.Last());
            Assert.AreEqual(Count + 1, newList.Count());
            Assert.AreEqual(Count, newList.Count(x => x == FirstElement));
        }

        [Test]
        public void Insert_Test()
        {
            var elements = Enumerable.Repeat(FirstElement, Count);
            var list = NewCollection(elements);

            foreach (var i in Enumerable.Range(0, Count))
            {
                var newList = list.Insert(i, SecondElement).ToArray();

                Assert.AreEqual(SecondElement, newList.ElementAt(i));
                Assert.AreEqual(Count + 1, newList.Count());
                Assert.AreEqual(Count, newList.Count(x => x == FirstElement));
            }
        }

        [Test]
        public void UpdateAt_Test()
        {
            var elements = Enumerable.Repeat(FirstElement, Count);
            var list = NewCollection(elements);

            foreach (var i in Enumerable.Range(0, Count))
            {
                var newList = list.UpdateAt(i, SecondElement);

                Assert.AreEqual(newList[i], newList.ElementAt(i));
                Assert.AreEqual(newList[i], SecondElement);

                var firstElementCount = newList.Count(x => x == FirstElement);
                var secondElementCount = newList.Count(x => x == SecondElement);

                Assert.AreEqual(Count - 1, firstElementCount);
                Assert.AreEqual(1, secondElementCount);
            }
        }

        [Test]
        public void RemoveAt_Test()
        {
            var elements = Enumerable.Range(0, Count).ToArray();
            var list = NewCollection(elements);

            foreach (var i in elements)
            {
                var newList = list.RemoveAt(i).ToArray();

                Assert.False(newList.Contains(i));
                Assert.AreEqual(Count - 1, newList.Count());
            }
        }

        [Test]
        public void Index_Test()
        {
            var elements = Enumerable.Range(0, Count).ToArray();
            var list = NewCollection(elements);

            foreach (var i in elements)
            {
                Assert.AreEqual(i, list[i]);
            }
        }

        [Test]
        public void IndexOf_Test()
        {
            var elements = Enumerable.Range(0, Count).ToArray();
            var list = NewCollection(elements);

            foreach (var i in elements)
            {
                Assert.AreEqual(i, list.IndexOf(i));
            }
        }
    }
}
