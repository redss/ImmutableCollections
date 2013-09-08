using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableCollections.DataStructures.Helpers
{
    public static class ArrayExtensions
    {
        public static T[] Append<T>(this T[] array, T item)
        {
            return ArrayHelper.AppendedArray(array, item);
        }

        public static T[] Change<T>(this T[] array, T item, int index)
        {
            return ArrayHelper.ChangedArray(array, item, index);
        }
    }
}
