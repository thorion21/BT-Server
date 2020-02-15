using MessagePack;

namespace Entities.Lobby
{
    [MessagePackObject]
    public class LobbyPlayer
    {
        [Key(0)] public string IGN;

        public LobbyPlayer(string ign)
        {
            IGN = ign;
        }
    }
}