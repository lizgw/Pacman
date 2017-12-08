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

        public void Update()
        {
            switch (direction)
            {
                case -1: //this represents staying in place
                    break;
                case Game1.UP:
                    y -= speed;
                    break;
                case Game1.LEFT:
                    x -= speed;
                    break;
                case Game1.RIGHT:
                    x += speed;
                    break;
                case Game1.DOWN:
                    y += speed;
                    break;
            }

            //if it passes an intersection, it will snap back to the intersection, change its direction and move in that direction the amount of lost distance
            int distanceFromLastTile = DistanceFromLastTile();
            if (distanceFromLastTile > Game1.TILE_SIZE || direction == -1)
            {
                switch (direction)
                {
                    case -1: //this represents staying in place
                        break;
                    case Game1.UP:
                        y = Map.TileToCoordinate(tileY - 1);
                        break;
                    case Game1.LEFT:
                        x = Map.TileToCoordinate(tileX - 1);
                        break;
                    case Game1.RIGHT:
                        x = Map.TileToCoordinate(tileX + 1);
                        break;
                    case Game1.DOWN:
                        y = Map.TileToCoordinate(tileY + 1);
                        break;
                }

                if (direction != -1)
                {
                    //update tileX and tileY so we can use them at the next intersection
                    tileX = Map.CoordinateToTile((int)x);
                    tileY = Map.CoordinateToTile((int)y);
                }

                bool wasMoving = direction != -1; //this is only used to make it not do anything with turnDistance
                direction = NextDirection();
                if (wasMoving)
                {
                    float turnDistance = distanceFromLastTile - Game1.TILE_SIZE; //this is the amount of distance left over it should travel in this frame in the new direction
                    switch (direction)
                    {
                        case -1: //this represents staying in place
                            break;
                        case Game1.UP:
                            y -= turnDistance;
                            break;
                        case Game1.LEFT:
                            x -= turnDistance;
                            break;
                        case Game1.RIGHT:
                            x += turnDistance;
                            break;
                        case Game1.DOWN:
                            y += turnDistance;
                            break;
                    }
                }
            }

            //update position of destRect
            destRect.X = (int)x - Game1.TILE_SIZE / 2;
            destRect.Y = (int)y - Game1.TILE_SIZE / 2;
        }

        public abstract void Draw(SpriteBatch sb);

        protected abstract short NextDirection();

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

        protected void checkWrap()
        {
            // wrap around left/right
            if (tileX == 0 && direction == Game1.LEFT)
            {
                //Console.WriteLine("wrap left to right");
            }
            if (tileX >= 19 && direction == Game1.RIGHT)
            {
                //Console.WriteLine("wrap right to left");
            }
        }
    }
}
