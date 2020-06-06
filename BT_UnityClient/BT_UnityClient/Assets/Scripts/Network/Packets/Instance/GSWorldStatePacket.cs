using System.Collections.Generic;
using ENet;
using Interfaces;
using MessagePack;
using utils;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class GSWorldStatePacket : ISerialize
    {
        [Key(0)] public Dictionary<string, NetworkVector> data;

        [SerializationConstructor]
        public GSWorldStatePacket(Dictionary<string, NetworkVector> _data)
        {
            data = _data;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static GSWorldStatePacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<GSWorldStatePacket>(bytes);
        }
    }
    
}