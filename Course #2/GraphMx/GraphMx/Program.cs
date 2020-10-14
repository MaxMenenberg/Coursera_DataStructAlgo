using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace GraphMx
{
    class Program
    {
        static void Main(string[] args)
        {

            GraphMx<int> G = createTestGraph();

            Debug.WriteLine("Node Count: " + G.nodeCount);
            Debug.WriteLine("Edge Count: " + G.edgeCount);
            Debug.WriteLine(G.ToString());

            List<NodeMx<int>> BFSNodes = BFS(G, 9);
            for (int n = 0; n < BFSNodes.Count; n++) {
                Debug.Write(BFSNodes[n].identifier + ", ");
            }

        }

        public static GraphMx<int> createTestGraph() {
            GraphMx<int> G = new GraphMx<int>();
            for (int n = 0; n < 10; n++)
            {
                G.addNode(1);
            }

            G.addConnection(G.nodes[0], G.nodes[1]);
            G.addConnection(G.nodes[0], G.nodes[2]);
            G.addConnection(G.nodes[1], G.nodes[3]);
            G.addConnection(G.nodes[2], G.nodes[3]);
            G.addConnection(G.nodes[2], G.nodes[4]);
            G.addConnection(G.nodes[3], G.nodes[4]);
            G.addConnection(G.nodes[3], G.nodes[5]);
            G.addConnection(G.nodes[4], G.nodes[5]);

            G.addConnection(G.nodes[6], G.nodes[7]);
            G.addConnection(G.nodes[7], G.nodes[8]);
            G.addConnection(G.nodes[8], G.nodes[9]);
            G.addConnection(G.nodes[9], G.nodes[6]);

            return G;
        }

        public static List<NodeMx<int>> BFS(GraphMx<int> G, int startNode) {
            List<NodeMx<int>> foundNodes = new List<NodeMx<int>>();

            Queue exploredNodes = new Queue();
            exploredNodes.Enqueue(G.nodes[startNode]);
            G.nodes[startNode].isVisited = true;

            while (exploredNodes.Count != 0) {
                NodeMx<int> v = (NodeMx<int>)exploredNodes.Dequeue();
                foundNodes.Add(v);
                for (int n = 0; n < v.nodeEdges.Count; n++) {
                    NodeMx<int> w = v.nodeEdges[n].nodes[1];
                    if (w.isVisited == false) {
                        w.isVisited = true;
                        exploredNodes.Enqueue(w);
                    }
                }

            }
            return foundNodes;
            
        }
    }
}
