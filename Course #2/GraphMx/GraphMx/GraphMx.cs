using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphMx
{
    class GraphMx<T>
    {
        int nodeCount = 0;
        int edgeCount = 0;
        int uniqueNodeID = 0;
        int uniqueEdgeID = 0;
        List<NodeMx<T>> nodes;
        List<EdgeMx<T>> edges;

        public GraphMx(){
            nodes = new List<NodeMx<T>>();
        }

        public void addNode(T data) {
            nodes.Add(new NodeMx<T>(data, uniqueNodeID));
            nodeCount = nodes.Count;
            uniqueNodeID++;
        }

        //Creates and unweights and undirected connection between nodes a and b
        public void addConnection(NodeMx<T> a, NodeMx<T> b) {
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, uniqueEdgeID);
            edges.Add(edgeA);
            edgeCount = edges.Count;
            uniqueEdgeID++;
        }

        //Add a undirected weighted connection between nodes a and b
        public void addConnection(NodeMx<T> a, NodeMx<T> b, double w){
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, w, uniqueNodeID);
            edges.Add(edgeA);
            edgeCount = edges.Count;
            uniqueNodeID++;
        }

        //Creates a directed connection between nodes a and b where
        //node a is the tail and b is the head
        //I.E     a --> b     a is connected to b, but b is not connected to a
        public void addDirectedConnection(NodeMx<T> a, NodeMx<T> b) {
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, true, uniqueNodeID);
            edges.Add(edgeA);
            edgeCount = edges.Count;
            uniqueNodeID++;
        }

        //Adds a directed weighted connection between nodes a and b
        public void addDirectedConnection(NodeMx<T> a, NodeMx<T> b, double w){
            EdgeMx<T> edgeA = new EdgeMx<T>(a, b, w, uniqueNodeID);
            edges.Add(edgeA);
            edgeCount = edges.Count;
            uniqueNodeID++;
        }

        //Remove an edge from the graph
        public void removeConnection(EdgeMx<T> E) {
            for (int n = 0; n < edges.Count; n++)
            {
                if (E.identifier == edges[n].identifier)
                {
                    edges.RemoveAt(n);
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
                    edges.RemoveAt(n);
                    n = n - 1; //So we don't skip an element after removal
                }
            }
            edgeCount = edges.Count;

            //Then remove the node from the graph completely
            for (int n = 0; n < edges.Count; n++) {
                if (nodes[n].identifier == x.identifier) {
                    nodes.RemoveAt(n);
                    nodeCount = nodes.Count;
                    break;
                }
            }
        }


    }
}
