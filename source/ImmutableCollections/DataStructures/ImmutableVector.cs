using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImmutableCollections.DataStructures.ImmutableVectorStructure;

namespace ImmutableCollections.DataStructures
{
    /// <summary>
    /// Collection based on Bitmapped Vector Trie.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImmutableVector<T> //: BaseImmutableList<T> //: IImmutableList<T>
    {
        private readonly IVectorNode<T> _root;

        private readonly int _count;

        public int Count
        {
            get { return _count; }
        }

        public ImmutableVector()
        {
            _count = 0;
            _root = null;
        }
        
        private ImmutableVector(int count, IVectorNode<T> root)
        {
            _count = count;
            _root = root;
        }

        public ImmutableVector<T> Add(T item)
        {
            if (_count == 0)
            {
                // Create tree with one element.
                var root = new VectorLeaf<T>(item);
                return new ImmutableVector<T>(_count + 1, root);
            }

            throw new NotImplementedException();
        }
    }
}
