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

        new protected void Update()
        {
            base.Update();
        }
        
        override protected short NextDirection()
        {
            //this part can be used to implement any activities that need to be done once per intersection
            if (direction != -1)
            {
                //change the newest tile to a blank and collect points
                game.GetMap().ChangeToBlank(tileX, tileY);
                game.addPoints(100);
            }

            kb = Keyboard.GetState();
            short[] surroundingTiles = game.GetMap().GetSurroundingTiles(tileX, tileY);

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
