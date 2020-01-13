using BT_Server.Interfaces;

namespace BT_Server.Packets
{
    public class LogoutPacket : DefaultPacket, IPacket
    {
        public LogoutPacket(params object[] args)
        {
            
        }
    }
}