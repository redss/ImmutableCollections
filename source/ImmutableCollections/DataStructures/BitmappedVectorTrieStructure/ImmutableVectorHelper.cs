namespace ImmutableCollections.DataStructures.BitmappedVectorTrieStructure
{
    static class ImmutableVectorHelper
    {
        public const int Shift = 5;

        public static readonly int Fragmentation = ComputeFragmentation(Shift);

        public static readonly int Mask = Fragmentation - 1;

        public static int ComputeIndex(int index, int level)
        {
            // TODO: Test properly.
            // We divide index into shift-sized chunks and get the level-th chunk.
            return (index >> Shift * level) & Mask;
        }

        public static int ComputeFragmentation(int shift)
        {
            return 2 << shift - 1;
        }

        public static int ComputeShift(int fragmentation)
        {
            return fragmentation - 1;
        }
    }
}
