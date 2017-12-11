using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    class Node : GameObject {
        public Node(int x, int y) {
            position = new Vector2(x * GameWorld.nodeSize, y * GameWorld.nodeSize);
            textureName = "groundSingleTile";
        }
    }
}
