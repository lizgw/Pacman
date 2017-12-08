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
    class PacMan : Mover
    {
        KeyboardState kb;

        public PacMan(Game1 aGame, int aTileX, int aTileY, Texture2D aTexture) : base(aGame, aTileX, aTileY, aTexture)
        {
            speed = 4;
            destRect = new Rectangle(0, 0, Game1.TILE_SIZE, Game1.TILE_SIZE);
            kb = Keyboard.GetState();
        }

        public PacMan(Game1 aGame, int aTileX, int aTileY, Texture2D aTexture, int aSpeed) : base(aGame, aTileX, aTileY, aTexture)
        {
            speed = aSpeed;
            destRect = new Rectangle(0, 0, Game1.TILE_SIZE, Game1.TILE_SIZE);
            kb = Keyboard.GetState();
        }

        override public void Update()
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

                //update tileX and tileY so we can use them at the next intersection
                tileX = Map.CoordinateToTile((int)x);
                tileY = Map.CoordinateToTile((int)y);

                direction = NextDirection();
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

            //update position of destRect
            destRect.X = (int)x - Game1.TILE_SIZE / 2;
            destRect.Y = (int)y - Game1.TILE_SIZE / 2;
        }

        protected short NextDirection()
        {
            kb = Keyboard.GetState();
            short[] surroundingTiles = game.GetMap().GetSurroundingTiles(tileX, tileY);
            Debug.WriteLine("X: " + tileX + " Y:" + tileY);
            Debug.WriteLine(surroundingTiles[0] + " " + surroundingTiles[1] + " " + surroundingTiles[2] + " " + surroundingTiles[3] + " " + surroundingTiles[4]);

            // change direction according to kb
            if (kb.IsKeyDown(Keys.Up) && direction != Game1.DOWN && surroundingTiles[Game1.UP] != Map.WALL)
            {
                return Game1.UP;
            }
            if (kb.IsKeyDown(Keys.Down) && direction != Game1.UP && surroundingTiles[Game1.DOWN] != Map.WALL)
            {
                return Game1.DOWN;
            }
            if (kb.IsKeyDown(Keys.Left) && direction != Game1.RIGHT && surroundingTiles[Game1.LEFT] != Map.WALL)
            {
                return Game1.LEFT;
            }
            if (kb.IsKeyDown(Keys.Right) && direction != Game1.LEFT && surroundingTiles[Game1.RIGHT] != Map.WALL)
            {
                return Game1.RIGHT;
            }

            if (direction == -1 || surroundingTiles[direction] == Map.WALL)
                return -1; //this can represent staying in place
            else
                return direction;
        }

        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destRect, Color.Yellow);
        }
    }
}
