using Network.Packets;

namespace Entities.PlayersLobby
{
    public static class LobbyResponseHandler
    {
        public static void Handle(ref Lobby lobby, ref DefaultPacket packet)
        {
            LobbyUpdatePacket response = LobbyUpdatePacket.Deserialize(packet.Buffer);

            foreach (var player in response.Joining)
            {
                lobby.JoinLobby(player);
            }

            foreach (var player in response.Leaving)
            {
                lobby.RemoveFromLobby(player);
            }
        }
    }
}