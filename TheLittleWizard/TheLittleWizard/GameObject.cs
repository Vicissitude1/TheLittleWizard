using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    public class GameObject {
        public Vector2 position;
        public string textureName;
        private Texture2D texture;
        private Rectangle rect;
        private Rectangle rect2;

        protected GameObject() {
            position = new Vector2();
        }

        public void LoadContent(ContentManager content) {
            texture = content.Load<Texture2D>(textureName);
            switch (textureName) {
                case "groundSingleTile":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(56, 53, 83, 83);
                    break;

                case "path":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(60, 58, 83, 83);
                    break;

                case "wallSingleTile":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(59, 56, 83, 83);
                    break;

                case "tree":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(0, 60, 200, 140);
                    break;

                case "portalA":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(0, 60, 200, 140);
                    break;

                case "iceTower":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(0, 60, 200, 140);
                    break;

                case "key":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(0, 60, 200, 140);
                    break;

                case "tower":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(0, 60, 200, 140);
                    break;

                case "wizardFront":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(0, 60, 200, 140);
                    break;

                case "redMonster":
                    rect = new Rectangle((int)position.X, (int)position.Y, GameWorld.nodeSize, GameWorld.nodeSize);
                    rect2 = new Rectangle(0, 60, 200, 140);
                    break;


                default:
                    break;
            }


        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, rect, rect2, Color.White);
        }
    }
}
