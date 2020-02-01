using System;
using BT_WorldServer.Interfaces;
using MessagePack;

namespace BT_WorldServer.Packets
{
    [MessagePackObject]
    public class DefaultPacket : ISerialize
    {
        [Key(0)] public byte PacketType;
        [Key(1)] public byte[] Buffer;

        [SerializationConstructor]
        public DefaultPacket(byte packetType, byte[] bytes)
        {
            Console.WriteLine("Default Packet constructor called.");
            PacketType = packetType;
            Buffer = bytes;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
    }
}