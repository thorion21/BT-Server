using System.Collections.Generic;

namespace Entities.Lobby
{
    public class Lobby : Singleton<Lobby>
    {
        private Dictionary<string, LobbyPlayer> _players;

        public Lobby()
        {
            _players = new Dictionary<string, LobbyPlayer>();
        }

        public void JoinLobby(LobbyPlayer player)
        {
            _players[player.IGN] = player;
        }

        public void RemoveFromLobby(LobbyPlayer player)
        {
            _players.Remove(player.IGN);
        }

        public Dictionary<string, LobbyPlayer>.ValueCollection GetLobbyPlayerList()
        {
            return _players.Values;
        }
    }
}