using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphClass
{
    class Vertex : IComparable
    {
        //A list of other verticies this vertext is connected to
        private List<int> neighbors = new List<int>();

        //This is for keeping track of fused nodes during contraction
        private List<int> contractedNeighbors = new List<int>();

        //A label meant to distinguish this vertex from others in the graph
        private int label;

        public Vertex(int x) {
            label = x;
        }

        public int getVertexLabel() {
            return label;
        }

        public void addNeighbor(Vertex v) {
            neighbors.Add(v.getVertexLabel());
            neighbors.Sort();
        }

        public void removeNeighbor(Vertex v) {
            neighbors.Remove(v.getVertexLabel());
        }

        public bool isConnected(Vertex v) {
            return neighbors.Contains(v.getVertexLabel());
        }

        public List<int> getNeighbors() {
            return neighbors;
        }

        public int CompareTo(object obj) {
            if (obj == null) {
                return 1;
            }
            Vertex v = obj as Vertex;
            return this.getVertexLabel().CompareTo(v.getVertexLabel());
        }

        public List<int> getContractedNeighbors() {
            return contractedNeighbors;
        }

        public void addContractedNeighbor(int vertexLabel) {
            contractedNeighbors.Add(vertexLabel);
        }

    }
}
