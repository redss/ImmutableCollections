﻿using System;
using System.Linq;
using ImmutableCollections.DataStructures.BitmappedVectorTrieStructure;
using NUnit.Framework;

namespace ImmutableCollections.Tests.DataStructures.VectorStructure
{
    [TestFixture]
    public class VectorTests
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
                var index = ImmutableVectorHelper.ComputeIndex(i, 0);
                var modulo = i % ImmutableVectorHelper.Fragmentation;

                Assert.AreEqual(modulo, index);
            }
        }

        [Test]
        public void AppendingToVectorNodes_DoesntThrowException()
        {
            IVectorNode<int> root = EmptyVector<int>.Instance;

            foreach (var i in Enumerable.Range(0, 1 << ImmutableVectorHelper.Shift * 3 + 1))
            {
                root = root.Append(i, i);

                // Is level correct?
                var level = (i == 0) ? 0 : (int) Math.Log(i, ImmutableVectorHelper.Fragmentation);
                Assert.AreEqual(level, root.Level);

                // Is type correct?
                var type = (level == 0) ? typeof(VectorLeaf<int>) : typeof(VectorLevel<int>);
                Assert.IsInstanceOf(type, root);
            }
        }

        [Test]
        public void VectorNodeNth_ReturnsCorrectResult()
        {
            var range = Enumerable.Range(0, 1 << 16).ToArray();

            IVectorNode<int> root = EmptyVector<int>.Instance;
            root = range.Aggregate(root, (current, i) => current.Append(i, i));

            foreach (var i in range)
            {
                var elem = root.Nth(i);
                Assert.AreEqual(i, elem);
            }
        }
    }
}
