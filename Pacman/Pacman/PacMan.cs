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
        Game1 game;

        float x;
        float y;
        int tileX;
        int tileY;

        float speed;
        short direction;

        Rectangle destRect;
        int spriteRadius;

        public PacMan(Game1 aGame, int aTileX, int aTileY)
        {
            game = aGame;
            x = game.getMap().TileToCoordinate(tileX);
            y = game.getMap().TileToCoordinate(tileY);
            tileX = aTileX;
            tileY = aTileY;
            speed = 4;
            direction = RIGHT;

            destRect = new Rectangle(0, 0, 2 * spriteRadius, 2 * spriteRadius);
        }

        public PacMan(Game1 aGame, int aTileX, int aTileY, int aSpeed)
        {
            game = aGame;
            x = game.getMap().TileToCoordinate(tileX);
            y = game.getMap().TileToCoordinate(tileY);
            tileX = aTileX;
            tileY = aTileY;
            speed = aSpeed;
            direction = RIGHT;

            destRect = new Rectangle(0, 0, 2 * spriteRadius, 2 * spriteRadius);
        }

        protected void Update()
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
            if (distanceFromLastTile > game.getMap().getTileSize())
            {
                switch (direction)
                {
                    case UP:
                        y = TileToCoordinate(tileY - 1);
                        break;
                    case LEFT:
                        x = TileToCoordinate(tileX - 1);
                        break;
                    case RIGHT:
                        x = TileToCoordinate(tileX + 1);
                        break;
                    case DOWN:
                        y = TileToCoordinate(tileY + 1);
                        break;
                }
                direction = NextDirection();
                float turnDistance = distanceFromLastTile - game.getMap().getTileSize(); //this is the amount of distance left over it should travel in this frame in the new direction
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

        protected void Draw()
        {

        }
    }
}
