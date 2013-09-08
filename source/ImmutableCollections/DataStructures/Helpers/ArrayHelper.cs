using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableCollections.DataStructures.Helpers
{
    public static class ArrayHelper
    {
        public static T[] AppendedArray<T>(T[] array, T item)
        {
            var newArray = new T[array.Length + 1];
            array.CopyTo(newArray, 0);
            newArray[array.Length] = item;

            return newArray;
        }

        public static T[] ChangedArray<T>(T[] array, T item, int index)
        {
            var newArray = array.ToArray();
            newArray[index] = item;

            return newArray;
        }
    }
}
