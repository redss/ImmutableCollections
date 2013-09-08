using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableCollections.DataStructures.ImmutableVectorStructure
{
    public class EmptyVector<T> : IVectorNode<T>
    {
        public int Level { get { return 0; } }

        public IEnumerable<T> GetValues()
        {
            yield break;
        }

        public IVectorNode<T> Append(T elem, int count)
        {
            return new VectorLeaf<T>(elem);
        }

        public T Nth(int index)
        {
            throw new ArgumentOutOfRangeException("index", "Vector is empty.");
        }

        public override string ToString()
        {
            return "Empty vector.";
        }
    }
}
