using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pacman
{
    class Ghost
    {
        private Game1 game; // reference to the game it's in
        private int speed;
        private Vector2 pos;

        private Rectangle rect;
        private Texture2D texture;

        public Ghost(int xPos, int yPos, Texture2D texture)
        {
            this.pos = new Vector2(xPos, yPos);
            this.rect = new Rectangle(xPos, yPos, 32, 32);
            this.texture = texture;
            this.speed = 1;
        }

        public void Update()
        {
            // check keyboard input
            // move according to input
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, this.rect, Color.White);
        }

        public void increaseSpeed(int amount)
        {
            speed += amount;
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
