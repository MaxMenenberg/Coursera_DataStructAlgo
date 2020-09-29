using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
namespace GraphClass
{
    class Program
    {
        static void Main(string[] args)
        {
            List<List<int>> gMinCut = new List<List<int>>();
            int minCrossings = 9999;
            int N = 100;
            for (int n = 0; n < N; n++) {
                Graph G = generateTestGraph();
                List<List<int>> gCut = randomizedContractionMinimumCut(G);
                if (gCut[0][0] < minCrossings) {
                    minCrossings = gCut[0][0];
                    gMinCut = gCut;
                }
            }

            Debug.WriteLine(generateTestGraph().ToString());
            Debug.WriteLine("Minimum Cut");
            Debug.WriteLine("# of crossings: " + gMinCut[0][0]);
            Debug.WriteLine("Group A vertices: ");
            for (int n = 0; n < gMinCut[1].Count; n++) {
                Debug.Write(gMinCut[1][n] + ", ");
            }

            Debug.WriteLine("");
            Debug.WriteLine("Group B vertices: ");
            for (int n = 0; n < gMinCut[2].Count; n++)
            {
                Debug.Write(gMinCut[2][n] + ", ");
            }
            Debug.WriteLine("");
        }

        public static Graph generateTestGraph() {
            Graph G = new Graph();

            for (int n = 0; n < 4; n++)
            {
                G.addVertex();
            }

            G.addUndirectedConnection(G.GetVertex(0), G.GetVertex(1));
            G.addUndirectedConnection(G.GetVertex(0), G.GetVertex(2));
            G.addUndirectedConnection(G.GetVertex(2), G.GetVertex(3));
            G.addUndirectedConnection(G.GetVertex(1), G.GetVertex(3));
            G.addUndirectedConnection(G.GetVertex(0), G.GetVertex(3));

            return G;
        }

        //List[0]: The number of crossing edges from the returned cut of G
        //List[1]: The member vertices from one side of the cut
        //List[2]: The memberg vertices from the other side of the cut
        public static List<List<int>> randomizedContractionMinimumCut(Graph G) {
            Random rng = new Random();
            int randVertexLabel, randVertexLabel2;
            Vertex randVertex = null;
            Vertex randVertex2 = null;
            List<int> tempRandomNeighbors = new List<int>();
            while (G.getVertexCount() > 2) {
                int countVertex = G.getVertexCount();

                bool isRandomVertexConnected = false;
                
                while (!isRandomVertexConnected)
                {
                    //Pick a vertex at random
                    randVertexLabel = rng.Next(0, countVertex - 1);
                    randVertex = G.GetVertex(randVertexLabel);

                    //Now pick on of the edges at random
                    tempRandomNeighbors = randVertex.getNeighbors();
                    if (tempRandomNeighbors.Count > 0) {
                        isRandomVertexConnected = true;
                    }
                }

                int tempRandomNeighborCount = tempRandomNeighbors.Count;
                randVertexLabel2 = rng.Next(0, tempRandomNeighborCount);
                randVertex2 = G.GetVertex(tempRandomNeighbors[randVertexLabel2]);
                G.contract(randVertex, randVertex2);
            }

            List<List<int>> graphCut = new List<List<int>>();
            
            List<int> crossings = new List<int>();
            crossings.Add(G.getEdgeCount());

            List<int> groupA = new List<int>();
            Vertex vA = G.vertices[0];
            groupA.Add(vA.getVertexLabel());
            List<int> vAConNeighbors = vA.getContractedNeighbors();
            for (int n = 0; n < vAConNeighbors.Count; n++) {
                groupA.Add(vAConNeighbors[n]);
            }

            List<int> groupB = new List<int>();
            Vertex vB = G.vertices[1];
            groupB.Add(vB.getVertexLabel());
            List<int> vBConNeighbors = vB.getContractedNeighbors();
            for (int n = 0; n < vBConNeighbors.Count; n++)
            {
                groupB.Add(vBConNeighbors[n]);
            }

            graphCut.Add(crossings);
            graphCut.Add(groupA);
            graphCut.Add(groupB);

            return graphCut;
        }
    }
}
