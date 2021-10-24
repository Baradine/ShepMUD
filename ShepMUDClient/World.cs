using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    class World
    {
        public Region[] regions;
        int maxRegions;
        int regionCount;
        public World(int max)
        {
            this.maxRegions = max;
            regions = new Region[maxRegions];
            regionCount = 0;
        }

        public void NewRegion()
        {
            if (regionCount >= maxRegions)
            {
                return;
            }
            Region r = new Region();
            r.FillTileGrid();
            regions[regionCount++] = r;
        }
    }
}
