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
            var message = string.Format("Key {0} was not found.", key);
            return new KeyNotFoundException(message);
        }

        [Pure]
        public static ArgumentException GetKeyAlreadyExistsException<TKey>(TKey key, string parameterName)
        {
            var message = string.Format("An element with '{0}' key already exists in the dictionary.", key);
            return new ArgumentException(message, parameterName);
        }

        [Pure]
        public static ArgumentException GetItemAlreadyExistsException<T>(T item, string parameterName)
        {
            var message = string.Format("Item '{0}' already exists in the collection.", item);
            return new ArgumentException(message, parameterName);
        }

        [Pure]
        public static ArgumentException GetKeyCannotBeNullException(string parameterName)
        {
            const string message = "Key cannot be null.";
            return new ArgumentException(message, parameterName);
        }

        [Pure]
        public static ArgumentOutOfRangeException GetIndexNegativeException(int index, string parameterName)
        {
            var message = string.Format("Index cannot be negative, but was: {0}.", index);
            return new ArgumentOutOfRangeException(parameterName, index, message);
        }

        [Pure]
        public static ArgumentOutOfRangeException GetIndexTooBigException(int index, int count, string parameterName)
        {
            var message = string.Format("Index {0} cannot be greater than {1}.", index, count);
            return new ArgumentOutOfRangeException(parameterName, message);
        }
    }
}
