using System;
using System.Collections.Generic;
using System.Diagnostics;
using BT_WorldServer.WorldServer;
using System.Collections;
using BT_WorldServer.Interfaces;
using BT_WorldServer.utils;

namespace BT_WorldServer.WorldServer
{
    public class Room : IRoom
    {
        public static uint Id;
        public byte Map;
        public byte Status;
        public byte GameMode;
        public byte MaxPlayers;
        public Hashtable Players;
        public GenericPlayer Owner;
        
        public Room(byte gameMode, byte map, byte maxPlayers, GenericPlayer owner)
        {
            Id += 1;
            Map = map;
            Owner = owner;
            GameMode = gameMode;
            MaxPlayers = maxPlayers;
            Status = RoomStatus.ROOM_STATUS_WAITING;
            Players = new Hashtable {{Owner.IGN, Owner}};
        }

        public bool JoinRoom(GenericPlayer attendant)
        {
            if (Players.Count < MaxPlayers)
            {
                Players.Add(attendant.IGN, attendant);
                return true;
            }

            return false;
        }
        
        public Tuple<GenericPlayer, bool> LeaveRoom(string attendant)
        {
            bool roomShouldDelete = false;
            
            /* Case 1: One player left the room */
            GenericPlayer leavingPlayer = (GenericPlayer) Players[attendant];
            Players.Remove(attendant);

            /* Case 2: If it was the owner, pick a new one */
            if (Owner.IGN.Equals(attendant) && Players.Count > 0)
            {
                IDictionaryEnumerator penumerator = Players.GetEnumerator();
                penumerator.MoveNext();
                Owner = (GenericPlayer) penumerator.Entry.Value;
            }
            
            /* Case 3: No players left => Destroy room */
            if (Players.Count <= 0)
                roomShouldDelete = true;
            
            return Tuple.Create(leavingPlayer, roomShouldDelete);
        }

    }
}