using System.Collections.Generic;
using FakeItEasy;
using ImmutableCollections.Helpers;
using NUnit.Framework;

namespace ImmutableCollections.Tests.Helpers
{
    class KeyComparerTests
    {
        [Test]
        public void Can_Compare_Only_Keys_With_Default_Comparer()
        {
            var sut = new KeyComparer<int, string>();

            Assert.That(sut.InnerComparer, Is.EqualTo(Comparer<int>.Default));
        }

        [Test]
        public void Can_Compare_Only_Keys_With_Given_Comparer()
        {
            var comparer = A.Fake<IComparer<int>>();
            
            var a = new KeyValuePair<int, int>(100, 200);
            var b = new KeyValuePair<int, int>(300, 400);

            var innerComparerResult = 123;

            A.CallTo(() => comparer.Compare(a.Key, b.Key))
                .Returns(innerComparerResult);

            var sut = new KeyComparer<int, int>(comparer);

            var result = sut.Compare(a, b);

            Assert.That(result, Is.EqualTo(innerComparerResult));
        }
    }
}