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
        public static World currentWorld;


        /// <summary>
        /// This is a test class for offline maps.  Deployment initialization will happen when connecting to the server
        /// </summary>
        public static void InitUniverse()
        {
            World A = new World(1);
            A.NewRegion();
            worlds = new World[1] { A };
            currentWorld = worlds[0];
        }

    }
}
