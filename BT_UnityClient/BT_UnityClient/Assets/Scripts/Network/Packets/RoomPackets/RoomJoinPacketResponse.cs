using Interfaces;
using MessagePack;
using System.Collections.Generic;
using Entities.Lobby;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject]
    public class RoomJoinPacketResponse : ISerialize
    {
        [Key(0)] public ushort RoomID;
        [Key(1)] public bool Status;
        [Key(2)] public List<LobbyPlayer> AddedPlayers;
        [Key(3)] public List<LobbyPlayer> RemovedPlayers;

        [SerializationConstructor]
        public RoomJoinPacketResponse(ushort id, bool status, List<LobbyPlayer> addedPlayers, List<LobbyPlayer> removedPlayers)
        {
            RoomID = id;
            Status = status;
            AddedPlayers = addedPlayers;
            RemovedPlayers = removedPlayers;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static RoomJoinPacketResponse Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<RoomJoinPacketResponse>(bytes);
        }
    }
}
