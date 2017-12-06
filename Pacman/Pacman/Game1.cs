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

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Mover[] movers;
        public Texture2D tempTexture;
        Map map;

        int timer = 0; //general timer we can use to time in-game actions

        public const int TILE_SIZE = 32;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = TILE_SIZE * 20;
            graphics.PreferredBackBufferHeight = TILE_SIZE * 20;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            tempTexture = Content.Load<Texture2D>("white");
            movers = new Mover[5];
            Reset();

            base.Initialize();
        }

        private void Reset()
        {
            map = new Map(this);
            movers[0] = new PacMan(this, 10, 15, tempTexture);
            movers[1] = new Ghost(this, 19, 19, tempTexture);
            movers[2] = new Ghost(this, 1, 1, tempTexture);
            movers[3] = new Ghost(this, 1, 19, tempTexture);
            movers[4] = new Ghost(this, 19, 1, tempTexture);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            //
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach (Mover mover in movers)
            {
                mover.Update();
            }

            timer++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            map.Draw(spriteBatch);
            foreach (Mover mover in movers)
            {
                mover.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public int GetTimer()
        {
            return timer;
        }
    }
}