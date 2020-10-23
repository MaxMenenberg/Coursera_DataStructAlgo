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

            List<NodeMx<int>>[] paths;

            double[] shortestPaths = DijkstraShortestPath(G, 5, out paths);

            int a = 4;
        }

        public static GraphMx<int> createTestGraph() {
            GraphMx<int> G = new GraphMx<int>();
            for (int n = 0; n < 10; n++)
            {
                G.addNode(1);
            }

            G.addDirectedConnection(G.nodes[0], G.nodes[1], 1);
            G.addDirectedConnection(G.nodes[0], G.nodes[3], 4);

            G.addDirectedConnection(G.nodes[1], G.nodes[2], 2);
            G.addDirectedConnection(G.nodes[1], G.nodes[4], 3);
            G.addDirectedConnection(G.nodes[1], G.nodes[5], 3);
            G.addDirectedConnection(G.nodes[1], G.nodes[6], 4);

            G.addDirectedConnection(G.nodes[2], G.nodes[4], 3);

            G.addDirectedConnection(G.nodes[3], G.nodes[4], 5);

            G.addDirectedConnection(G.nodes[4], G.nodes[5], 3);
            G.addDirectedConnection(G.nodes[4], G.nodes[7], 1);

            G.addDirectedConnection(G.nodes[5], G.nodes[6], 2);
            G.addDirectedConnection(G.nodes[5], G.nodes[7], 1);

            G.addDirectedConnection(G.nodes[6], G.nodes[8], 2);
            G.addDirectedConnection(G.nodes[6], G.nodes[9], 3);

            G.addDirectedConnection(G.nodes[7], G.nodes[9], 7);

            G.addDirectedConnection(G.nodes[8], G.nodes[9], 4);




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

        public static List<NodeMx<int>> DFS(GraphMx<int> G, int startNode) {
            List<NodeMx<int>> foundNodes = new List<NodeMx<int>>();

            NodeMx<int> s = G.nodes[startNode];
            s.isVisited = true;
            foundNodes.Add(s);

            for (int n = 0; n < s.nodeEdges.Count; n++) {
                NodeMx<int> v = s.nodeEdges[n].nodes[1];
                if (v.isVisited == false)
                {
                    v.isVisited = true;
                    List<NodeMx<int>> foundNodesTemp = DFS(G, v.identifier);
                    foundNodes.AddRange(foundNodesTemp);
                }

            }

            return foundNodes;
        }

        public static double[] DijkstraShortestPath(GraphMx<int> G, int S, out List<NodeMx<int>>[] paths) {
            double[] shortestPaths = new double[G.nodeCount];
            paths = new List<NodeMx<int>>[G.nodeCount];

            //Set all the initials shortest paths to basically infinity and then reassign them to an 
            //initial value of -1 for the nodes that are reachable
            for (int n = 0; n < shortestPaths.Length; n++) {
                shortestPaths[n] = Double.MaxValue;
            }

            //First we need to figure out which nodes arent reachable from out starting node S. 
            List<NodeMx<int>> ReachableNodes = BFS(G, S);
            for (int n = 0; n < ReachableNodes.Count; n++) {
                shortestPaths[ReachableNodes[n].identifier] = -1;
            }

            //During the process of the breadth first search (BFS) we will have set the "isVisited" flag to true
            //for each node which we now need to undo
            for (int n = 0; n < G.nodeCount; n++) {
                G.nodes[n].isVisited = false;
            }

            //Initialize the paths to unreachable nodes as null
            for (int n = 0; n < G.nodeCount; n++) {
                if (shortestPaths[n] > -1)
                {
                    paths[n] = null;
                }
                else {
                    paths[n] = new List<NodeMx<int>>();
                    paths[n].Add(G.nodes[S]);
                }
            }

            //Initialize our explored nodes with our starting node
            List<NodeMx<int>> exploredNodes = new List<NodeMx<int>>();
            exploredNodes.Add(G.nodes[S]);
            G.nodes[S].isVisited = true;
            shortestPaths[S] = 0;
            

            while (exploredNodes.Count < ReachableNodes.Count) {
               
                //For each node we have explored
                List<EdgeMx<int>> edgesToConsider = new List<EdgeMx<int>>();
                for (int n = 0; n < exploredNodes.Count; n++) {
                    NodeMx<int> tempNodeTail = exploredNodes[n];

                    //Find all of its edges in the region which we have not yet explored
                    for (int m = 0; m < tempNodeTail.nodeEdges.Count; m++) {
                        NodeMx<int> tempNodeHead = tempNodeTail.nodeEdges[m].nodes[1];
                        if (tempNodeHead.isVisited == false) {
                            edgesToConsider.Add(tempNodeTail.nodeEdges[m]);
                        }
                    }
                }

                //Now that we have the list of candidate edges we need to compute the Dijkstra criteria for each edge
                EdgeMx<int> nextEdge = edgesToConsider[0];
                double DijstraMin = Double.MaxValue;
                double tempDijkstra;
                for (int n = 0; n < edgesToConsider.Count; n++) {
                    tempDijkstra = shortestPaths[edgesToConsider[n].nodes[0].identifier] + edgesToConsider[n].weight;
                    if (tempDijkstra < DijstraMin) {
                        DijstraMin = tempDijkstra;
                        nextEdge = edgesToConsider[n];
                    }
                }

                //Now that we have found the edge that minimizes the Dijkstra criteria we will add the node at the
                //head of the edge to the list of explored nodes and update its shortest path length and shortest 
                //path in our other variables
                NodeMx<int> nodeToAdd = nextEdge.nodes[1];
                nodeToAdd.isVisited = true;
                exploredNodes.Add(nodeToAdd);

                //The shortest path of the node we are going to add is equal to the shortest path of the node from which we
                //came from plus the weight of the edge
                shortestPaths[nodeToAdd.identifier] = shortestPaths[nextEdge.nodes[0].identifier] + nextEdge.weight;
                List<NodeMx<int>> pathUptillNow = paths[nextEdge.nodes[0].identifier];
                for (int n = 1; n < pathUptillNow.Count; n++) {
                    paths[nodeToAdd.identifier].Add(pathUptillNow[n]);
                }
                paths[nodeToAdd.identifier].Add(nodeToAdd);
            }

            return shortestPaths;


        }
    }
}
