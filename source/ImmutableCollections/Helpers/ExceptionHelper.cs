using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace ImmutableCollections.Helpers
{
    /// <summary>
    /// Provides some common exception functionality.
    /// </summary>
    static class ExceptionHelper
    {
        [Pure]
        public static KeyNotFoundException GetKeyNotFoundException<TKey>(TKey key)
        {
            var message = $"Key {key} was not found.";
            return new KeyNotFoundException(message);
        }

        [Pure]
        public static ArgumentException GetKeyAlreadyExistsException<TKey>(TKey key, string parameterName)
        {
            var message = $"An element with '{key}' key already exists in the dictionary.";
            return new ArgumentException(message, parameterName);
        }

        [Pure]
        public static ArgumentException GetItemAlreadyExistsException<T>(T item, string parameterName)
        {
            var message = $"Item '{item}' already exists in the collection.";
            return new ArgumentException(message, parameterName);
        }

        [Pure]
        public static ArgumentException GetKeyCannotBeNullException(string parameterName)
        {
            var message = "Key cannot be null.";
            return new ArgumentException(message, parameterName);
        }

        [Pure]
        public static ArgumentOutOfRangeException GetIndexNegativeException(int index, string parameterName)
        {
            var message = $"Index cannot be negative, but was: {index}.";
            return new ArgumentOutOfRangeException(parameterName, index, message);
        }

        [Pure]
        public static ArgumentOutOfRangeException GetIndexTooBigException(int index, int count, string parameterName)
        {
            var message = $"Index {index} cannot be greater than {count}.";
            return new ArgumentOutOfRangeException(parameterName, message);
        }
    }
}
