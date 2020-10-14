using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMx
{
    class EdgeMx<T>: IComparable
    {
        public int identifier;
        public bool isDirected = false;
        public bool isWeighted = false;
        public double weight = 0;
        public NodeMx<T>[] nodes = new NodeMx<T>[2];

        //Default constructor for the undirected and unweighted case
        public EdgeMx(NodeMx<T> a, NodeMx<T> b, int i) {
            nodes[0] = a;
            nodes[1] = b;
            identifier = i;
        }

        //If the edge is directed then the convention is that a is the tail, and b is the head
        public EdgeMx(NodeMx<T> a, NodeMx<T> b, bool directed, int i) {
            nodes[0] = a;
            nodes[1] = b;
            isDirected = directed;
            identifier = i;
        }

        //If the edge is un directed but weighted
        public EdgeMx(NodeMx<T> a, NodeMx<T> b, double edgeWeigt, int i) {
            nodes[0] = a;
            nodes[1] = b;
            isWeighted = true;
            weight = edgeWeigt;
            identifier = i;
        }

        //The fully comprehensive contrcutor
        public EdgeMx(NodeMx<T> a, NodeMx<T> b, bool directed, double edgeWeigt, int i)
        {
            nodes[0] = a;
            nodes[1] = b;
            isDirected = directed;
            isWeighted = true;
            weight = edgeWeigt;
            identifier = i;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            EdgeMx<T> e = obj as EdgeMx<T>;
            return this.identifier.CompareTo(e.identifier);
        }
    }
}
