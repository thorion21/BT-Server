using Interfaces;
using MessagePack;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject()]
    public class RoomExitPacket : ISerialize
    {
        [Key(0)] public string IGN;
        [Key(1)] public ushort RoomID;

        [SerializationConstructor]
        public RoomExitPacket(string ign, ushort roomid)
        {
            IGN = ign;
            RoomID = roomid;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
        
        public static RoomExitPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<RoomExitPacket>(bytes);
        }
    }
}