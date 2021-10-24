using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    class Tile
    {
        public int xPos;
        public int yPos;

        public List<Entity> currentEntities;

        public Tile()
        {
            currentEntities = new List<Entity>();
        }
    }
}
