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
            x = Map.TileToCoordinate(tileX);
            y = Map.TileToCoordinate(tileY);

            direction = Game1.RIGHT;

            destRect = new Rectangle((int)x, (int)y, Game1.TILE_SIZE, Game1.TILE_SIZE);
            this.texture = texture;
        }

        public abstract void Update();
        public abstract void Draw(SpriteBatch sb);

        public void increaseSpeed(float amount)
        {
            speed += amount;
        }

        protected int DistanceFromLastTile()
        {
            switch (direction)
            {
                case Game1.UP:
                    return Map.TileToCoordinate(tileY) - (int)y;
                case Game1.LEFT:
                    return Map.TileToCoordinate(tileX) - (int)x;
                case Game1.RIGHT:
                    return (int)x - Map.TileToCoordinate(tileX);
                case Game1.DOWN:
                    return (int)y - Map.TileToCoordinate(tileY);
                default:
                    return -1;
            }
        }
    }
}
