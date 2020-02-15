using System.Collections.Generic;
using Entities.Lobby;
using Interfaces;
using MessagePack;

namespace Entities.Room
{
    [MessagePackObject]
    public class Room : ISerialize
    {
        [Key(0)] public ushort RoomID;
        [Key(1)] public byte Map;
        [Key(2)] public byte Status;
        [Key(3)] public byte GameMode;
        [Key(4)] public byte MaxPlayers;
        [Key(5)] public LobbyPlayer Owner;
        [Key(6)] public Dictionary<string, LobbyPlayer> Players;

        [SerializationConstructor]
        public Room() { }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public Room Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<Room>(bytes);
        }
    }
}