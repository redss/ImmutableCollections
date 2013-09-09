﻿using System.Diagnostics.Contracts;
using System.Linq;

namespace ImmutableCollections.DataStructures.Helpers
{
    public static class ArrayExtensions
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
    }
}
