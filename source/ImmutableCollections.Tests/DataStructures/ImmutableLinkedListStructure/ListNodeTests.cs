using FakeItEasy;
using ImmutableCollections.DataStructures.ImmutableLinkedListStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.ImmutableLinkedListStructure
{
    class ListNodeTests
    {
        [Test]
        public void Properties_Return_Constructor_Values()
        {
            var element = 12;
            var tail = A.Fake<IListNode<int>>();
            
            var sut = new ListNode<int>(element, tail);

            Assert.That(sut.Value, Is.EqualTo(element));
            Assert.That(sut.Tail, Is.SameAs(tail));
        }

        [Test]
        public void Prepend_Returns_List_Containing_Both_Elements()
        {
            var sut = new ListNode<int>(1, A.Fake<IListNode<int>>());

            var result = sut.Prepend(2);

            Assert.That(result.Value, Is.EqualTo(2));
            Assert.That(result.Tail, Is.SameAs(sut));
        }

        [Test]
        public void Change_Value_Changes_An_Element()
        {
            var sut = new ListNode<int>(1, A.Fake<IListNode<int>>());

            var result = sut.ChangeValue(2);

            Assert.That(result.Value, Is.EqualTo(2));
            Assert.That(result.Tail, Is.SameAs(sut.Tail));
        }

        [Test]
        public void Change_Tail_Changes_Tail()
        {
            var newTail = A.Fake<IListNode<int>>();
            var sut = new ListNode<int>(1, A.Fake<IListNode<int>>());

            var result = sut.ChangeTail(newTail);

            Assert.That(result.Value, Is.EqualTo(sut.Value));
            Assert.That(result.Tail, Is.EqualTo(newTail));
        }
    }
}