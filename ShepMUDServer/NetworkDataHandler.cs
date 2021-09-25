using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShepMUD
{
    class NetworkDataHandler
    {
        byte[] packet;
        int currentIndex;
        int maxLength;

        public void NewPacket(int length)
        {
            packet = new byte[length];
            currentIndex = 0;
            maxLength = length-1;
        }

        public void AddDataToPacket(byte[] data)
        {
            foreach (byte b in data)
            {
                if (currentIndex <= maxLength)
                {
                    packet[currentIndex] = b;
                    currentIndex++;
                }
            }
        }

        public byte[] GetPacket()
        {
            return packet;
        }
    }
}
