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
using System.Collections;

namespace Pacman
{
    class Ghost : Mover
    {
        KeyboardState kb;

        public Ghost(Game1 aGame, int aTileX, int aTileY, Texture2D aTexture) : base(aGame, aTileX, aTileY, aTexture)
        {
            speed = 3.5f;
            destRect = new Rectangle(0, 0, Game1.TILE_SIZE, Game1.TILE_SIZE);
            kb = Keyboard.GetState();
        }

        public Ghost(Game1 aGame, int aTileX, int aTileY, Texture2D aTexture, int aSpeed) : base(aGame, aTileX, aTileY, aTexture)
        {
            speed = aSpeed;
            destRect = new Rectangle(0, 0, Game1.TILE_SIZE, Game1.TILE_SIZE);
            kb = Keyboard.GetState();
        }

        protected override void IntersectionActions()
        {
            //doesn't need to be implemented for now or possibly ever
        }

        override protected short[] DirectionPreferences()
        {
            ArrayList tempDirs = new ArrayList();
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.Up))
                tempDirs.Add(Game1.UP);
            if (kb.IsKeyDown(Keys.Right))
                tempDirs.Add(Game1.RIGHT);
            if (kb.IsKeyDown(Keys.Down))
                tempDirs.Add(Game1.DOWN);
            if (kb.IsKeyDown(Keys.Left))
                tempDirs.Add(Game1.LEFT);

            short[] output = new short[tempDirs.Count];
            for (int i = 0; i < tempDirs.Count; i++)
            {
                output[i] = (short)tempDirs[i];
            }

            return output;
        }

        override public void Draw(SpriteBatch sb)
        {
           
            if (PacMan.pacman_powerup)
            {
                sb.Draw(texture, destRect, Color.Blue);
            }
            else
            {
                sb.Draw(texture, destRect, Color.Yellow);
            }
            
            
            
        }
    }
}
