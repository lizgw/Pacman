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
     abstract class Mover
    {
        public const short UP = 0;
        public const short RIGHT = 1;
        public const short DOWN = 2;
        public const short LEFT = 3;

        protected Game1 game; // a reference to the current game

        protected float x; // actual x position
        protected float y; // actual y position
        protected int tileX; // current tile X
        protected int tileY; // current tile Y

        protected float speed;
        protected short direction;

        protected Rectangle destRect;
        protected Texture2D texture;

        public Mover(Game1 game, int tileX, int tileY, Texture2D texture)
        {
            this.game = game;

            this.tileX = tileX;
            this.tileY = tileY;
            x = Map.TileToCoordinates(tileX, tileY)[0];
            y = Map.TileToCoordinates(tileX, tileY)[1];

            direction = RIGHT;

            destRect = new Rectangle((int)x, (int)y, Game1.TILE_SIZE, Game1.TILE_SIZE);
            this.texture = texture;
        }

        public abstract void Update();
        public abstract void Draw(SpriteBatch sb);

        public void increaseSpeed(float amount)
        {
            speed += amount;
        }
    }
}
