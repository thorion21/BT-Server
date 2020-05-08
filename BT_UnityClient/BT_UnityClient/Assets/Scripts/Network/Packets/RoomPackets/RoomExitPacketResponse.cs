using Interfaces;
using MessagePack;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject()]
    public class RoomExitPacketResponse : ISerialize
    {
        [Key(0)] public string IGN;
        [Key(1)] public ushort RoomID;

        [SerializationConstructor]
        public RoomExitPacketResponse(string ign, ushort roomid)
        {
            IGN = ign;
            RoomID = roomid;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
        
        public static RoomExitPacketResponse Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<RoomExitPacketResponse>(bytes);
        }
    }
}