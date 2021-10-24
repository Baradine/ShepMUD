using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ShepMUDClient
{
    class TileMap : Map
    {
        Image[,] map;
        public TileMap(Window1 mainWindow) : base (mainWindow)
        {
            win = mainWindow;
            map = new Image[3, 3]
                {
                    { win.NWestTile, win.NorthTile, win.NEastTile },
                    { win.WestTile, win.CurrentLocationTile, win.EastTile },
                    { win.SWestTile, win.SouthTile, win.SEastTile }
                };
        }

        public void UpdateMap(Tile[,] currentGrid, Tile currentTile)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int yPos = currentTile.yPos + ((i-1) * -1);
                    int xPos = currentTile.xPos + (k-1);
                    if (xPos < 0 || xPos > currentGrid.GetLength(0) - 1 || yPos < 0 || yPos > currentGrid.GetLength(0) - 1)
                    {
                        SetTileVisibility(i, k, false);
                    }
                    else
                    {
                        SetTileVisibility(i, k, true);
                    }

                }
            }
        }

        public void ToggleTileVisibility(int x, int y)
        {
            if (map[x, y].IsVisible)
            {
                map[x, y].Visibility = Visibility.Hidden;
            }
            else
            {
                map[x, y].Visibility = Visibility.Visible;
            }
        }

        public void SetTileVisibility(int x, int y, bool vis)
        {
            if (vis && !map[x, y].IsVisible)
            {
                map[x, y].Visibility = Visibility.Visible;
            }
            else if (!vis && map[x, y].IsVisible)
            {
                map[x, y].Visibility = Visibility.Hidden;
            }
        }


        
    }
}
