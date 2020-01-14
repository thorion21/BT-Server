using BT_GameServer.utils;
using BT_GameServer.Interfaces;
using BT_GameServer.libs.Serialization;
using BT_GameServer.Packets;

namespace BT_GameServer.Factories
{
    public class PacketFactory
    {
        public static IPacket Build(byte packetType, params object[] args)
        {
            switch (packetType)
            {
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