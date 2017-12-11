using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TheLittleWizard {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld:Game {

        private static GameWorld _instance;
        public static GameWorld instance {
            get {
                if (instance == null) {
                    _instance = new GameWorld();
                }
                return _instance;
            }
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static readonly int nodeSize = 64;

        public List<GameObject> gameObjects;
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

            gameObjects = new List<GameObject>();

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

            for (int x = 0; x < 10; x++) {
                for (int y = 0; y < 10; y++) {
                    if (CheckIfRoad(new Vector2(x, y))) {
                        nodeMap[x, y] = new Node(x, y, "path");
                        gameObjects.Add(nodeMap[x, y]);
                    } else {
                        nodeMap[x, y] = new Node(x, y);
                        gameObjects.Add(nodeMap[x, y]);
                    }
                }
            }
        }

        private static readonly List<Vector2> roadSet = new List<Vector2>() { new Vector2(0,3), new Vector2(0,4),
            new Vector2(0,5), new Vector2(0,6), new Vector2(0,7),
            new Vector2(1,3), new Vector2(1,7),
            new Vector2(2,3), new Vector2(2,7),
            new Vector2(3,3), new Vector2(3,7),
            new Vector2(4,3), new Vector2(4,7),
            new Vector2(5,1), new Vector2(5,2), new Vector2(5,3), new Vector2(5,7), new Vector2(5,8),
            new Vector2(6,1), new Vector2(6,8),
            new Vector2(7,1),
            new Vector2(8,1), new Vector2(8,2), new Vector2(8,3), new Vector2(8,4), new Vector2(8,5), new Vector2(8,6), new Vector2(8,7), new Vector2(8,8)};
        private bool CheckIfRoad(Vector2 pos) {
            return roadSet.Contains(pos);
        }
    }
}
