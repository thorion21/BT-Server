using BT_GameServer.Interfaces;
using BT_GameServer.libs.Serialization;

namespace BT_GameServer.Packets
{
    public class FirePacket : DefaultPacket, IPacket
    {
        public FirePacket(params object[] args)
        {
            
        }
    }
}