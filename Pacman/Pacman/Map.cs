using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman
{
    public class Map
    {
        public const int BLANK = 0;
        public const int WALL = 1;
        public const int POINT = 2;
        public const int POWERUP = 3;
        public const int FRUIT = 4;

        private Game1 game;
        private short[,] map;
        private Texture2D tempTexture;

        public Map(Game1 aGame)
        {
            // setup map with default map to start with
            map = initMap;
            game = aGame;
            tempTexture = game.Content.Load<Texture2D>("white");
        }

        //will this ever need to get an implementation?
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
                            texture = this.game.tileBlank;
                            tileColor = Color.Black;
                            break;
                        case WALL:
                            texture = this.game.tileWall;
                            tileColor = Color.Blue;
                            break;
                        case POINT:
                            texture = this.game.tilePoint;
                            tileColor = Color.Yellow;
                            break;
                        case POWERUP:
                            texture = this.game.tilePowerup;
                            tileColor = Color.Red;
                            break;
                        case FRUIT:
                            texture = this.game.tileFruit;
                            tileColor = Color.Green;
                            break;
                    }

                    sb.Draw(texture, rect, tileColor);
                }
            }
        }
        
        public static short[,] initMap = new short[,] {

            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },//1
            { 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1 },//2
            { 1, 2, 1, 1, 2, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 2, 2, 2, 2, 1 },//3
            { 1, 2, 1, 3, 2, 2, 2, 2, 1, 2, 2, 1, 2, 2, 2, 2, 1, 1, 2, 1 },//4
            { 2, 2, 1, 2, 1, 1, 1, 2, 1, 2, 2, 1, 2, 1, 1, 2, 3, 1, 2, 2 },//5
            { 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 1, 2, 1 },//6
            { 1, 2, 1, 1, 2, 1, 2, 2, 1, 2, 1, 1, 2, 2, 1, 2, 2, 2, 2, 1 },//7
            { 1, 2, 1, 1, 2, 2, 2, 1, 1, 2, 1, 2, 2, 2, 2, 1, 1, 1, 2, 1 },//8
            { 1, 2, 2, 2, 2, 1, 2, 1, 1, 0, 0, 1, 1, 1, 2, 1, 1, 1, 2, 1 },//9
            { 1, 1, 1, 2, 2, 2, 2, 1, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 1 },//10
            { 1, 1, 1, 2, 2, 1, 2, 1, 0, 0, 0, 0, 1, 1, 2, 1, 1, 1, 1, 1 },//11
            { 2, 2, 2, 2, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 2, 2, 2 },//12
            { 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 2, 1, 2, 1 },//13
            { 1, 2, 1, 1, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 1 },//14
            { 1, 2, 2, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 2, 2, 1 },//15
            { 1, 1, 2, 2, 1, 2, 2, 2, 2, 2, 1, 1, 2, 2, 2, 1, 1, 2, 1, 1 },//16
            { 1, 1, 1, 2, 1, 2, 1, 1, 1, 2, 1, 1, 2, 2, 1, 3, 1, 2, 1, 1 },//17
            { 1, 3, 1, 2, 1, 2, 2, 1, 2, 2, 1, 1, 1, 2, 1, 2, 1, 2, 1, 1 },//18
            { 1, 2, 2, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 1, 1, 2, 2, 2, 2, 1 },//19
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }//20

        };

        public static int TileToCoordinate(int tile)
        {
            return (tile * Game1.TILE_SIZE) + Game1.TILE_SIZE / 2;
        }

        public static int CoordinateToTile(int coord)
        {
            return coord / Game1.TILE_SIZE;
        }

        // takes the map and the current position as [x, y], returns [up, right, down, left]
        public short[] GetSurroundingTiles(int xVal, int yVal)
        {
            short[] coords = new short[5];

            coords[Game1.UP] = map[yVal - 1, xVal];
            coords[Game1.RIGHT] = map[yVal, xVal + 1];
            coords[Game1.DOWN] = map[yVal + 1, xVal];
            coords[Game1.LEFT] = map[yVal, xVal - 1];

            coords[4] = map[yVal, xVal];

            return coords;
        }
    }
}
