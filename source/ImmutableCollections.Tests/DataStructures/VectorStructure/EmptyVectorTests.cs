using ImmutableCollections.DataStructures.BitmappedVectorTrieStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.VectorStructure
{
    [TestFixture]
    public class EmptyVectorTests
    {
        [Test]
        public void Empty_Vector_Is_Always_On_Zero_Level()
        {
            var sut = CreateSut();

            Assert.That(sut.Level, Is.EqualTo(0));
        }

        [Test]
        public void Append_Returns_New_Vector_Leaf()
        {
            var result = CreateSut().Append(elem: 20, count: 0);

            Assert.That(result, Is.TypeOf<VectorLeaf<int>>());
        }

        [Test]
        public void Update_At_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.UpdateAt(elem: 20, index: 0),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Update_And_Remove_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.UpdateAndRemove(item: 20, index: 0),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Nth_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.Nth(index: 0),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Remove_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.Remove(index: 0),
                Throws.InvalidOperationException);
        }

        private EmptyVector<int> CreateSut()
        {
            return new EmptyVector<int>();
        }
    }
}