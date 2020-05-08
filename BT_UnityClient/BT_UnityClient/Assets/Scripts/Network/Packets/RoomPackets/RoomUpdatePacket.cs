using System.Collections.Generic;
using Entities.Room;
using Interfaces;
using MessagePack;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject]
    public class RoomUpdatePacket : ISerialize
    {
        [Key(0)] public List<Room> Added;
        [Key(1)] public List<Room> Removed;
        [Key(2)] public bool Full;
        
        [SerializationConstructor]
        public RoomUpdatePacket() { }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static RoomUpdatePacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<RoomUpdatePacket>(bytes);
        }
    }
}