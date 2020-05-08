using System.Collections.Generic;
using Entities.Lobby;
using Events;
using Interfaces;
using MessagePack;
using utils;

namespace Entities.Room
{
    [MessagePackObject]
    public class Room : ISerialize
    {
        [Key(0)] public ushort RoomID;
        [Key(1)] public byte Map;
        [Key(2)] public byte GameMode;
        [Key(3)] public byte MaxPlayers;
        [Key(4)] public LobbyPlayer Owner;
        [Key(5)] public byte Status;
        [Key(6)] public Dictionary<string, LobbyPlayer> Players;

        [SerializationConstructor]
        public Room(ushort id, byte map, byte gameMode, byte maxPlayers, LobbyPlayer owner)
        {
            RoomID = id;
            Map = map;
            Owner = owner;
            GameMode = gameMode;
            MaxPlayers = maxPlayers;
        }

        public void JoinRoom(LobbyPlayer player)
        {
            Players.Add(player.IGN, player);
        }

        public void LeaveRoom(LobbyPlayer player)
        {
            Players.Remove(player.IGN);
        }
        
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