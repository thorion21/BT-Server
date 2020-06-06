using Interfaces;
using MessagePack;
using utils;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class GSCorrectionsPacket : ISerialize
    {
        [Key(0)] public uint tick_number;
        [Key(1)] public NetworkVector position;
        [Key(2)] public NetworkVector rotation;

        [SerializationConstructor]
        public GSCorrectionsPacket(uint _tick, NetworkVector _position, NetworkVector _rotation)
        {
            tick_number = _tick;
            position = _position;
            rotation = _rotation;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
        
        public static GSCorrectionsPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<GSCorrectionsPacket>(bytes);
        }
    }
}