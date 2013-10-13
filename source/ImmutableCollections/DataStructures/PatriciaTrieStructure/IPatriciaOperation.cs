namespace ImmutableCollections.DataStructures.PatriciaTrieStructure
{
    interface IPatriciaOperation<T>
    {
        T[] OnFound(T[] items);

        T[] OnInsert();
    }
}