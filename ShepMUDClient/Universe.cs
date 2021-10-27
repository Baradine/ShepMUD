using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    static class Universe
    {
        public static World[] worlds;
        // Current world is the world the player is currently in (not the server default world or anything else)
        public static World currentWorld;
        public static List<int> regionIDs;

        /// <summary>
        /// This is a test class for offline maps.  Deployment initialization will happen when connecting to the server
        /// </summary>
        public static void InitUniverse()
        {
            regionIDs = new List<int>();
            World A = new World(1);
            A.NewRegion();
            worlds = new World[1] { A };
            currentWorld = worlds[0];
        }

        public static Region GetCurrentWorldRegionByID(int id)
        {
            if (regionIDs == null || worlds == null || currentWorld == null)
            {
                return null;
            }
            foreach (Region r in currentWorld.regions)
            {
                if (r.ID == id)
                {
                    return r;
                }
            }
            return null;
        }

    }
}
