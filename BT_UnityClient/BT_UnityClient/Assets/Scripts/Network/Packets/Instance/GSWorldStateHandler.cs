using Account;
using utils;

namespace Network.Packets.Instance
{
    public static class GSWorldStateHandler
    {
        private static MyAccount _account = MyAccount.Instance;
        public static void Handle(ref DefaultPacket packet, ref GameInstance gameInstance)
        {
            GSWorldStatePacket response = GSWorldStatePacket.Deserialize(packet.Buffer);

            foreach (var player in response.data)
            {
                if (player.Key != _account.GetIGN())
                    gameInstance.Signal(GameEvents.WorldState, (player.Key, player.Value));
            }
        }
    }
}