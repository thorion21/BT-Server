using System.Collections.Generic;
using Entities.Room;
using Interfaces;
using MessagePack;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject]
    public class RoomUpdatePacket : ISerialize
    {
        [Key(0)] public List<Room> NewRooms;
        [Key(1)] public List<ushort> RemovedRooms;
        
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