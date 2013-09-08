namespace ImmutableCollections.DataStructures.ImmutableVectorStructure
{
    public interface IVectorNode<T>
    {
        int Level { get; }

        /// <summary>
        /// Apeends an element at the end of the structure.
        /// </summary>
        /// <param name="elem">Appended element.</param>
        /// <param name="count">Count of the elements in this sub-tree.</param>
        /// <returns>Propagated node.</returns>
        IVectorNode<T> Append(T elem, int count);

        T Nth(int index);
    }
}
