using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    class Node : GameObject {
        private static readonly int squareSize = 32;

        public Node(int x, int y) {
            position = new Vector2(x * squareSize, y * squareSize);
        }
    }
}
