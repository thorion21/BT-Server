using Interfaces;
using System.Collections.Generic;
using MessagePack;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class GSConfirmIdentityPacket : ISerialize
    {
        [Key(0)] public int instance_id;
        [Key(1)] public string ign;

        public GSConfirmIdentityPacket(int _id, string _ign)
        {
            instance_id = _id;
            ign = _ign;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static GSConfirmIdentityPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<GSConfirmIdentityPacket>(bytes);
        }
    }
}