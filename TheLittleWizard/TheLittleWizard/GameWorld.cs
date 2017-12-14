using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TheLittleWizard {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld:Game {

        private static GameWorld _instance;
        public static GameWorld instance {
            get {
                if (_instance == null) {
                    _instance = new GameWorld();
                }
                return _instance;
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static readonly int nodeSize = 64;

        public List<GameObject> gameObjects;
        public GameObject player;
        public GameObject[] keys;
        Node[,] nodeMap;

        private GameWorld() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = nodeSize * 10;
            graphics.PreferredBackBufferHeight = nodeSize * 10;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;

            gameObjects = new List<GameObject>();
            keys = new GameObject[2];

            SetupMap();



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            foreach (GameObject obj in gameObjects) {
                obj.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            foreach (GameObject obj in gameObjects) {
                obj.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SetupMap() {
            nodeMap = new Node[10, 10];

            for (int y = 0; y < 10; y++) {
                for (int x = 0; x < 10; x++) {
                    if (CheckIfRoad(new Vector2(x, y))) {
                        if (checkIfSingleTraversable(new Vector2(x,y))) {
                            nodeMap[x, y] = new Node(x, y, "path", true);
                        }
                        nodeMap[x, y] = new Node(x, y, "path");
                        gameObjects.Add(nodeMap[x, y]);
                    } else {
                        nodeMap[x, y] = new Node(x, y);
                        gameObjects.Add(nodeMap[x, y]);
                    }
                }
            }

            for (int i = 4; i < 7; i++) {
                for (int j = 1; j < 7; j++) {
                    nodeMap[i, j].AddObjectOnTop(new GameObject(i, j, "wallSingleTile"));
                }
            }

            player = new GameObject(1, 8, "wizardFront");
            gameObjects.Add(player);
            nodeMap[2, 4].AddObjectOnTop(new GameObject(2, 4, "tower"));
            nodeMap[8, 7].AddObjectOnTop(new GameObject(8, 7, "iceTower"));
            nodeMap[0, 8].AddObjectOnTop(new GameObject(0, 8, "portalA"));
            
            for (int j = 7; j < 10; j += 2) {
                for (int i = 2; i < 7; i++) {
                    nodeMap[i, j].AddObjectOnTop(new GameObject(i, j, "tree"));
                }
            }
            
            List<Vector2> positionsForKeys = new List<Vector2>();
            for (int x = 0; x < 10; x++) {
                for (int y = 0; y < 10; y++) {
                    if (nodeMap[x, y].traversable == true) {
                        positionsForKeys.Add(new Vector2(x, y));
                    }
                }
            }

            Random rnd = new Random();
            int first = rnd.Next(0, positionsForKeys.Count);
            int second = -1;
            while (second == -1 || second == first) {
                second = rnd.Next(0, positionsForKeys.Count);
            }
            keys[0] = new GameObject((int)positionsForKeys[first].X, (int)positionsForKeys[first].Y, "key");
            keys[1] = new GameObject((int)positionsForKeys[second].X, (int)positionsForKeys[second].Y, "key");
            gameObjects.Add(keys[0]);
            gameObjects.Add(keys[1]);
        }

        private static readonly List<Vector2> roadSet = new List<Vector2>() {
            new Vector2(3,0), new Vector2(4,0), new Vector2(5,0), new Vector2(6,0), new Vector2(7,0),
            new Vector2(3,1), new Vector2(7,1),
            new Vector2(3,2), new Vector2(7,2),
            new Vector2(3,3), new Vector2(7,3),
            new Vector2(3,4), new Vector2(7,4),
            new Vector2(1,5), new Vector2(2,5), new Vector2(3,5), new Vector2(7,5), new Vector2(8,5),
            new Vector2(1,6), new Vector2(8,6),
            new Vector2(1,7),
            new Vector2(1,8), new Vector2(2,8), new Vector2(3,8), new Vector2(4,8), new Vector2(5,8), new Vector2(6,8), new Vector2(7,8), new Vector2(8,8)
        };
        private bool CheckIfRoad(Vector2 pos) {
            return roadSet.Contains(pos);
        }

        private static readonly List<Vector2> singleTraversableRoadSet = new List<Vector2>() {
            new Vector2(2,8), new Vector2(3,8), new Vector2(4,8), new Vector2(5,8), new Vector2(6,8)
        };
        private bool checkIfSingleTraversable(Vector2 pos) {
            return singleTraversableRoadSet.Contains(pos);
        }
    }
}
