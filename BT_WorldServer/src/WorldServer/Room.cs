using System.Collections;
using System.Collections.Generic;
using BT_WorldServer.Interfaces;
using BT_WorldServer.utils;
using MessagePack;

namespace BT_WorldServer.WorldServer
{
    [MessagePackObject]
    public class Room : IRoom, ISerialize
    {
        [Key(0)] public byte GameMode;
        [Key(1)] public byte Map;
        [Key(2)] public byte MaxPlayers;
        [Key(3)] public GenericPlayer Owner;
        [Key(4)] public static uint Id;
        [Key(5)] public byte Status;
        [Key(6)] public Dictionary<string, GenericPlayer> Players;
        

        [SerializationConstructor]
        public Room(byte gameMode, byte map, byte maxPlayers, GenericPlayer owner)
        {
            Id += 1;
            Map = map;
            Owner = owner;
            GameMode = gameMode;
            MaxPlayers = maxPlayers;
            Status = RoomStatus.ROOM_STATUS_WAITING;
            Players = new Dictionary<string, GenericPlayer>();
            JoinRoom(Owner);
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
        
        public GenericPlayer LeaveRoom(string attendant, out bool roomShouldDelete)
        {
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
            roomShouldDelete = Players.Count == 0;
            
            return leavingPlayer;
        }

        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
    }
}