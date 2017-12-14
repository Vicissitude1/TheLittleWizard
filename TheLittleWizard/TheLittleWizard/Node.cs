﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    public class Node : GameObject {
        public Vector2 TilePos { get; private set; }
        public bool traversable;
        public bool traversableOnce;
        private GameObject another;

        public Node(int x, int y) {
            TilePos = new Vector2(x, y);
            position = new Vector2(x * GameWorld.nodeSize, y * GameWorld.nodeSize);
            textureName = "groundSingleTile";
            traversable = true;
            traversableOnce = false;
        }
        public Node(int x, int y, string nameOfTexture) {
            TilePos = new Vector2(x, y);
            position = new Vector2(x * GameWorld.nodeSize, y * GameWorld.nodeSize);
            textureName = nameOfTexture;
            traversable = true;
            traversableOnce = false;
        }
        public Node(int x, int y, string nameOfTexture, bool traversableOnce) {
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
                GameWorld.instance.gameObjects.Add(new GameObject((int)TilePos.X, (int)TilePos.Y, "redMonster"));
                traversable = false;
            }
            return position;
        }
    }
}
