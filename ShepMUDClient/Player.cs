using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ShepMUDClient
{
    static class Player
    {
        public static Creature creature;
        public static TileMap playerMap;

        // Will take in actual variables that we get from the server later, for now, this is just a test init.
        public static void InitPlayer(TileMap map)
        {
            creature = new Creature();
            creature.region = Universe.currentWorld.regions[0];
            creature.region.tileGrid[0, 0].currentEntities.Add(creature);
            creature.xPos = 0;
            creature.yPos = 0;
            creature.permission = new Permission { ownerIDs = new uint[] { 0 } };
            playerMap = map;
            creature.PositionUpdated += PositionUpdated;
            playerMap.UpdateMap(creature.region.tileGrid, creature.region.tileGrid[creature.xPos, creature.yPos]);
        }

        public static void PositionUpdated(object sender, EventArgs a)
        {
            playerMap.UpdateMap(creature.region.tileGrid, creature.region.tileGrid[creature.xPos,creature.yPos]);
        }
    }
}
