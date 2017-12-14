using System;
using System.Collections.Generic;

namespace TheLittleWizard {
    public enum Direction { Northwest, North, Northeast, West, East, Southwest, South, Southeast }

    public class NeighbourHandler {
        bool neighboursSetup;

        Node owner;

        public Node Northwest { get; private set; }
        public Node North { get; private set; }
        public Node Northeast { get; private set; }
        public Node West { get; private set; }
        public Node East { get; private set; }
        public Node Southwest { get; private set; }
        public Node South { get; private set; }
        public Node Southeast { get; private set; }

        private List<Node> allNeighhours;
        public List<Node> AllNeighbhours {
            get {
                if (neighboursSetup) {
                    return allNeighhours;
                } else {
                    return new List<Node>();
                }
            }
        }


        public NeighbourHandler(Node owner) {
            this.owner = owner;
            neighboursSetup = false;
            allNeighhours = new List<Node>();
        }

        public void Setup(Node[,] nodeMap) {
            int lengthX = nodeMap.GetLength(0);
            int lengthY = nodeMap.GetLength(1);

            for (int xChange = -1; xChange < 2; xChange++) {
                for (int yChange = -1; yChange < 2; yChange++) {
                    if (owner.TilePos.X + xChange >= 0 && owner.TilePos.X + xChange < lengthX && owner.TilePos.Y + yChange >= 0 && owner.TilePos.Y + yChange < lengthY) {
                        SetNeighbour(nodeMap, (int)owner.TilePos.X + xChange, (int)owner.TilePos.Y + yChange);
                    }
                }
            }
        }

        private void SetNeighbour(Node[,] nodeMap, int x, int y) {
            if (x == -1) {
                if (y == -1) {
                    Northwest = nodeMap[x, y];
                }
                if (y == 0) {
                    North = nodeMap[x, y];
                }
                if (y == 1) {
                    Northeast = nodeMap[x, y];
                }
            }
            if (x == 0) {
                if (y == -1) {
                    West = nodeMap[x, y];
                }
                if (y == 1) {
                    East = nodeMap[x, y];
                }
            }
            if (x == 1) {
                if (y == -1) {
                    Southwest = nodeMap[x, y];
                }
                if (y == 0) {
                    South = nodeMap[x, y];
                }
                if (y == 1) {
                    Southeast = nodeMap[x, y];
                }
            }

            allNeighhours.Add(nodeMap[x, y]);
            neighboursSetup = true;
        }
    }
}