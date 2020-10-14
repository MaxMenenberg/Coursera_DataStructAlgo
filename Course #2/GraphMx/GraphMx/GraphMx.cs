using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMx
{
    class GraphMx<T>
    {
        public int nodeCount = 0;
        public int edgeCount = 0;
        public int uniqueNodeID = 0;
        public int uniqueEdgeID = 0;
        public List<NodeMx<T>> nodes;
        public List<EdgeMx<T>> edges;

        public GraphMx(){
            nodes = new List<NodeMx<T>>();
            edges = new List<EdgeMx<T>>();
        }

        public void addNode(T data) {
            nodes.Add(new NodeMx<T>(data, uniqueNodeID));
            nodeCount = nodes.Count;
            uniqueNodeID++;
        }

        //Creates and unweights and undirected connection between nodes a and b
        public void addConnection(NodeMx<T> a, NodeMx<T> b) {
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, uniqueEdgeID);
            EdgeMx<T> edgeB = new EdgeMx<T>(b, a, uniqueEdgeID);
            edges.Add(edgeA);
            a.addEdge(edgeA);
            b.addEdge(edgeB);
            edgeCount = edges.Count;
            uniqueEdgeID++;
        }

        //Add a undirected weighted connection between nodes a and b
        public void addConnection(NodeMx<T> a, NodeMx<T> b, double w){
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, w, uniqueNodeID);
            EdgeMx<T> edgeB = new EdgeMx<T>(b, a, w, uniqueNodeID);
            edges.Add(edgeA);
            a.addEdge(edgeA);
            b.addEdge(edgeB);
            edgeCount = edges.Count;
            uniqueNodeID++;
        }

        //Creates a directed connection between nodes a and b where
        //node a is the tail and b is the head
        //I.E     a --> b     a is connected to b, but b is not connected to a
        public void addDirectedConnection(NodeMx<T> a, NodeMx<T> b) {
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, true, uniqueNodeID);
            edges.Add(edgeA);
            a.addEdge(edgeA);
            edgeCount = edges.Count;
            uniqueNodeID++;
        }

        //Adds a directed weighted connection between nodes a and b
        public void addDirectedConnection(NodeMx<T> a, NodeMx<T> b, double w){
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, w, uniqueNodeID);
            edges.Add(edgeA);
            a.addEdge(edgeA);
            edgeCount = edges.Count;
            uniqueNodeID++;
        }

        //Remove an edge from the graph
        public void removeConnection(EdgeMx<T> E) {
            for (int n = 0; n < edges.Count; n++)
            {
                if (E.identifier == edges[n].identifier)
                {
                    //First remove the edge from the 2 nodes it connects
                    E.nodes[0].removeEdge(E);
                    if (!E.isDirected)
                    {
                        E.nodes[1].removeEdge(E);
                    }

                    edges.RemoveAt(n);
                    edges.TrimExcess();
                    edgeCount = edges.Count;
                    break;
                }
            }
        }

        //Remove a node from the graph, and thus all of the edges connected to it
        public void removeNode(NodeMx<T> x) {
            //First remove all edges invovling this node
            for (int n = 0; n < edges.Count; n++) {
                if (edges[n].nodes[0].identifier == x.identifier || edges[n].nodes[1].identifier == x.identifier) {
                    
                    //First remove the edge from the 2 nodes it connects
                    edges[n].nodes[0].removeEdge(edges[n]);
                    if (!edges[n].isDirected)
                    {
                        edges[n].nodes[1].removeEdge(edges[n]);
                    }

                    //Then remove from the graph
                    edges.RemoveAt(n);
                    edges.TrimExcess();
                    n = n - 1; //So we don't skip an element after removal
                }
            }
            edgeCount = edges.Count;

            //Then remove the node from the graph completely
            for (int n = 0; n < nodes.Count; n++) {
                if (nodes[n].identifier == x.identifier) {
                    nodes.RemoveAt(n);
                    nodes.TrimExcess();
                    nodeCount = nodes.Count;
                    break;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            nodes.Sort();
            for (int n = 0; n < nodes.Count; n++) {
                sb.Append(nodes[n].ToString() + "\n");
            }
            return sb.ToString();
        }


    }
}
