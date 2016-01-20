using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace MirrorShooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputManager input;

        
        public static Vector2 leftLowerBound = new Vector2(-50, -50);
        public static Vector2 rightUpperBound = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width + 50, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height); 
        public const int  WINDOW_WIDTH = 1280;
        public const int  WINDOW_HEIGHT = 720;
        public static SpriteFont Font1;
        

        InGame gameState;

        float deltaTime;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            input = new InputManager();

            Player.hitboxCircle = Content.Load<Texture2D>("circleTest");
            Player.textureRef = Content.Load<Texture2D>("player");
            Cell.texture = Content.Load<Texture2D>("gridcell");
            Chicken.textureRef = Content.Load<Texture2D>("chickenspritesheet");
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(Content.Load<Texture2D>("circleTest"));
            Selector.textureRef = Content.Load<Texture2D>("crosshair");
            Chicken.testc = Content.Load<Texture2D>("circleTest");
            ParticleEngine.textures = textures;
            Picture2D.pictureList.Add(Content.Load<Texture2D>("road"));
            Picture2D.pictureList.Add(Content.Load<Texture2D>("shootbutton"));
            Picture2D.pictureList.Add(Content.Load<Texture2D>("bullet"));
            Picture2D.pictureList.Add(Content.Load<Texture2D>("tutorial"));


            gameState = new InGame(1, this);

            Window.Title = "Chicken Thief";


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            

            Font1 = Content.Load<SpriteFont>("testFont");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        

        



        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
                this.Exit();

            //DELTATIME IS IN SECONDS
            deltaTime = gameTime.ElapsedGameTime.Milliseconds;
            
            InputManager.Instance.Update();

            gameState.Update(deltaTime);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            gameState.Draw(spriteBatch);
            
            // TODO: Add your drawing code here



            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void RestartGame()
        {
            gameState = new InGame(1, this);
        }
        
    }
}
