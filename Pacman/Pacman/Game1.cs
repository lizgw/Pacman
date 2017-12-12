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

        public Texture2D tempTexture;

        int score;
        
        public Texture2D tileBlank;
        public Texture2D tileWall;
        public Texture2D tilePoint;
        public Texture2D tilePowerup;
        public Texture2D tileFruit;
        public Texture2D title;
        public Texture2D start_button;
        public Texture2D title_pac;
        int timer = 0; //general timer we can use to time in-game actions
        int powerup_time = 0; //this timer is used for ending the power up 
        bool game_started;
        public const int TILE_SIZE = 32;

        public SpriteFont font1;

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
            IsMouseVisible = true;
            tileBlank = Content.Load<Texture2D>("tile_blank");
            tileWall = Content.Load<Texture2D>("tile_wall");
            tilePoint = Content.Load<Texture2D>("tile_point");
            tilePowerup = Content.Load<Texture2D>("tile_powerup");
            tileFruit = Content.Load<Texture2D>("tile_fruit");
            tempTexture = Content.Load<Texture2D>("white");
            movers = new Mover[5];
            Reset();
            // game_started = false;
            base.Initialize();
        }

        private void Reset()
        {
            map = new Map(this);
            movers[0] = new PacMan(this, 10, 5, tempTexture);
            //movers[1] = new Ghost(this, 19, 19, tempTexture);
            movers[2] = new Ghost(this, 1, 1, tempTexture);

            //movers[3] = new Ghost(this, 1, 19, tempTexture);
            //movers[4] = new Ghost(this, 19, 1, tempTexture);

            movers[3] = new Ghost(this, 1, 19, tempTexture);
            movers[4] = new Ghost(this, 19, 1, tempTexture);
            game_started = false;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            title = this.Content.Load<Texture2D>("title");
            start_button = this.Content.Load<Texture2D>("start_button");
            title_pac = this.Content.Load<Texture2D>("title_pac");
            font1 = this.Content.Load<SpriteFont>("font1");
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
            MouseState mouse = Mouse.GetState();
            map.Update();
            foreach (Mover mover in movers)
            {
                if (mover != null)
                    mover.Update();
            }
            if (mouse.X > 160 && mouse.X < 410 && mouse.Y > 245 && mouse.Y < 395 && mouse.LeftButton == ButtonState.Pressed)
            {
                game_started = true;
            }
            timer++;
            
            if (PacMan.pacman_powerup)
            {
                powerup_time++;
                if (powerup_time==300)
                {
                    PacMan.pacman_powerup = false;
                    powerup_time = 0;

                }
            }
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
            if (game_started)
            {
                map.Draw(spriteBatch);
                foreach (Mover mover in movers)
                {
                    if (mover != null)
                        mover.Draw(spriteBatch);
                }
            }
            else
            {
                spriteBatch.Draw(title, new Rectangle(170 + 85, 200, 500, 300), new Rectangle(0, 0, title.Width, title.Height), Color.White, 0, new Vector2(250, 150), SpriteEffects.None, 0);
                spriteBatch.Draw(start_button, new Rectangle(170 + 115, 170 + 150, 250, 150), new Rectangle(0, 0, start_button.Width, start_button.Height), Color.White, 0, new Vector2(125, 75), SpriteEffects.None, 0);
                spriteBatch.Draw(title_pac, new Rectangle(0, 190, 200, 300), new Rectangle(0, 0, title_pac.Width, title_pac.Height), Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(title_pac, new Rectangle(620, 190, 200, 300), new Rectangle(0, 0, title_pac.Width, title_pac.Height), Color.White, 0, new Vector2(200, 0), SpriteEffects.None, 0);
            }

            //Todo: replace this with graphically displayed score
            spriteBatch.DrawString(font1, "" + score, new Vector2(10, 2), Color.White);

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


        public void AddPoints(int numPoints)
        {
            score += numPoints;
        }

        public static short OppositeDirection(short dir)
        {
            switch (dir)
            {
                case UP:
                    return DOWN;
                case RIGHT:
                    return LEFT;
                case DOWN:
                    return UP;
                case LEFT:
                    return RIGHT;
                default:
                    return -1;
            }
        }    
    }
}