using ImmutableCollections.DataStructures.PatriciaTrieStructure;
using NUnit.Framework;

#pragma warning disable 169

namespace ImmutableCollections.Tests.DataStructures.PatriciaTrieStructure
{
    class PatriciaHelperTests
    {
        [TestCaseSource(nameof(_branchingBitSource))]
        public void BranchingBit_Test(int prefixA, int prefixB)
        {
            var expected = NaiveBranchingBit(prefixA, prefixB);
            var result = PatriciaHelper.BranchingBit(prefixA, prefixB);

            Assert.AreEqual(expected, result);
        }

        private static readonly int[][] _branchingBitSource =
        {
            new[] { 32, 3123 },
            new[] { 12, 31 },
            new[] { 3213123, 31232132 },
            new[] { 1 << 30, 1 << 30 + 1 },
            new[] { -23, 123123 },
            new[] { -100, 100 }
        };

        private int NaiveBranchingBit(int prefixA, int prefixB)
        {
            if (prefixA == prefixB)
                return 0;

            for (var i = 1; ; i <<= 1)
                if ((prefixA & i) != (prefixB & i))
                    return i;
        }

        [TestCaseSource(nameof(_lowestBitSource))]
        public void LowestBit_Test(int number)
        {
            var expected = NaiveLowestBit(number);
            var result = PatriciaHelper.LowestBit(number);

            Assert.AreEqual(expected, result);
        }

        private static readonly int[] _lowestBitSource =
        {
            1,
            2,
            3,
            123,
            31412,
            41241412,
            1 << 30,
            -10,
            -20
        };

        private int NaiveLowestBit(int number)
        {
            return number % 2 != 0 ? 1 : 2 * NaiveLowestBit(number / 2);
        }
    }
}
