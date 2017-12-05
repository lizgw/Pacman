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
    class PacMan : Mover
    {
        int spriteRadius;

        public PacMan(Game1 aGame, int aTileX, int aTileY, Texture2D aTexture) : base(aGame, aTileX, aTileY, aTexture)
        {
            speed = 4;
        }

        public PacMan(Game1 aGame, int aTileX, int aTileY, Texture2D aTexture, int aSpeed) : base(aGame, aTileX, aTileY, aTexture)
        {
            speed = aSpeed;
        }

        override public void Update()
        {
            switch (direction)
            {
                case UP:
                    y -= speed;
                    break;
                case LEFT:
                    x -= speed;
                    break;
                case RIGHT:
                    x += speed;
                    break;
                case DOWN:
                    y += speed;
                    break;
            }

            //if it passes an intersection, it will snap back to the intersection, change its direction and move in that direction the amount of lost distance
            int distanceFromLastTile = DistanceFromLastTile();
            if (distanceFromLastTile > Game1.TILE_SIZE)
            {
                switch (direction)
                {
                    case UP:
                        y = Map.TileToCoordinates(tileX, tileY - 1)[1];
                        break;
                    case LEFT:
                        x = Map.TileToCoordinates(tileX - 1, tileY)[0];
                        break;
                    case RIGHT:
                        x = Map.TileToCoordinates(tileX + 1, tileY)[0];
                        break;
                    case DOWN:
                        y = Map.TileToCoordinates(tileX, tileY + 1)[1];
                        break;
                }
                direction = NextDirection();
                float turnDistance = distanceFromLastTile - Game1.TILE_SIZE; //this is the amount of distance left over it should travel in this frame in the new direction
                switch (direction)
                {
                    case UP:
                        y -= turnDistance;
                        break;
                    case LEFT:
                        x -= turnDistance;
                        break;
                    case RIGHT:
                        x += turnDistance;
                        break;
                    case DOWN:
                        y += turnDistance;
                        break;
                }
            }

            //update position of destRect
            destRect.X = (int)x - spriteRadius;
            destRect.Y = (int)y - spriteRadius;
        }

        protected short NextDirection()
        {
            return RIGHT; //this is a temporary solution to see if it works
        }

        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destRect, Color.White);
        }

        private int DistanceFromLastTile()
        {
            // TODO: implement this
            return -1;
        }
    }
}
