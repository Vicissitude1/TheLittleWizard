using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    class Dijkstra {

        public List<Node> fullCheck;
        public List<Node> notFullCheck;


        GameObject map;

        private Node start;
        private Node end;
        private Node current;
        bool allowDiagonal;

        int iterations;

        public int Iterations { get { return iterations; } }

        public Dijkstra(GameObject map, Node start, Node end, bool allowDiagonal) {
            this.map = map;
            this.start = start;
            this.end = end;
            this.allowDiagonal = allowDiagonal;

            fullCheck = new List<Node>();
            notFullCheck = new List<Node>();
            
        }



        
    }
}
