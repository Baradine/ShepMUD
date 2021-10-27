using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    class Region
    {
        public Tile[,] tileGrid;
        string name;
        int tileGridWidth;
        int tileGridHeight;
        private World parentWorld;
        public int ID
        {
            get;
        }

        public Region(World parent)
        {
            name = "default";
            tileGridWidth = 4;
            tileGridHeight = 4;
            tileGrid = new Tile[tileGridWidth, tileGridHeight];
            parentWorld = parent;
            ID = CalculateID();
        }

        public Region(string n, int w, int h, World parent)
        {
            name = n;
            tileGridWidth = w;
            tileGridHeight = h;
            tileGrid = new Tile[tileGridWidth, tileGridHeight];
            parentWorld = parent;
            ID = CalculateID();
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

        public int GetParentWorldID()
        {
            return parentWorld.ID;
        }

        private int CalculateID()
        {
            if (Universe.regionIDs != null)
            {
                int index = 1;
                foreach (int i in Universe.regionIDs)
                {
                    index += i;
                }
                Universe.regionIDs.Add(index);
                return index;
            }
            else
            {
                Universe.regionIDs = new List<int>();
                Universe.regionIDs.Add(1);
                return 1;
            }
            
        }
    }
}
