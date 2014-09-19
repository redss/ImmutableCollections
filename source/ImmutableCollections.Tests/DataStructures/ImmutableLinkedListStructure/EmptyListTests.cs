using ImmutableCollections.DataStructures.ImmutableLinkedListStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.ImmutableLinkedListStructure
{
    class EmptyListTests
    {
        [Test]
        public void Value_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.Value,
                Throws.InvalidOperationException);
        }

        [Test]
        public void Tail_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.Tail,
                Throws.InvalidOperationException);
        }

        [Test]
        public void Prepend_Returns_List_Node_With_Given_Value()
        {
            var sut = CreateSut();

            var result = sut.Prepend(12);

            Assert.That(result, Is.InstanceOf<ListNode<int>>());
            Assert.That(result.Value, Is.EqualTo(12));
        }

        [Test]
        public void Change_Value_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.ChangeValue(12),
                Throws.InvalidOperationException);
        }

        [Test]
        public void Change_Tail_Throws_Exception()
        {
            var sut = CreateSut();

            Assert.That(() => sut.ChangeTail(new ListNode<int>(12, new EmptyList<int>())),
                Throws.InvalidOperationException);
        }

        private EmptyList<int> CreateSut()
        {
            return new EmptyList<int>();
        }
    }
}