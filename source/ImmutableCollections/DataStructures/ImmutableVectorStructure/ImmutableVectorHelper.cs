namespace ImmutableCollections.DataStructures.ImmutableVectorStructure
{
    public static class ImmutableVectorHelper
    {
        public const int Shift = 5;

        public static readonly int Fragmentation = 2 << Shift - 1;

        public static int CountIndex(int index, int level)
        {
            // We divide index into shift-sized chunks and get the level-th chunk.
            return (index >> Shift * level) & (Fragmentation - 1);
        }
    }
}
