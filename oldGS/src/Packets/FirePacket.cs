using BT_Server.Interfaces;
using BT_Server.libs.Serialization;

namespace BT_Server.Packets
{
    public class FirePacket : DefaultPacket, IPacket
    {
        public FirePacket(params object[] args)
        {
            
        }
    }
}