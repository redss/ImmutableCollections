using System.Linq;
using ImmutableCollections.DataStructures.BitmappedVectorTrieStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.VectorStructure
{
    class ImmutableVectorHelperTests
    {
        [TestCase(5, 32)]
        [TestCase(6, 64)]
        public void Fragmentation_Should_Be_Two_Power_Shift(int shift, int expectedFragmentation)
        {
            var result = ImmutableVectorHelper.ComputeFragmentation(shift);
            Assert.That(result, Is.EqualTo(expectedFragmentation));
        }

        [TestCase(32, 31)]
        [TestCase(64, 63)]
        public void Shift_Should_Be_Decremented_Mask(int fragmentation, int expectedMask)
        {
            var result = ImmutableVectorHelper.ComputeShift(fragmentation);
            Assert.That(result, Is.EqualTo(expectedMask));
        }

        [Test]
        public void Count_Index_Gives_Result_In_Modulo_32()
        {
            // TODO: Weird test... what does it prove?

            foreach (var i in Enumerable.Range(0, 1000))
            {
                var index = ImmutableVectorHelper.ComputeIndex(i, 0);
                var modulo = i % ImmutableVectorHelper.Fragmentation;

                Assert.That(modulo, Is.EqualTo(index));
            }
        }
    }
}