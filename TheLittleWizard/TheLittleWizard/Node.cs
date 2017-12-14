using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    public class Node : GameObject {
        #region AStarVariables
        public NeighbourHandler neighbourHandler;
        public int f;
        public int g;
        public int h;
        public Node parent;
        #endregion AStarVariables


        public Vector2 TilePos { get; private set; }
        public bool traversable;
        public bool traversableOnce;
        private GameObject another;

        public Node(int x, int y) {
            neighbourHandler = new NeighbourHandler(this);
            TilePos = new Vector2(x, y);
            position = new Vector2(x * GameWorld.nodeSize, y * GameWorld.nodeSize);
            textureName = "groundSingleTile";
            traversable = true;
            traversableOnce = false;
        }
        public Node(int x, int y, string nameOfTexture) {
            neighbourHandler = new NeighbourHandler(this);
            TilePos = new Vector2(x, y);
            position = new Vector2(x * GameWorld.nodeSize, y * GameWorld.nodeSize);
            textureName = nameOfTexture;
            traversable = true;
            traversableOnce = false;
        }
        public Node(int x, int y, string nameOfTexture, bool traversableOnce) {
            neighbourHandler = new NeighbourHandler(this);
            TilePos = new Vector2(x, y);
            position = new Vector2(x * GameWorld.nodeSize, y * GameWorld.nodeSize);
            textureName = nameOfTexture;
            traversable = true;
            this.traversableOnce = traversableOnce;
        }

        public void AddObjectOnTop(GameObject obj) {
            another = obj;
            traversable = false;
            GameWorld.instance.gameObjects.Add(obj);
        }

        public Vector2 Traverse() {
            if (traversableOnce) {
                GameObject monster = new GameObject((int)TilePos.X, (int)TilePos.Y, "redMonster");
                monster.LoadContent(GameWorld.instance.Content);
                GameWorld.instance.gameObjects.Add(monster);
                
                traversable = false;
            }
            return position;
        }

        public void CalculateFGX(Node parent, Node end) {
            if (g <= 0) {
                g = ((int)(Vector2.Distance(parent.position, position) + parent.g)) / 4;
                this.parent = parent;
            } else {
                if (g > (int)(Vector2.Distance(parent.position, position) + parent.g)) {
                    this.parent = parent;
                    g = ((int)(Vector2.Distance(parent.position, position) + parent.g)) / 4;
                }
            }
            h = (int)Vector2.Distance(end.position, position);
            f = g + h;
        }
        public void ResetFGX() {
            g = 0;
            h = 0;
            f = 0;
            parent = null;
        }

        public void CalculateDijkstraFGX(Node parent, Node end) {
            if (g <= 0) {
                g = ((int)(Vector2.Distance(parent.position, position) + parent.g)) / 4;
                this.parent = parent;
            } else {
                if (g > (int)(Vector2.Distance(parent.position, position) + parent.g)) {
                    this.parent = parent;
                    g = ((int)(Vector2.Distance(parent.position, position) + parent.g)) / 4;
                }
            }
            
            f = g + h;
        }
        public void ResetDijkstraFGX() {
            g = 0;
            f = 0;
            parent = null;
        }
    }
}
