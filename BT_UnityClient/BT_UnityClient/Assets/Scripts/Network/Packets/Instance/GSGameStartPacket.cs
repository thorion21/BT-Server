using Interfaces;
using System.Collections.Generic;
using MessagePack;

namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class GSGameStartPacket : ISerialize
    {
        [Key(0)] public int instance_id;
        [Key(1)] public List<PacketPlayer> players;

        [SerializationConstructor]
        public GSGameStartPacket(int _id, List<PacketPlayer> _players)
        {
            instance_id = _id;
            players = _players;
        }

        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static GSGameStartPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<GSGameStartPacket>(bytes);
        }
    }
}