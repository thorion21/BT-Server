using Interfaces;
using MessagePack;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class GSInstanceReady : ISerialize
    {
        [Key(0)] public int id;

        public GSInstanceReady(int _id)
        {
            id = _id;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static GSInstanceReady Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<GSInstanceReady>(bytes);
        }
    }
}