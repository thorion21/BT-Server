using System.Collections.Generic;
using BT_WorldServer.WorldServer;
using BT_WorldServer.utils;

namespace BT_WorldServer.WorldServer
{
    public class Room
    {
        public static uint Id;
        public byte GameMode;
        public byte Map;
        public byte CurrentPlayers;
        public byte MaxPlayers;
        public byte Status;
        public GenericPlayer Owner;
        public List<GenericPlayer> Players;
        
        public Room(byte _gamemode, byte _map, byte _maxPlayers, GenericPlayer _owner)
        {
            Id += 1;
            GameMode = _gamemode;
            Map = _map;
            CurrentPlayers = 1;
            MaxPlayers = _maxPlayers;
            Status = RoomStatus.ROOM_STATUS_WAITING;
            Owner = _owner;
            Players = new List<GenericPlayer>(MaxPlayers);
        }

        public bool JoinRoom(GenericPlayer attendant)
        {
            if (Players.Count < MaxPlayers)
            {
                Players.Add(attendant);
                CurrentPlayers += 1;
                return true;
            }

            return false;
        }

        public void ShouldDestroyOnLeave(GenericPlayer attendant)
        {
            /* Case 1: No players left => Destroy room */

            /* Case 2: If it was the owner, pick a new one */

            /* Case 3: One player left the room */
        }
        
    }
}