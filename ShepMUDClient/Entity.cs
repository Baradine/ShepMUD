using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUDClient
{
    class Entity
    {
        public Region region;
        public bool sendPositionEvent = false;
        private int _x;
        public int xPos
        {
            get { return _x; }
            set
            {
                _x = value;
                if (PositionUpdated != null)
                {
                    PositionUpdated(this, EventArgs.Empty);
                }
                
            }
        }
        private int _y;
        public int yPos
        {
            get { return _y; }
            set
            {
                _y = value;
                if (PositionUpdated != null)
                {
                    PositionUpdated(this, EventArgs.Empty);
                }
            }
        }
        public Permission permission;

        public event EventHandler PositionUpdated;

    }

    


    struct Permission
    {
        // public key determines permissions for non-owners
        public int publicKey;
        public int privateKey;
        public enum PermissionKey
        {
            None = 1,
            Interact = 2,
            Control = 4
        }
        public uint[] ownerIDs;  // Owner ID 0 is admin, and shouldn't be added to the list, but given an override in checks instead.
    }
}
