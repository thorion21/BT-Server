using BT_WorldServer.utils;
using BT_WorldServer.Interfaces;
using BT_WorldServer.libs.Serialization;
using BT_WorldServer.Packets;

namespace BT_WorldServer.Factories
{
    public class PacketFactory
    {
        public static IPacket Build(byte packetType, params object[] args)
        {
            switch (packetType)
            {
                case PacketType.LOGIN_RSP_PKT:
                    return new LoginResponse(args);
                case PacketType.LOGOUT_RSP_PKT:
                    return new LogoutResponse(args);
                default:
                    return null;
            }
        }
    }
}