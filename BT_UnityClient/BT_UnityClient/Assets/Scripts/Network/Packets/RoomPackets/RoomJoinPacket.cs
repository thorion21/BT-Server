using Interfaces;
using MessagePack;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject]
    public class RoomJoinPacket : ISerialize
    {
        [Key(0)] public ushort RoomID;
        [Key(1)] public string IGN;

        [SerializationConstructor]
        public RoomJoinPacket(ushort roomId, string ign)
        {
            RoomID = roomId;
            IGN = ign;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static RoomJoinPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<RoomJoinPacket>(bytes);
        }
    }
}