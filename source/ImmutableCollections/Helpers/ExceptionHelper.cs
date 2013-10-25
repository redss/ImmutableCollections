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
    }
}
