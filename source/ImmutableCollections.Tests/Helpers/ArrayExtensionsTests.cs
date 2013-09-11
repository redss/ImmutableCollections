using System;
using System.Linq;
using NUnit.Framework;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.Tests.Helpers
{
    [TestFixture]
    public class ArrayExtensionsTests
    {
        private const int Count = 10;

        private const int Foo = 20;

        private const int Index = 4;

        [Test]
        public void Append_Test()
        {
            var values = Enumerable.Range(0, Count).ToArray();
            var arr = values.ToArray();

            var appended = arr.Append(Foo);

            CollectionAssert.AreEqual(values, arr);
            CollectionAssert.AreEqual(values, appended.Take(Count));
            Assert.AreEqual(Count + 1, appended.Length);
            Assert.AreEqual(Foo, appended[Count]);
        }

        [Test]
        public void Change_Test()
        {
            var values = Enumerable.Range(0, Count).ToArray();
            var arr = values.ToArray();

            var changed = arr.Change(Foo, Index);
            
            CollectionAssert.AreEqual(values, arr);
            Assert.AreEqual(Count, changed.Length);
            Assert.AreEqual(Foo, changed[Index]);

            foreach (var i in values.Where(x => x != Index))
                Assert.AreEqual(arr[i], changed[i]);
        }

        [Test]
        public void TakeAndChange_Test()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Take_Test()
        {
            throw new NotImplementedException();
        }
    }
}
