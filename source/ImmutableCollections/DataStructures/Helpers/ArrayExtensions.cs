using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ImmutableCollections.DataStructures.Helpers
{
    static class ArrayExtensions
    {
        [Pure]
        public static T[] Append<T>(this T[] array, T item)
        {
            var newArray = new T[array.Length + 1];
            array.CopyTo(newArray, 0);
            newArray[array.Length] = item;

            return newArray;
        }

        [Pure]
        public static T[] Change<T>(this T[] array, T item, int index)
        {
            var newArray = array.ToArray();
            newArray[index] = item;

            return newArray;
        }

        [Pure]
        public static T[] TakeAndChange<T>(this T[] array, T item, int index, int take)
        {
            var newArray = new T[take];
            Array.Copy(array, 0, newArray, 0, take);
            newArray[index] = item;

            return newArray;
        }

        [Pure]
        public static T[] Take<T>(this T[] array, int take)
        {
            var newArray = new T[take];
            Array.Copy(array, 0, newArray, 0, take);

            return newArray;
        }
    }
}
