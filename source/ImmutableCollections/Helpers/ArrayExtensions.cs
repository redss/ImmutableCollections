using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ImmutableCollections.Helpers
{
    /// <summary>
    /// Extension methods for array type.
    /// </summary>
    static class ArrayExtensions
    {
        /// <summary>
        /// Creates copy of the array with given value appended at the end. Original array is intact.
        /// </summary>
        [Pure]
        public static T[] Append<T>(this T[] array, T item)
        {
            var newArray = new T[array.Length + 1];
            array.CopyTo(newArray, 0);
            newArray[array.Length] = item;

            return newArray;
        }

        /// <summary>
        /// Creates copy of the array with given value changed at specified index. Original array is intact.
        /// </summary>
        [Pure]
        public static T[] Change<T>(this T[] array, T item, int index)
        {
            var newArray = array.ToArray();
            newArray[index] = item;

            return newArray;
        }

        /// <summary>
        /// Creates copy of the array reduced to given size (take) and changes value at specified index. Original array is intact.
        /// </summary>
        [Pure]
        public static T[] TakeAndChange<T>(this T[] array, T item, int index, int take)
        {
            var newArray = new T[take];
            Array.Copy(array, 0, newArray, 0, take);
            newArray[index] = item;

            return newArray;
        }

        /// <summary>
        /// Creates copy of the array reduced to given size. Original array is intact.
        /// </summary>
        [Pure]
        public static T[] ArrayTake<T>(this T[] array, int take)
        {
            var newArray = new T[take];
            Array.Copy(array, 0, newArray, 0, take);

            return newArray;
        }

        /// <summary>
        /// Creates copy of the array without element at given index.
        /// If array contains only one element, returns null;
        /// </summary>
        [Pure]
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            var length = array.Length;

            if (length == 1)
                return null;

            var newArray = new T[length - 1];

            if (index > 0)
                Array.Copy(array, 0, newArray, 0, index);

            if (index < length)
                Array.Copy(array, index + 1, newArray, index, length - index - 1);

            return newArray;
        }
    }
}
