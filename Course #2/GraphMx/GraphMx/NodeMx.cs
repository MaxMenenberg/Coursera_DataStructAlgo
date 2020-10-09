using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMx
{
    class NodeMx<T> : IComparable
    {
         public int identifier;
         public T data;

        public NodeMx(T x, int i) {
            identifier = i;
            data = x;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            NodeMx<T> n = obj as NodeMx<T>;
            return this.identifier.CompareTo(n.identifier);
        }

    }
}
