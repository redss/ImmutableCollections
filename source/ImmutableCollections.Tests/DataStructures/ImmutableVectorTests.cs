using System;
using System.Linq;
using NUnit.Framework;
using ImmutableCollections.DataStructures.ImmutableVectorStructure;

namespace ImmutableCollections.Tests.DataStructures
{
    [TestFixture]
    public class ImmutableVectorTests
    {
        [Test]
        public void Fragmentation_IsTwoPowerShift()
        {
            var fragmentation = (int) Math.Pow(2, ImmutableVectorHelper.Shift);
            Assert.AreEqual(fragmentation, ImmutableVectorHelper.Fragmentation);
        }

        [Test]
        public void CountIndex_GivesResultInModulo32()
        {
            foreach (var i in Enumerable.Range(0, 1000))
            {
                var index = ImmutableVectorHelper.CountIndex(i, 0);
                var modulo = i % ImmutableVectorHelper.Fragmentation;

                Assert.AreEqual(modulo, index);
            }
        }

        [Test]
        public void AppendingToVectorNodes_DoesntThrowException()
        {
            IVectorNode<int> root = new VectorLeaf<int>(0);

            foreach (var i in Enumerable.Range(1, 400))
            {
                root = root.Append(i, i);

                if (i < ImmutableVectorHelper.Fragmentation)
                    Assert.IsInstanceOf<VectorLeaf<int>>(root);
                else
                    Assert.IsInstanceOf<VectorLevel<int>>(root);
            }

            System.Diagnostics.Debugger.Break();
        }
    }
}
