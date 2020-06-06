using Interfaces;
using System.Collections.Generic;
using MessagePack;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class GSClientInitPacket : ISerialize
    {
        [Key(0)] public int instance_id;

        public GSClientInitPacket(int _id)
        {
            instance_id = _id;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static GSClientInitPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<GSClientInitPacket>(bytes);
        }
    }
}