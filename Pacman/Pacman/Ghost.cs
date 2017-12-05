using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Ghost : Mover
    {
       public Ghost(Game1 aGame, int aTileX, int aTileY, Texture2D aTexture) : base(aGame, aTileX, aTileY, aTexture)
        {
            speed = 1;
            direction = RIGHT;
        }

        override public void Update()
        {
            // move & update position
            // check tile (wall or free)
            // if at an intersection, pick a direction
            // collisions, etc...
        }

        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destRect, Color.White);
        }

        // picks the next direction & returns it
        public int NextDirection()
        {
            int direction = 0;

            // TODO: implement this

            return direction;
        }
    }
}
