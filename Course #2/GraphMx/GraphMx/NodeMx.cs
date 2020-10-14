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
        public List<EdgeMx<T>> nodeEdges;
        public bool isVisited = false;

        public NodeMx(T x, int i) {
            identifier = i;
            data = x;
            nodeEdges = new List<EdgeMx<T>>();
        }

        //Create an undirected/unwieghted edge between this node and some other node
        //The graph class will make sure this edge is created correctly. The purpose
        //of having a list of edges in this class is to quickly see the neighbors that 
        //this node has
        public void addEdge(EdgeMx<T> x) {
            nodeEdges.Add(x);
        }

        //Remove an edge from this node
        public void removeEdge(EdgeMx<T> x) {
            for (int n = 0; n < nodeEdges.Count; n++) {
                if (nodeEdges[n].identifier == x.identifier) {
                    nodeEdges.RemoveAt(n);
                    nodeEdges.TrimExcess();
                    break;
                }
            }
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.identifier + ": ");
            for (int n = 0; n < nodeEdges.Count; n++) {
                sb.Append(nodeEdges[n].nodes[1].identifier + ", ");            
            }

            return sb.ToString();
        }

    }
}
