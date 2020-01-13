using BT_Server.utils;
using BT_Server.Interfaces;
using BT_Server.libs.Serialization;
using BT_Server.Packets;

namespace BT_Server.Factories
{
    public class PacketFactory
    {
        public static IPacket Build(byte packetType, params object[] args)
        {
            switch (packetType)
            {
                case PacketType.LOGIN_PKT:
                    return new LoginPacket(args);
                case PacketType.LOGOUT_PKT:
                    return new LogoutPacket(args);
                case PacketType.MOVEMENT_PKT:
                    return new MovementPacket(args);
                case PacketType.FIRE_PKT:
                    return new FirePacket(args);
                default:
                    return null;
            }
        }
    }
}