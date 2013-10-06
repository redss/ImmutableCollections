using System;

namespace ImmutableCollections.DataStructures.RedBlackTreeStructure
{
    class InvalidRedBlackTreeException : Exception
    {
        public InvalidRedBlackTreeException(string message)
            : base(message) { }
    }
}
