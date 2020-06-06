using Interfaces;
using MessagePack;
using utils;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class PacketPlayer : ISerialize
    {
        [Key(0)] public string Ign;
        [Key(1)] public NetworkVector position;
        [Key(2)] public NetworkVector rotation;

        [SerializationConstructor]
        public PacketPlayer(string _ign, NetworkVector _position, NetworkVector _rotation)
        {
            Ign = _ign;
            position = _position;
            rotation = _rotation;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static PacketPlayer Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<PacketPlayer>(bytes);
        }
    }
}