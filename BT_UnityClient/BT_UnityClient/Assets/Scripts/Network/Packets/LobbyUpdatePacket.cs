using System.Collections.Generic;
using Entities.Lobby;
using MessagePack;

namespace Network.Packets
{
    [MessagePackObject()]
    public class LobbyUpdatePacket
    {
        [Key(0)] public List<LobbyPlayer> Joining;
        [Key(1)] public List<LobbyPlayer> Leaving;

        [SerializationConstructor]
        public LobbyUpdatePacket()
        {
            
        }
    }
}