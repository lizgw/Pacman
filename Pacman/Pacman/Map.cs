using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    class Map
    {
        public const int BLANK = 0;
        public const int WALL = 1;
        public const int POINT = 2;
        public const int POWERUP = 3;
        public const int FRUIT = 4;

        private Game1 game;

        // TODO: replace with actual map
        public static int[,] initMap = new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 2, 3, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };

        // takes a tile from the array and returns the center of that tile in game space as [x, y]
        public static int[] TileToCoordinates(int tileX, int tileY)
        {
            int[] coord = new int[] { -1, -1 };

            // TODO: implement this

            return coord;
        }

        // takes world coordinates and returns the tile that lies there as [row, column] in the array
        public static int[] CoordinatesToTile(int coordX, int coordY)
        {
            int[] tile = new int[] { -1, -1 };

            // TODO: implement this

            return tile;
        }

        private int[,] map;

        public Map(Game1 game)
        {
            // setup map with default map to start with
            this.map = initMap;
            this.game = game;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    // draw tile
                    int tileType = map[r, c];
                    Texture2D texture = null;
                    Rectangle rect = new Rectangle(Game1.TILE_SIZE * c, Game1.TILE_SIZE * r, Game1.TILE_SIZE, Game1.TILE_SIZE);
                    Color tileColor = Color.White;

                    switch(tileType)
                    {
                        case BLANK:
                            texture = this.game.tempTexture;
                            tileColor = Color.Black;
                            break;
                        case WALL:
                            texture = this.game.tempTexture;
                            tileColor = Color.Blue;
                            break;
                        case POINT:
                            texture = this.game.tempTexture;
                            tileColor = Color.Yellow;
                            break;
                        case POWERUP:
                            texture = this.game.tempTexture;
                            tileColor = Color.Red;
                            break;
                        case FRUIT:
                            texture = this.game.tempTexture;
                            tileColor = Color.Green;
                            break;
                    }

                    sb.Draw(texture, rect, tileColor);
                }
            }
        }
    }
}
