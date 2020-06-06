using Interfaces;
using System.Collections.Generic;
using MessagePack;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class GSUpdateHealthPacket : ISerialize
    {
        [Key(0)] public int amount;

        [SerializationConstructor]
        public GSUpdateHealthPacket(int _amount)
        {
            amount = _amount;
        }

        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static GSUpdateHealthPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<GSUpdateHealthPacket>(bytes);
        }
    }
}