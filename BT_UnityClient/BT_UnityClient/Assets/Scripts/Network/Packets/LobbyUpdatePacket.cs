using System.Collections.Generic;
using Entities.Lobby;
using Interfaces;
using MessagePack;

namespace Network.Packets
{
    [MessagePackObject]
    public class LobbyUpdatePacket : ISerialize
    {
        [Key(0)] public List<LobbyPlayer> Joining;
        [Key(1)] public List<LobbyPlayer> Leaving;

        [SerializationConstructor]
        public LobbyUpdatePacket(List<LobbyPlayer> joining, List<LobbyPlayer> leaving)
        {
            Joining = joining;
            Leaving = leaving;
        }

        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static LobbyUpdatePacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<LobbyUpdatePacket>(bytes);
        }
    }
}