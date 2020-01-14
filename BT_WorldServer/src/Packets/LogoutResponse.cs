using BT_WorldServer.Interfaces;

namespace BT_WorldServer.Packets
{
    public class LogoutResponse : DefaultPacket, IPacket
    {
        public LogoutResponse(params object[] args)
        {
            
        }
    }
}