using Interfaces;
using MessagePack;

namespace Network.Packets.Instance
{
    namespace Network.Packets.RoomPackets
    {
        [MessagePackObject]
        public class StartGamePacket : ISerialize
        {
            [Key(0)] public ushort RoomID;
            [Key(1)] public string IGN;

            [SerializationConstructor]
            public StartGamePacket(ushort roomId, string ign)
            {
                RoomID = roomId;
                IGN = ign;
            }
        
            public byte[] AsByteArray()
            {
                return MessagePackSerializer.Serialize(this);
            }

            public static StartGamePacket Deserialize(byte[] bytes)
            {
                return MessagePackSerializer.Deserialize<StartGamePacket>(bytes);
            }
        }
    }
}