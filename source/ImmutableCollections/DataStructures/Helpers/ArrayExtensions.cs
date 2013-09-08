using System.Diagnostics.Contracts;

namespace ImmutableCollections.DataStructures.Helpers
{
    public static class ArrayExtensions
    {
        [Pure]
        public static T[] Append<T>(this T[] array, T item)
        {
            return ArrayHelper.AppendedArray(array, item);
        }

        [Pure]
        public static T[] Change<T>(this T[] array, T item, int index)
        {
            return ArrayHelper.ChangedArray(array, item, index);
        }
    }
}
