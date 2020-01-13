using BT_Server.Interfaces;

namespace BT_Server.Packets
{
    public class LogoutResponse : DefaultPacket, IPacket
    {
        public LogoutResponse(params object[] args)
        {
            
        }
    }
}