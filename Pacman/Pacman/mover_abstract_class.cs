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
        public Game1 game; // a reference to the current game

        public const short UP = 0;
        public const short RIGHT = 1;
        public const short DOWN = 2;
        public const short LEFT = 3;

        public float x; // actual x position
        public float y; // actual y position
        public int tileX; // current tile X
        public int tileY; // current tile Y

        public float speed;
        public short direction;

        public Rectangle destRect;
        public Texture2D texture;

        public Mover(Game1 game, int tileX, int tileY, Texture2D texture)
        {
            this.game = game;

            this.tileX = tileX;
            this.tileY = tileY;
            this.x = Map.TileToCoordinates(tileX, tileY)[0];
            this.y = Map.TileToCoordinates(tileX, tileY)[1];

            this.direction = RIGHT;

            this.destRect = new Rectangle((int)this.x, (int)this.y, Game1.TILE_SIZE, Game1.TILE_SIZE);
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
