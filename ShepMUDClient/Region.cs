using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    class Region
    {
        Tile[,] tileGrid;
        string name;
        int tileGridWidth;
        int tileGridHeight;

        public Region()
        {
            name = "default";
            tileGridWidth = 4;
            tileGridHeight = 4;
            tileGrid = new Tile[tileGridWidth, tileGridHeight];
        }

        public Region(string n, int w, int h)
        {
            name = n;
            tileGridWidth = w;
            tileGridHeight = h;
            tileGrid = new Tile[tileGridWidth, tileGridHeight];
        }

        /// <summary>
        /// Populates the current tileGrid with empty tiles
        /// </summary>
        public void FillTileGrid()
        {
            for (int i = 0; i < tileGridWidth; i++)
            {
                for (int k = 0; k < tileGridHeight; k++)
                {
                    tileGrid[i, k] = new Tile { xPos = i, yPos = k };
                }
            }
        }
    }
}
