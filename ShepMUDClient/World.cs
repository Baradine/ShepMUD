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
        public string name;
        public int ID
        {
            get;
        }
        public World(int max)
        {
            this.maxRegions = max;
            regions = new Region[maxRegions];
            regionCount = 0;
            name = "default";
            if (Universe.worlds != null)
            {
                int index = 1;
                foreach (World w in Universe.worlds)
                {
                    index += w.ID;
                }
                this.ID = index;
            }
            else
            {
                this.ID = 1;
            }
        }

        public void NewRegion()
        {
            if (regionCount >= maxRegions)
            {
                return;
            }
            Region r = new Region(this);
            r.FillTileGrid();
            regions[regionCount++] = r;
        }
    }
}
