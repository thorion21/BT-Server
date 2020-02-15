using Entities.Lobby;
using Interfaces;
using MessagePack;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject]
    public class RoomCreationPacketResponse : ISerialize
    {
        [Key(0)] public uint Id;
        [Key(1)] public byte Status;
        [Key(2)] public byte Map;
        [Key(3)] public byte MaxPlayers;
        [Key(4)] public LobbyPlayer Owner;

        [SerializationConstructor]
        public RoomCreationPacketResponse(uint id, byte status,
            byte map, byte maxPlayers, LobbyPlayer owner)
        {
            Id = id;
            Status = status;
            Map = map;
            MaxPlayers = maxPlayers;
            Owner = owner;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public RoomCreationPacketResponse Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<RoomCreationPacketResponse>(bytes);
        }
    }
}