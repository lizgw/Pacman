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
using System.Diagnostics;

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const short UP = 0;
        public const short RIGHT = 1;
        public const short DOWN = 2;
        public const short LEFT = 3;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Mover[] movers;
        Map map;
        Texture2D tempTexture;
        int score;

        public Texture2D tileBlank;
        public Texture2D tileWall;
        public Texture2D tilePoint;
        public Texture2D tilePowerup;
        public Texture2D tileFruit;

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
            tileBlank = Content.Load<Texture2D>("tile_blank");
            tileWall = Content.Load<Texture2D>("tile_wall");
            tilePoint = Content.Load<Texture2D>("tile_point");
            tilePowerup = Content.Load<Texture2D>("tile_powerup");
            tileFruit = Content.Load<Texture2D>("tile_fruit");
            tempTexture = Content.Load<Texture2D>("white");
            movers = new Mover[5];
            Reset();

            base.Initialize();
        }

        private void Reset()
        {
            map = new Map(this);
            movers[0] = new PacMan(this, 10, 5, tempTexture);
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
            map.Update();
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

            //Todo: replace this with graphically displayed score
            //Debug.WriteLine("Score: " + score);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public int GetTimer()
        {
            return timer;
        }

        public Map GetMap()
        {
            return map;
        }

        public void addPoints(int numPoints)
        {
            score += numPoints;
        }
    }
}