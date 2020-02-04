using Network.Packets;

namespace Entities.Lobby
{
    public static class LobbyManager
    {
        public static void Handle(ref Lobby lobby, ref DefaultPacket packet)
        {
            LoginPacketResponse response = LoginPacketResponse.Deserialize(packet.Buffer);
        }
    }
}