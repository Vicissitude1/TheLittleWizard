using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    class Node : GameObject {
        public bool traversable;
        private GameObject another;

        public Node(int x, int y) {
            position = new Vector2(x * GameWorld.nodeSize, y * GameWorld.nodeSize);
            textureName = "groundSingleTile";
            traversable = true;
        }

        public void AddObjectOnTop(GameObject obj) {
            another = obj;
            traversable = false;
            GameWorld.instance.gameObjects.Add(obj);
        }
    }
}
