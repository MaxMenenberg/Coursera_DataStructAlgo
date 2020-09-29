using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphClass
{
    class Graph
    {
        public List<Vertex> vertices = new List<Vertex>();
        int vertexCount = 0;
        int edgeCount = 0;

        public Graph() {

        }

        public void addVertex() {
            vertices.Add(new Vertex(vertexCount));
            vertexCount++;
        }

        public void removeVertex(Vertex v) {
            int removedVertexLabel = v.getVertexLabel();

            //First remove all connections from the vertex we want to remove
            for (int n = 0; n < vertexCount; n++) {
                while(vertices[n].isConnected(v)) {
                    removeConnection(vertices[n], v);
                }
            }

            //Now remove the vertex itself from the graph
            vertices.Remove(v);
            vertexCount--;

        }

        //Creates an undirected connection between vertices v1 and v2
        public void addUndirectedConnection(Vertex v1, Vertex v2) {
            v1.addNeighbor(v2);
            v2.addNeighbor(v1);
            edgeCount++;
        }

        //Creates a directed connection between vertices v1 and v2 (i.e. from v1 to v2)
        public void addDirectedConnection(Vertex v1, Vertex v2) {
            v1.addNeighbor(v2);
            edgeCount++;
        }

        public void removeConnection(Vertex v1, Vertex v2) {
            if(v1.isConnected(v2)) {
                v1.removeNeighbor(v2);
            }
            if(v2.isConnected(v1)) {
                v2.removeNeighbor(v1);
            }

            edgeCount--;

        }

        public Vertex GetVertex(int vertexLabel) {
            int returnIndex = 0;
            for (int n = 0; n < vertices.Count; n++) {
                if (vertices[n].getVertexLabel() == vertexLabel) {
                    returnIndex = n;
                    break;
                }
            }
            return vertices[returnIndex];
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            Vertex tempVertex;
            List<int> tempNeighbors;
            for (int n = 0; n < vertices.Count; n++) {
                tempVertex = vertices[n];
                sb.Append("Vertex " + tempVertex.getVertexLabel() + ": ");
                tempNeighbors = tempVertex.getNeighbors();
                for (int m = 0; m < tempNeighbors.Count; m++) {
                    if (m == tempNeighbors.Count - 1)
                    {
                        sb.Append(tempNeighbors[m]);
                    }
                    else
                    {
                        sb.Append(tempNeighbors[m] + ", ");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public int getVertexCount() {
            return vertexCount;
        }

        public int getEdgeCount() {
            return edgeCount;
        }
        
        // "Fuses vertices v1 and v2 together
        public void contract(Vertex v1, Vertex v2) {
            List<int> n1 = v1.getNeighbors();
            List<int> n2 = v2.getNeighbors();
            int neighborsGiven = 0;
            for (int n = 0; n < n2.Count; n++) {
                if (n2[n] == v1.getVertexLabel())
                {
                    //Do Nothing
                }
                else {
                    v1.addNeighbor(this.GetVertex(n2[n]));
                    this.GetVertex(n2[n]).addNeighbor(v1);
                    neighborsGiven++;
                }
            }
            edgeCount = edgeCount + neighborsGiven;

            //Add v2 as a contracted neighbor
            v1.addContractedNeighbor(v2.getVertexLabel());

            //If v2 has any conracted neighbors they now need to become v1's contracted neighbors
            List<int> tempConNeighbors = v2.getContractedNeighbors();
            if (tempConNeighbors.Count > 0) {
                for (int n = 0; n < tempConNeighbors.Count; n++) {
                    v1.addContractedNeighbor(tempConNeighbors[n]);
                }
            }
            this.removeVertex(v2);
        
        }


    }
}
