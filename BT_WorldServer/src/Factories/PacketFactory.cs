using BT_WorldServer.Interfaces;
using BT_WorldServer.Packets;

namespace BT_WorldServer.Factories
{
    public class PacketFactory
    {
        public static DefaultPacket Build(byte packetType, ISerialize classObject)
        {
            /*
             * Receives a packet type and a class and serializes the class
             * inside the bytes[] attribute of the DefaultPacket
             */
            return new DefaultPacket(packetType, classObject.AsByteArray());
        }
    }
}