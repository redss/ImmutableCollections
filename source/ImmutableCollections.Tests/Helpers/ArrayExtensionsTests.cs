using System.Linq;
using NUnit.Framework;
using ImmutableCollections.Helpers;

namespace ImmutableCollections.Tests.Helpers
{
    class ArrayExtensionsTests
    {
        [Test]
        public void Can_Append_Element_To_New_Array()
        {
            var array = new[] { 1, 2, 3 };
            
            var result = array.Append(4);
            
            Assert.That(result, Is.EqualTo(new[] { 1, 2, 3, 4 }));
        }

        [Test]
        public void Can_Change_Element_In_Array()
        {
            var array = new[] { 1, 2, 3 };

            var result = array.Change(item: 100, index: 1);

            Assert.That(result, Is.EqualTo(new[] { 1, 100, 3 }));
        }

        [Test]
        public void Can_Reduce_Array_Size_And_Chenge_One_Element()
        {
            var array = new[] { 1, 2, 3, 4 };

            var result = array.TakeAndChange(item: 100, index: 1, take: 3);

            Assert.That(result, Is.EqualTo(new[] { 1, 100, 3 }));
        }

        [Test]
        public void Can_Create_Array_Copy_With_Reduced_Size()
        {
            var array = new[] { 1, 2, 3, 4 };

            var result = array.Take(3);

            Assert.That(result, Is.EqualTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void Can_Create_Array_Copy_Without_Element_At_Given_Index()
        {
            var array = new[] { 1, 2, 3 };

            var result = array.RemoveAt(1);

            Assert.That(result, Is.EqualTo(new[] { 1, 3 }));
        }

        [Test]
        public void Removing_Last_Array_Element_Should_Return_Null()
        {
            // TODO: This behavior is strange and should be changed.

            var arr = new[] { 1 };

            var result = arr.RemoveAt(1);
            
            Assert.That(result, Is.Null);
        }
    }
}
